using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PermMachine : MonoBehaviour
    {
        List<Row> rows = null;

        IEnumerator _DelayedUpdate()
        {
            while (true)
            {
                yield return PrintCombination();

                if (MoveSelector())
                {

                }
                else
                {
                    break;
                }
            }

            Debug.Log("total combinations: " + ResultManager.totalCombinations);
        }

        public bool MoveSelector()
        {
            for (int i = rows.Count - 1; i >= 0; i--)
            {
                if (rows[i].SelectorCanMoveRight())
                {
                    // set lowest movable selector to the right
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

        IEnumerator PrintCombination()
        {
            while (true)
            {
                UpdateResult();

                yield return new WaitForSeconds(UpdateDelayManager.delay);

                Row bottomRow = rows[rows.Count - 1];

                // can one selector move right? (bottom)
                if (bottomRow.SelectorCanMoveRight())
                {
                    // move one selector and repeat
                    bottomRow.selector.MoveIndexRight();
                }
                else
                {
                    Debug.Log("---");
                    break;
                }
            }
        }

        public void SetTargetRows(List<Row> targetRows)
        {
            rows = targetRows;
        }

        public void UpdateResult()
        {
            ResultManager.currentCombination = string.Empty;

            // print each item selected
            for (int i = 0; i < rows.Count; i++)
            {
                ResultManager.currentCombination += rows[i].selector.VALUE + "     ";
            }

            Debug.Log(ResultManager.currentCombination);

            if (ModeManager.mode == GameModes.NORMAL)
            {
                ResultManager.totalCombinations++;
            }
        }

        public void PrintAll()
        {
            ResultManager.totalCombinations = 0;
            StartCoroutine(_DelayedUpdate());
        }
    }
}