using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class UIController : MonoBehaviour
    {
        [Header("---Setup---")]
        public GameObject RowPrefab;
        public GameObject SelectorPrefab;
        public GameObject VerticalLinePrefab;
        public RectTransform rowAnchor;
        public UnityEngine.UI.Text textResult;
                
        [SerializeField] int ItemsPerRow = 0;
        [SerializeField] int TotalRows = 0;
        [SerializeField] float RowIndent = 0f;
        [SerializeField] [Range(0.01f, 1f)] float UpdateDelay = 0f;

        [Header("---Debug---")]
        [SerializeField] GameLogic gameLogic;
        [SerializeField] List<RectTransform> RowUIList = new List<RectTransform>();
        [SerializeField] List<RectTransform> SelectorUIList = new List<RectTransform>();

        private void Update()
        {
            UpdateDelayManager.delay = UpdateDelay;

            if (gameLogic != null)
            {
                for (int i = 0; i < gameLogic.TOTAL_ROWS; i++)
                {
                    if (i < SelectorUIList.Count && i < RowUIList.Count)
                    {
                        int totalItems = gameLogic.GetRow(i).listInts.Count;
                        int selection = gameLogic.GetRow(i).selector.INDEX;
                        float localX = GetLocalUIXPos(RowUIList[i], totalItems, selection);

                        SelectorUIList[i].anchoredPosition = new Vector2(localX, 0f);
                        textResult.text = ResultManager.totalCombinations.ToString();
                    }
                }
            }
        }

        void SetupGraphics()
        {
            SelectorUIList.Clear();

            for (int nRows = 0; nRows < gameLogic.TOTAL_ROWS; nRows++)
            {
                RectTransform rect = CreateRowUI(nRows);

                AttachSelector(rect);

                Row r = gameLogic.GetRow(nRows);
                AttachVerticalLines(rect, r.listInts.Count);
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

        private void AttachVerticalLines(RectTransform rectTransform, int nCount)
        {
            for (int i = 0; i < nCount; i++)
            {
                float localX = GetLocalUIXPos(rectTransform, nCount, i);
                Vector2 localPos = new Vector2(localX, 0);

                GameObject vertObj = Instantiate(VerticalLinePrefab);
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

        public void OnClickGo()
        {
            Debugger.Log("---go!---");

            if (gameLogic == null)
            {
                gameLogic = FindObjectOfType<GameLogic>();
            }
            
            gameLogic.SetupMachine(ItemsPerRow, TotalRows);
            SetupGraphics();
            gameLogic.StartMachine();
        }
    }
}