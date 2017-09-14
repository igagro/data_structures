using System;

namespace DataStructures
{
    class AVLTree
    {
        private Node root;
        private int balance;
        static bool taller;
        static bool shorter;

        public AVLTree()
        {
            root = null;
            //balance = 0;
        }

        void Insert(int x)
        {
            root = Insert(root, x);
        }

        Node Insert(Node node, int x)
        {
            if (node==null)
            {
                node = new Node();
                node.key = x;
                node.balance = 0;
                taller = true;
            }
            else if (x < node.key)
            {
                node.leftChild = Insert(node.leftChild, x);
                if (taller == true)
                {
                    node = InsertionLeftSubtreeCheck(node);
                }
            }
            else if (x > node.key)
            {
                node.rightChild = Insert(node.rightChild, x);
                if (taller == true)
                {
                    node = InsertionRightSubtreeCheck(node);
                }
            }
            return node;
        }

        Node InsertionLeftSubtreeCheck(Node node)
        {
            switch (node.balance)
            {
                case 0: // Case L1 : was balanced
                    node.balance = 1; // now left heavy
                    break;
                case -1: // Case L2 : was right heavy
                    node.balance = 0; // now balanced
                    taller = false;
                    break;
                case 1: // Case L3 : was left heavy
                    node = InsertionLeftBalance(node); // Left balancing
                    taller = false;
                    break;
            }
            return node;
        }

        Node InsertionRightSubtreeCheck(Node node)
        {
            switch (node.balance)
            {
                case 0: // Case R1: was balanced
                    node.balance = -1; // noew right heavy
                    break;
                case 1: // Case R2 : was left heavy
                    node.balance = 0; // now balanced
                    taller = false;
                    break;
                case -1: // Case R3: Right heavy
                    node = InsertionRightBalance(node); // Right balancing
                    taller = false;
                    break;
                default:
                    break;
            }
            return node;
        }

        Node InsertionLeftBalance(Node node)
        {
            Node a, b;

            a = node.leftChild;
            if (a.balance == 1) // Case L3A : Insertion in AL
            {
                node.balance = 0;
                a.balance = 0;
                node = RotateRight(node);
            }
            else // Case L3B : Insertion in AR
            {
                b = a.rightChild;
                switch (b.balance)
                {
                    case 1: // Case L3B2 : Insertion in BL
                        node.balance = -1;
                        a.balance = 0;
                        break;
                    case -1: // Case L3B2 : Insertion in BR
                        node.balance = 0;
                        a.balance = 1;
                        break;
                    case 0: // Case
                        node.balance = 0;
                        a.balance = 0;
                        break;
                    default:
                        break;
                }
                b.balance = 0;
                node.leftChild = RotateLeft(a);
                node = RotateRight(node);
            }
            return node;
        }

        Node InsertionRightBalance(Node node)
        {
            Node a, b;

            a = node.rightChild;
            if (a.balance == -1)
            {
                node.balance = 0;
                a.balance = 0;
                node = RotateLeft(node);
            }
            else
            {
                b = a.leftChild;
                switch (b.balance)
                {
                    case -1: // Insertion in BR
                        node.balance = 1;
                        a.balance = 0;
                        break;
                    case 1: // Insertion in BL
                        node.balance = 0;
                        a.balance = -1;
                        break;
                    case 0: // B is the newly inserted node
                        node.balance = 0;
                        a.balance = 0;
                        break;
                    default:
                        break;
                }
                b.balance = 0;
                node.rightChild = RotateRight(a);
                node = RotateLeft(node);
            }
            return node;
        }

        Node RotateRight(Node node)
        {
            var a = node.leftChild;
            node.leftChild = a.rightChild;
            a.rightChild = node;
            return a;
        }

        Node RotateLeft(Node node)
        {
            var a = node.rightChild;
            node.rightChild = a.leftChild;
            a.leftChild = node;
            return a;
        }

        //
        // Delete
        //

        void Delete(int x)
        {
            root = Delete(root, x);
        }

