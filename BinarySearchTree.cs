using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    class BinarySearchTree
    {
        private Node root;

        public BinarySearchTree()
        {
            root = null;
        }

        Node Find(int k, Node node)
        {
            if (node.key == k)
            {
                return node;
            }
            else if (node.key > k)
            {
                if (node.leftChild != null)
                {
                    return Find(k, node.leftChild);
                }
                return node;
            }
            else if (node.key < k)
            {
                if (node.rightChild != null)
                {
                    return Find(k, node.rightChild);
                }
                return node;
            }
            return node;
        }

        Node Next(Node node)
        {
            if (node.rightChild != null)
            {
                return LeftDescendant(node.rightChild);
            }
            else
            {
                return RightAncestor(node);
            }
        }

        Node LeftDescendant(Node node)
        {
            if (node.leftChild == null)
            {
                return node;
            }
            else
            {
                return LeftDescendant(node.leftChild);
            }
        }

        Node RightAncestor(Node node)
        {
            if (node == null)
            {
                return null;
            }
            if (node.key < node.parent.key)
            {
                return node.parent;
            }
            else
            {
                return RightAncestor(node.parent);
            }
        }

        List<Node> RangeSearch(int x, int y, Node node)
        {
            var list = new List<Node>();
            Node n = Find(x, node);

            while (n.key <= y)
            {
                if (n.key >= x)
                {
                    list.Add(n);
                }
                n = Next(n);
            }
            return list;
        }

        void Insert(int k, Node node)
        {
            var newNode = new Node();
            newNode.key = k;

            if (root == null)
            {
                root = newNode;
            }

            Node p = Find(k, root);

            if (p.key > k)
            {
                p.leftChild = newNode;
                p.leftChild.parent = p;
            }
            if (p.key < k)
            {
                p.rightChild = newNode;
                p.rightChild.parent = p;
            }
        }

        // Recursive
        void Delete(int x)
        {
            root = Delete(root, x);
        }

        Node Delete(Node p, int x)
        {
            Node ch, s;

            if (p == null)
            {
                Console.WriteLine(x + " not found");
                return p;
            }
            if (x < p.key) // Delete from left subtree
            {
                p.leftChild = Delete(p.leftChild, x);
            }
            else if(x > p.key) // Delete from right subtree
            {
                p.rightChild = Delete(p.rightChild, x);
            }
            else // Key to be deleted is found
            {
                if (p.leftChild != null && p.rightChild != null) //two children
                {
                    s = p.rightChild;
                    while (s.leftChild != null)
                    {
                        s = s.leftChild;
                    }
                    p.key = s.key;
                    p.rightChild = Delete(p.rightChild, s.key);
                }
                else // one child or no child
                {
                    if (p.leftChild != null) // only left child
                    {
                        ch = p.leftChild;
                    }
                    else // only right child or no child
                    {
                        ch = p.rightChild;
                    }
                    p = ch; // Copy from next node to deleted node.
                }
            }
            return p;
        }

        //Iterative
        void Delete1(int x)
        {
            Node p = root;
            Node parent = null;

            while (p != null)
            {
                if (x == p.key)
                {
                    break;
                }
                parent = p;
                if (x < p.key)
                {
                    p = p.leftChild;
                }
                else
                {
                    p = p.rightChild;
                }
            }
            if (p == null)
            {
                Console.WriteLine(x + " not found");
                return;
            }

            //Case 2 : two children

            Node s, ps;

            if (p.leftChild != null && p.rightChild != null)
            {
                ps = p;
                s = p.rightChild;

                while (s.leftChild != null)
                {
                    ps = s;
                    s = s.leftChild;
                }
                p.key = s.key;
                p = s;
                parent = ps;
            }

            //Case one or no children

            Node ch;
            if (p.leftChild != null)
            {
                ch = p.leftChild;
            }
            else
            {
                ch = p.rightChild;
            }
            if (parent == null)
            {
                root = ch;
            }
            else if(p == parent.leftChild)
            {
                parent.leftChild = ch;
            }
            else
            {
                parent.rightChild = ch;
            }
        }
    }

    class Node
    {
        public int key;
        public Node leftChild;
        public Node rightChild;
        public Node parent;
    }
}