using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class UIController : MonoBehaviour
    {
        [Header("---Setup---")]
        [SerializeField]
        public RectTransform CenterAnchor;
        public List<RectTransform> RowUIElements;
        public GameObject RowPrefab;
        public GameObject SelectorPrefab;
        public GameObject VerticalLinePrefab;
        public GameObject FixerPrefab;
        public RectTransform rowAnchor;

        [Header("---Debug---")]
        [SerializeField]
        public GameLogic gameLogic;

        public void SetupPermMachine(GameLogic logic)
        {
            gameLogic = logic;

            for (int nRows = 0; nRows < gameLogic.ROW_COUNT; nRows++)
            {
                RectTransform rect = CreateRowUI(nRows);
                Row r = gameLogic.GetItem(nRows);

                for (int i = 0; i < r.listInts.Count ; i++)
                {
                    AttachSelector(rect);
                    AttachVerticalLines(rect, r.listInts.Count);
                }
            }

            AttachFixer(CenterAnchor);
        }

        private RectTransform CreateRowUI(int rowCount)
        {
            GameObject row = Instantiate(RowPrefab);
            Vector2 localP = new Vector2(0f, 0f);
            float yOffset = -120f;
            localP.y = yOffset * rowCount;
            AttachToRect(row, rowAnchor, localP);

            return row.GetComponent<RectTransform>();
        }

        private void AttachSelector(RectTransform rowRectTransform)
        {
            GameObject selectorObj = Instantiate(SelectorPrefab);
            AttachToRect(selectorObj, rowRectTransform, Vector2.zero);
        }

        private void AttachVerticalLines(RectTransform rectTransform, int nCount)
        {
            for (int i = 0; i <= nCount; i++)
            {
                float leftside = -(float)rectTransform.rect.width / 2f;
                float offset = (float)rectTransform.rect.width / (float)nCount;
                Vector2 localPos = new Vector2(leftside + (offset * i), 0);

                GameObject vertObj = Instantiate(VerticalLinePrefab);
                AttachToRect(vertObj, rectTransform, localPos);
            }
        }

        private void AttachFixer(RectTransform centerRectTransform)
        {
            GameObject fixerObj = Instantiate(FixerPrefab);
            AttachToRect(fixerObj, centerRectTransform, Vector2.zero);
        }

        private void AttachToRect(GameObject obj, RectTransform parentRect, Vector2 localPos)
        {
            obj.transform.parent = parentRect.transform;
            RectTransform objRect = obj.GetComponent<RectTransform>();
            objRect.anchoredPosition = localPos;
        }
    }
}