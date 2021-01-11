using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField]
        private List<Row> RowsList = new List<Row>();
        PermMachine permMachine = new PermMachine();

        private void Start()
        {
            Run();
            StartGraphics();
        }

        public int ROW_COUNT
        {
            get
            {
                return RowsList.Count;
            }
        }

        public void Run()
        {
            Debugger.Log("game logic started");

            RowsList.Clear();

            Row r0 = new Row(7); r0.ID = 0;
            Row r1 = new Row(7); r1.ID = 1;
            Row r2 = new Row(7); r2.ID = 2;
            Row r3 = new Row(7); r3.ID = 3;
            Row r4 = new Row(7); r4.ID = 4;
            Row r5 = new Row(7); r5.ID = 5;

            RowsList.Add(r0);
            RowsList.Add(r1);
            RowsList.Add(r2);
            RowsList.Add(r3);
            RowsList.Add(r4);
            RowsList.Add(r5);

            permMachine.PrintAll(RowsList);
        }

        public void StartGraphics()
        {
            UIController uiController = FindObjectOfType<UIController>();
            uiController.SetupPermMachine(this);
        }

        public Row GetItem(int rowIndex)
        {
            return RowsList[rowIndex];
        }
    }
}