using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XML_Sportsmen
{
    [Serializable]
public class Tree
    {
       
                     
        public Node root;

        public Tree(Node root)
        {
            this.root = root;
        }

        public Tree(){}

        public Node Node
        {
            get => default;
            set
            {
            }
        }

        public void insert(Sportsman man_)
        {
            root = doInsert(root, man_.GetHashCode(),man_);
        }

        private static Node doInsert(Node node, int x, Sportsman man_)
        {
            if (node == null)
            {
                return new Node(x,man_);
            }
            if (x < node.key)
            {
                node.left = doInsert(node.left, x, man_);
            }
            else if (x > node.key)
            {
                node.right = doInsert(node.right, x, man_);
            }
            return node;
        }


        public void DeleteNodeFromBinary(ref Node node,ref Node parent,int key)
        {
            if (node == null)
                return;

            if (key < node.key)
                DeleteNodeFromBinary(ref node.left, ref node, key);

            else if (key > node.key)
                DeleteNodeFromBinary(ref node.right, ref node, key);
            //deleting
            else
            {
                //если оба потомка нулевые
                if (node.left == null && node.right == null)
                {
                    //и удаляем не корень
                    if (parent != node)
                    {
                        if (parent.left == node)
                            parent.left = null;
                        else
                            parent.right = null;
                    }
                    node = null;
                }
                else
                {
                    Node newnode = null;
                    //если есть только один потомок
                    if (
                        (node.left != null && node.right == null) ||
                        (node.left == null && node.right != null)
                        )
                    {
                        //если только левый
                        if (node.left != null && node.right == null)
                            //newnode = Rightmost(ref node.left);
                            node = node.left;
                        //если только правый
                        else
                            //newnode = Leftmost(ref node.right);
                            node = node.right;

                        //если один потомок и удаляем не корень
                        //if (parent != node)
                        //{
                        //    if (parent.left == node)
                        //        parent.left = newnode;
                        //    else
                        //        parent.right = newnode;

                        //    newnode.right = node.right;
                        //    newnode.left = node.left;
                        //    node = null;
                        //}
                        ////если один потомок и удаляем корень
                        //else
                        //{
                        //    if (node.left != null)
                        //        node = node.left;
                        //    else
                        //        node = node.right;
                        //}
                    }
                    //если есть оба потомка
                    else
                    {

                        newnode = node.left;
                        Node pmin = node.right;
                        while (pmin.left != null)
                        {
                            pmin = pmin.left;
                        }
                        pmin.left = node.left;
                        node = node.right;
                    }
                }  
            }
        }
        Node Leftmost(ref Node node)
        {
            if (node == null)
                return null;
            if (node.left!= null)
            {
                return Leftmost(ref node.left);
            }
            return node;
        }
        Node Rightmost(ref Node node)
        {
            if (node == null)
                return null;
            if (node.right!= null)
            {
                return Rightmost(ref node.right);
            }
            return node;
        }
    }
}
