using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class UIController : MonoBehaviour
    {
        [Header("---Setup---")]
        [SerializeField] GameObject RowPrefab;
        [SerializeField] GameObject SelectorPrefab;
        [SerializeField] GameObject VerticalLinePrefab;
        [SerializeField] RectTransform rowAnchor;
        [SerializeField] UnityEngine.UI.Text textCurrentCombination;
        [SerializeField] UnityEngine.UI.Text textTotalCombinations;
        [SerializeField] UnityEngine.UI.InputField itemField;
        [SerializeField] UnityEngine.UI.InputField rowField;
        [SerializeField] UnityEngine.UI.Slider delaySlider;
        [SerializeField] float RowIndent = 0f;
        

        [Header("---Debug---")]
        [SerializeField] GameLogic gameLogic;
        [SerializeField] List<RectTransform> RowUIList = new List<RectTransform>();
        [SerializeField] List<RectTransform> SelectorUIList = new List<RectTransform>();

        Coroutine UpdateRoutine = null;

        IEnumerator _UpdateUI()
        {
            while(true)
            {
                UpdateDelayManager.delay = delaySlider.value;

                if (gameLogic != null && ModeManager.mode == GameModes.NORMAL)
                {
                    for (int i = 0; i < gameLogic.TOTAL_ROWS; i++)
                    {
                        if (i < SelectorUIList.Count && i < RowUIList.Count)
                        {
                            int selection = gameLogic.GetRow(i).selector.INDEX;
                            PlaceSelectorUI(i, selection);

                            textTotalCombinations.text = ResultManager.totalCombinations.ToString();
                        }
                    }
                }

                textCurrentCombination.text = ResultManager.currentCombination;

                yield return new WaitForEndOfFrame();
            }
        }

        public void PlaceSelectorUI(int rowIndex, int x)
        {
            int totalItems = gameLogic.GetRow(rowIndex).listInts.Count;
            float localX = GetLocalUIXPos(RowUIList[rowIndex], totalItems, x);

            SelectorUIList[rowIndex].anchoredPosition = new Vector2(localX, 0f);
        }

        void SetupGraphics()
        {
            SelectorUIList.Clear();

            for (int rowID = 0; rowID < gameLogic.TOTAL_ROWS; rowID++)
            {
                RectTransform rect = CreateRowUI(rowID);

                AttachSelector(rect);

                Row r = gameLogic.GetRow(rowID);
                AttachVerticalLines(rect, r.listInts.Count, rowID);
            }
        }

        private RectTransform CreateRowUI(int rowCount)
        {
            GameObject row = Instantiate(RowPrefab);
            Vector2 localP = new Vector2(0f, 0f);
            float yOffset = -RowIndent;
            localP.y = yOffset * rowCount;
            AttachToRect(row, rowAnchor, localP);

            RowUIList.Add(row.GetComponent<RectTransform>());

            return row.GetComponent<RectTransform>();
        }

        private void AttachSelector(RectTransform rowRectTransform)
        {
            GameObject selectorObj = Instantiate(SelectorPrefab);
            AttachToRect(selectorObj, rowRectTransform, Vector2.zero);

            SelectorUIList.Add(selectorObj.GetComponent<RectTransform>());
        }

        private void AttachVerticalLines(RectTransform rectTransform, int nCount, int rowID)
        {
            for (int i = 0; i < nCount; i++)
            {
                float localX = GetLocalUIXPos(rectTransform, nCount, i);
                Vector2 localPos = new Vector2(localX, 0);

                GameObject vertObj = Instantiate(VerticalLinePrefab);

                VerticalLineUI lineUI = vertObj.GetComponent<VerticalLineUI>();
                lineUI.Setup(this, i, rowID);

                AttachToRect(vertObj, rectTransform, localPos);
            }
        }

        float GetLocalUIXPos(RectTransform horizontalLine, int totalItems, int selection)
        {
            float leftside = -(float)horizontalLine.rect.width / 2f;
            float offset = 0;

            if (totalItems > 1)
            {
                offset = (float)horizontalLine.rect.width / (totalItems - 1);
            }

            return leftside + (offset * selection);
        }

        private void AttachToRect(GameObject obj, RectTransform parentRect, Vector2 localPos)
        {
            obj.transform.parent = parentRect.transform;
            RectTransform objRect = obj.GetComponent<RectTransform>();
            objRect.anchoredPosition = localPos;
        }

        void FindGameLogic()
        {
            if (gameLogic == null)
            {
                gameLogic = FindObjectOfType<GameLogic>();
            }
        }

        public void OnClickGo()
        {
            ModeManager.mode = GameModes.NORMAL;
            Debug.Log("---go!---");
            FindGameLogic();
            Cleanup();

            int itemCount = int.Parse(itemField.text);
            int rowCount = int.Parse(rowField.text);

            gameLogic.SetupMachine(itemCount, rowCount);
            SetupGraphics();

            gameLogic.StartMachine();

            UpdateRoutine = StartCoroutine(_UpdateUI());
        }

        public void OnClickManualMode()
        {
            ModeManager.mode = GameModes.MANUAL_CLICK;
            Debug.Log("---manual mode---");
            FindGameLogic();
            Cleanup();

            int itemCount = int.Parse(itemField.text);
            int rowCount = int.Parse(rowField.text);

            gameLogic.SetupMachine(itemCount, rowCount);
            SetupGraphics();

            // place all selectors back to beginning
            for (int i = 0; i < gameLogic.TOTAL_ROWS; i++)
            {
                PlaceSelectorUI(i, 0);
            }
        }

        public void OnClickQuit()
        {
            Application.Quit();
        }

        void Cleanup()
        {
            foreach (RectTransform rect in RowUIList)
            {
                Destroy(rect.gameObject);
            }

            RowUIList.Clear();
            SelectorUIList.Clear();

            ResultManager.totalCombinations = 0;
            textTotalCombinations.text = "0";

            if (UpdateRoutine != null)
            {
                StopCoroutine(UpdateRoutine);
            }
        }
    }
}