using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class GameLogic : MonoBehaviour
    {
        [Header("---Debug---")]
        [SerializeField] List<Row> RowsList = new List<Row>();
        [SerializeField] PermMachine permMachine = null;

        public int TOTAL_ROWS
        {
            get
            {
                return RowsList.Count;
            }
        }

        void Cleanup()
        {
            if (permMachine != null)
            {
                Destroy(permMachine.gameObject);
            }

            RowsList.Clear();
        }

        public void SetupMachine(int itemsPerRow, int totalRows)
        {
            Cleanup();

            permMachine = CreateMachine();

            // rows setup
            for (int i = 0; i < totalRows; i++)
            {
                Row r = CreateRow(itemsPerRow);
                RowsList.Add(r);
            }

            // rows manual setup
            // https://www.geeksforgeeks.org/combinations-from-n-arrays-picking-one-element-from-each-array/
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

            permMachine.SetTargetRows(RowsList);
        }

        public PermMachine CreateMachine()
        {
            GameObject obj = new GameObject();
            obj.name = typeof(PermMachine).Name;
            PermMachine machine = obj.AddComponent(typeof(PermMachine)) as PermMachine;

            return machine;
        }

        Row CreateRow(int nItemCount)
        {
            Row row = new Row();

            for (int i = 0; i < nItemCount; i++)
            {
                row.listInts.Add(i);
            }

            return row;
        }

        public void StartMachine()
        {
            permMachine.PrintAll();
        }

        public Row GetRow(int index)
        {
            return RowsList[index];
        }

        public void UpdateResult()
        {
            if (permMachine != null)
            {
                permMachine.PrintCombination();
            }
        }
    }
}