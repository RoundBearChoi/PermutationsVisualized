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

        public bool MoveFixer()
        {
            if (fixer.x < rows[fixer.rowID].listInts.Count - 1)
            {
                fixer.x++;
                return true;
            }
            else
            {
                return false;
            }
        }

        void ResetSelectors()
        {
            for (int i = 0; i < rows.Count; i++)
            {
                if (i != fixer.rowID)
                {
                    rows[i].selector.SetIndex(0);
                }
                else
                {
                    rows[i].selector.SetIndex(fixer.x);
                }
            }
        }

        public void PrintAll(List<Row> targetRows)
        {
            rows = targetRows;

            while(true)
            {
                PrintCombinations();

                // can selector move?
                if (MoveSelectors())
                {
                    // yes: move selector and repeat
                }
                // if not, can fixer move?
                else if (MoveFixer())
                {
                    // yes: move fixer, reset selectors, and repeat
                    ResetSelectors();
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

                // print each item selected
                for (int i = 0; i < rows.Count; i++)
                {
                    combination += rows[i].selector.VALUE + " ";
                }

                Debugger.Log(combination);
                totalCombinations++;

                Row bottomRow = rows[rows.Count - 1];

                // can one selector move right? (bottom)
                if (bottomRow.SelectorCanMoveRight())
                {
                    // move one selector and repeat
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