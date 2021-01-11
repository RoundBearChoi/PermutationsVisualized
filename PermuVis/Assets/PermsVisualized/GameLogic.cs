using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField]
        private List<Row> Rows = new List<Row>();

        private void Start()
        {
            Setup();
            StartMachine();
        }

        public int ROW_COUNT
        {
            get
            {
                return Rows.Count;
            }
        }

        public void Setup()
        {
            Rows.Clear();

            Row r0 = new Row(7);
            Row r1 = new Row(7);
            Row r2 = new Row(7);
            Row r3 = new Row(7);
            Row r4 = new Row(7);
            Row r5 = new Row(7);

            Rows.Add(r0);
            Rows.Add(r1);
            Rows.Add(r2);
            Rows.Add(r3);
            Rows.Add(r4);
            Rows.Add(r5);
        }

        public void StartMachine()
        {
            UIController uiController = FindObjectOfType<UIController>();
            uiController.SetupPermMachine(this);
        }

        public Row GetItem(int rowIndex)
        {
            return Rows[rowIndex];
        }
    }
}