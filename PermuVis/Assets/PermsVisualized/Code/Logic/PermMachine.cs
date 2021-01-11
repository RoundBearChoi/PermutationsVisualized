using System.Collections;
using System.Collections.Generic;

namespace Roundbeargames
{
    public class PermMachine
    {
        List<Row> rows = null;
        int totalCombinations = 0;

        public bool MoveSelectors()
        {
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                if (rows[i].SelectorCanMoveRight())
                {
                    // set farthest movable selector to the right
                    rows[i].selector.MoveIndexRight();

                    // set all lower selectors back to starting point
                    for (int k = i + 1; k < rows.Count; k++)
                    {
                        rows[k].selector.SetIndex(0);
                    }

                    return true;
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

                // can selector move?
                // if yes: move selector and stay in loop
                if (MoveSelectors())
                {
                    
                }
                // if not: end loop
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