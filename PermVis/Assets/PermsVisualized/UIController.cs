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
                AttachFixer(CenterAnchor);
            }
        }

        private void AttachSelector(RectTransform rowRectTransform)
        {
            GameObject selectorObj = Instantiate(SelectorPrefab);

            selectorObj.transform.parent = rowRectTransform.transform;
            RectTransform selectorRectTransform = selectorObj.GetComponent<RectTransform>();
            selectorRectTransform.position = Vector3.zero;
            selectorRectTransform.anchoredPosition = Vector2.zero;
        }

        private void AttachFixer(RectTransform centerRectTransform)
        {
            GameObject fixerObj = Instantiate(FixerPrefab);

            fixerObj.transform.parent = centerRectTransform.transform;
            RectTransform fixerRectTransform = fixerObj.GetComponent<RectTransform>();
            fixerRectTransform.position = Vector3.zero;
            fixerRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}