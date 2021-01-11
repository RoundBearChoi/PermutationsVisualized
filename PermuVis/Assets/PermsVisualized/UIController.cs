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
        public GameObject SelectorPrefab;
        public GameObject VerticalLinePrefab;
        public GameObject FixerPrefab;

        [Header("---Debug---")]
        [SerializeField]
        private List<Row> Rows;

        private void Start()
        {
            SetupPermMachine();
        }

        private void SetupPermMachine()
        {
            Rows.Clear();

            foreach (RectTransform t in RowUIElements)
            {
                Row r = new Row();
                Rows.Add(r);

                AttachSelector(t);
                AttachVerticalLines(t, r.listInts.Count);
            }

            AttachFixer(CenterAnchor);
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