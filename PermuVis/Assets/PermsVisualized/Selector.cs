using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [System.Serializable]
    public class Selector
    {
        private Row mOwner;

        [SerializeField]
        private int index = 0;

        public int INDEX
        {
            get
            {
                return index;
            }
        }

        public int VALUE
        {
            get
            {
                return mOwner.listInts[index];
            }
        }

        public Selector(Row owner)
        {
            mOwner = owner;
        }

        public void MoveIndexRight()
        {
            index++;
        }
    }
}