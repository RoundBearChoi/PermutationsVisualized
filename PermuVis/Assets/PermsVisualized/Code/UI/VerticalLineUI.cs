using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Roundbeargames
{
    public class VerticalLineUI : MonoBehaviour, IPointerClickHandler
    {
        GameLogic gameLogic = null;
        UIController mUIController = null;
        int mRowID = 0;
        int mX = 0;

        public void Setup(UIController uiController, int xID, int rowID)
        {
            mUIController = uiController;
            mRowID = rowID;
            mX = xID;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            Debug.Log("vertical line clicked: " + this.gameObject.name);

            mUIController.SetSelector(mRowID, mX);

            if (gameLogic == null)
            {
                gameLogic = FindObjectOfType<GameLogic>();
            }

            gameLogic.UpdateResult();
            mUIController.PrintUpdatedResult();
        }
    }
}