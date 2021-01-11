using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] List<Row> RowsList = new List<Row>();
        PermMachine permMachine = new PermMachine();

        void Start()
        {
            Run();
        }

        public void Run()
        {
            UIController uiController = FindObjectOfType<UIController>();

            // rows auto setup
            for (int i = 0; i < uiController.TOTAL_ROWS; i++)
            {
                CreateRow(uiController.ITEMS_PER_ROW);
            }

            // rows manual setup
            /*
            Row r0 = new Row();
            r0.listInts.Add(1);
            r0.listInts.Add(2);
            r0.listInts.Add(3);

            Row r1 = new Row();
            r1.listInts.Add(4);

            Row r2 = new Row();
            r2.listInts.Add(5);
            r2.listInts.Add(6);

            RowsList.Add(r0);
            RowsList.Add(r1);
            RowsList.Add(r2);
            */

            uiController.SetupGraphics(this);
            permMachine.PrintAll(RowsList);
        }

        void CreateRow(int nItemCount)
        {
            Row row = new Row();

            for (int i = 0; i < nItemCount; i++)
            {
                row.listInts.Add(i);
            }

            RowsList.Add(row);
        }

        public Row GetRow(int index)
        {
            return RowsList[index];
        }

        public int TOTAL_ROWS
        {
            get
            {
                return RowsList.Count;
            }
        }
    }
}