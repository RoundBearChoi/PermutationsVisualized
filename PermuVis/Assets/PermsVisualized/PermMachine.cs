using System.Collections;
using System.Collections.Generic;

namespace Roundbeargames
{
    public class PermMachine
    {
        List<Row> rows = null;
        int totalCombinations = 0;

        public void PrintAll(List<Row> targetRows)
        {
            rows = targetRows;

            while(true)
            {
                PrintCombinations();

                // can fixer move?
                // yes: move fixer
                // no: end loop

                break;
            }

            Debugger.Log("total combinations: " + totalCombinations);
        }

        private void PrintCombinations()
        {
            while (true)
            {
                string combination = string.Empty;

                for (int i = 0; i < rows.Count; i++)
                {
                    combination += rows[i].selector.VALUE + " ";
                }

                Debugger.Log(combination);
                totalCombinations++;

                Row bottomRow = rows[rows.Count - 1];

                if (bottomRow.SelectorCanMoveRight())
                {
                    bottomRow.selector.MoveIndexRight();
                }
                else
                {
                    Debugger.Log("---end of printall---");
                    break;
                }
            }
        }
    }
}