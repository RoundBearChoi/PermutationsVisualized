using System.Collections;
using System.Collections.Generic;

namespace Roundbeargames
{
    public class PermMachine
    {
        List<Row> rows = null;
        Fixer fixer = new Fixer();
        int totalCombinations = 0;

        public bool MoveSelectors()
        {
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                if (i > fixer.rowID)
                {
                    if (rows[i].SelectorCanMoveRight())
                    {
                        // set movable selector to the right
                        rows[i].selector.MoveIndexRight();

                        // set lower selectors back to starting point
                        for (int k = i + 1; k < rows.Count; k++)
                        {
                            rows[k].selector.SetIndex(0);
                        }

                        return true;
                    }

                }
            }

            return false;
        }

        public void PrintAll(List<Row> targetRows)
        {
            rows = targetRows;

            while(true)
            {
                PrintCombinations();

                // can fixer move?
                // yes: move fixer
                // no: end loop

                if (MoveSelectors())
                {

                }
                else
                {
                    break;
                }
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
                    Debugger.Log("---");
                    break;
                }
            }
        }
    }
}