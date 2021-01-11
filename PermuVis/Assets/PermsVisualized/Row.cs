using System.Collections;
using System.Collections.Generic;

namespace Roundbeargames
{
    [System.Serializable]
    public class Row
    {
        public List<int> listInts = new List<int>();
        public Selector selector;

        public Row(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                listInts.Add(i);
            }
        }
    }
}