        Node Delete(Node node, int x)
        {
            Node ch, s;

            if (node == null)
            {
                shorter = false;
                return node;
            }

            if (x < node.key) // delete from left subtree
            {
                node.leftChild = Delete(node.leftChild, x);
                if (shorter == true)
                {
                    node = DeletionLeftSubtreeCheck(node);
                }
            }
            else if (x > node.key) // Delete from right subtree
            {
                node.rightChild = Delete(node.rightChild, x);
                if (shorter == true)
                {
                    node = DeletionRightSubtreeCheck(node);
                }
            }
            else
            {
                // key to be deleted is find
                if (node.leftChild != null && node.rightChild != null) // 2 children
                {
                    s = node.rightChild;
                    while (s.leftChild != null)
                    {
                        s = s.leftChild;
                    }
                    node.key = s.key;
                    node.rightChild = Delete(node.rightChild, s.key);
                    if (shorter == true)
                    {
                        node = DeletionRightSubtreeCheck(node);
                    }
                }
                else // one child or no child
                {
                    if (node.leftChild != null) // only left child
                    {
                        ch = node.leftChild;
                    }
                    else // only right child or no child
                    {
                        ch = node.rightChild;
                    }
                    node = ch;
                    shorter = true;
                }
            }
            return node;
        }

        Node DeletionLeftSubtreeCheck(Node node)
        {
            switch (node.balance)
            {
                case 0: // was balanced
                    node.balance = -1; // now right heavy
                    shorter = false;
                    break;
                case 1: // was left heavy
                    node.balance = 0; // now balanced
                    break;
                case -1: // was right heavy
                    node = DeletionRightBalance(node); // Right balancing
                    break;
                default:
                    break;
            }
            return node;
        }

        Node DeletionRightSubtreeCheck(Node node)
        {
            switch (node.balance)
            {
                case 0: // was balanced
                    node.balance = 1; // now left heavy
                    shorter = false;
                    break;
                case -1: // was right heavy
                    node.balance = 0; // now balanced
                    break;
                case 1: // was left heavy
                    node = DeletionLeftBalance(node); // Left balancing
                    break;
                default:
                    break;
            }
            return node;
        }

        Node DeletionRightBalance(Node node)
        {
            Node a, b;

            a = node.rightChild;
            if (a.balance == 0)
            {
                a.balance = 1;
                shorter = false;
                node = RotateLeft(node);
            }
            else if (a.balance == -1)
            {
                node.balance = 0;
                a.balance = 0;
                node = RotateLeft(node);
            }
            else
            {
                b = a.leftChild;
                switch (b.balance)
                {
                    case 0:
                        node.balance = 0;
                        a.balance = 0;
                        break;
                    case 1:
                        node.balance = 0;
                        a.balance = -1;
                        break;
                    case -1:
                        node.balance = 1;
                        a.balance = 0;
                        break;
                    default:
                        break;
                }

                b.balance = 0;
                node.rightChild = RotateRight(a);
                node = RotateLeft(node);
            }
            return node;
        }

        Node DeletionLeftBalance(Node node)
        {
            Node a, b;

            a = node.leftChild;
            if (a.balance == 0)
            {
                a.balance = -1;
                shorter = false;
                node = RotateRight(node);
            }
            else if (a.balance == 1)
            {
                node.balance = 0;
                a.balance = 0;
                node = RotateRight(node);
            }
            else
            {
                b = a.rightChild;
                switch (b.balance)
                {
                    case 0:
                        node.balance = 0;
                        a.balance = 0;
                        break;
                    case -1:
                        node.balance = 0;
                        a.balance = 1;
                        break;
                    case 1:
                        node.balance = -1;
                        a.balance = 0;
                        break;
                    default:
                        break;
                }

                b.balance = 0;
                node.leftChild = RotateLeft(a);
                node = RotateRight(node);
            }
            return node;
        }
    }

    class Node
    {
        public int key;
        public Node leftChild;
        public Node rightChild;
        public Node parent;
        public int balance;
    }
}