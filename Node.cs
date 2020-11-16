using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Sportsmen
{
    [Serializable]
    public class Node
    {
        public int key;
        public Sportsman man;
        public Node left;
        public Node right;

        public Node(int key, Sportsman man)
        {
            this.key = key;
            this.man = man;
        }
        public Node() { }

        public Sportsman Sportsman
        {
            get => default;
            set
            {
            }
        }
    }

}
