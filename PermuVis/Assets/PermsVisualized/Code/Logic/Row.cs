using System.Collections;
using System.Collections.Generic;

namespace Roundbeargames
{
    [System.Serializable]
    public class Row
    {
        public List<int> listInts = new List<int>();
        public Selector selector;

        public Row()
        {
            selector = new Selector(this);
        }

        public Row(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                listInts.Add(i);
            }

            selector = new Selector(this);
        }

        public bool SelectorCanMoveRight()
        {
            // selector index can be increased by 1
            // selector index is lower than the last element
            if (selector.INDEX < listInts.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}