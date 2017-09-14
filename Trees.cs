using System;
using System.Collections.Generic;

namespace DataStructures
{
    class Node
    {
        public int item;
        public Node leftc;
        public Node rightc;
    }

    class Tree
    {
        public Node root;
        public Tree()
        {
            root = null;
        }
        public Node ReturnRoot()
        {
            return root;
        }
        public void Insert(int id)
        {
            Node newNode = new Node();
            newNode.item = id;
            if (root == null)
            {
                root = newNode;
            }                
            else
            {
                Node current = root;
                Node parent;
                while (true)
                {
                    parent = current;
                    if (id < current.item)
                    {
                        current = current.leftc;
                        if (current == null)
                        {
                            parent.leftc = newNode;
                            return;
                        }
                    }
                    else
                    {
                        current = current.rightc;
                        if (current == null)
                        {
                            parent.rightc = newNode;
                            return;
                        }
                    }
                }
            }
        }
        public int Height(Node tree)
        {
            if (tree==null)
            {
                return 0;
            }
            else
            {
                return 1 + Math.Max(Height(tree.leftc), Height(tree.rightc));
            }
        }

        public void InOrderTraversal(Node tree)
        {
            if (tree==null)
            {
                return;
            }
            InOrderTraversal(tree.leftc);
            Console.Write(tree.item + " ");
            InOrderTraversal(tree.rightc);
        }

        public void PreOrderTraversal(Node tree)
        {
            if (tree==null)
            {
                return;
            }
            Console.Write(tree.item + " ");
            PreOrderTraversal(tree.leftc);
            PreOrderTraversal(tree.rightc);
        }

        public void PostOrderTraversal(Node tree)
        {
            if (tree==null)
            {
                return;
            }
            PostOrderTraversal(tree.leftc);
            PostOrderTraversal(tree.rightc);
            Console.Write(tree.item + " ");
        }

        public void LevelTraversal(Node tree)
        {
            if (tree==null)
            {
                return;
            }
            Node node = new Node();
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(tree);
            while (q.Count!=0)
            {
                node = q.Dequeue();
                Console.Write(node.item + " ");
                if (node.leftc!=null)
                {
                    q.Enqueue(node.leftc);
                }
                if (node.rightc!=null)
                {
                    q.Enqueue(node.rightc);
                }
            }
        }
    }
}