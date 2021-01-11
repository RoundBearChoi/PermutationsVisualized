using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class UIController : MonoBehaviour
    {
        [Header("---Setup---")]
        public List<RectTransform> RowUIElements;
        public GameObject RowPrefab;
        public GameObject SelectorPrefab;
        public GameObject VerticalLinePrefab;
        public RectTransform rowAnchor;

        [SerializeField] int TotalRows = 0;
        [SerializeField] int ItemsPerRow = 0;
        [SerializeField] float RowIndent = 0f;

        [Header("---Debug---")]
        [SerializeField]
        public GameLogic gameLogic;

        public int TOTAL_ROWS
        {
            get
            {
                return TotalRows;
            }
        }

        public int ITEMS_PER_ROW
        {
            get
            {
                return ItemsPerRow;
            }
        }

        public void SetupGraphics(GameLogic logic)
        {
            gameLogic = logic;

            for (int nRows = 0; nRows < gameLogic.TOTAL_ROWS; nRows++)
            {
                RectTransform rect = CreateRowUI(nRows);

                Row r = gameLogic.GetRow(nRows);

                for (int i = 0; i < r.listInts.Count ; i++)
                {
                    AttachSelector(rect);
                    AttachVerticalLines(rect, r.listInts.Count);
                }
            }
        }

        private RectTransform CreateRowUI(int rowCount)
        {
            GameObject row = Instantiate(RowPrefab);
            Vector2 localP = new Vector2(0f, 0f);
            float yOffset = -RowIndent;
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
            for (int i = 0; i < nCount; i++)
            {
                float leftside = -(float)rectTransform.rect.width / 2f;
                float offset = 0;
                
                if (nCount > 1)
                {
                    offset = (float)rectTransform.rect.width / (nCount - 1);
                }

                Vector2 localPos = new Vector2(leftside + (offset * i), 0);

                GameObject vertObj = Instantiate(VerticalLinePrefab);
                AttachToRect(vertObj, rectTransform, localPos);
            }
        }

        private void AttachToRect(GameObject obj, RectTransform parentRect, Vector2 localPos)
        {
            obj.transform.parent = parentRect.transform;
            RectTransform objRect = obj.GetComponent<RectTransform>();
            objRect.anchoredPosition = localPos;
        }
    }
}