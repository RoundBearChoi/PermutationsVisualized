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
            listInts.Add(0);
            listInts.Add(1);
            listInts.Add(2);
            listInts.Add(3);
            listInts.Add(4);
            listInts.Add(5);
        }
    }
}