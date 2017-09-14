using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    //Naive implementation
    class DisjointSets
    {
        int[] smallest;

        public DisjointSets(int[] arr)
        {
            smallest = new int[arr.Length+1];
        }

        public void MakeSet(int i)
        {
            smallest[i] = i;
        }

        public int Find(int i)
        {
            return smallest[i];
        }

        // Rule : Using smalest element of a set as its Id
        public void Union(int i, int j)
        {
            int i_id = Find(i); // id of the set where i is located
            int j_id = Find(j); // id of set where j is located

            if (i_id == j_id) // if both id's are same than they are is same segment so we just return
            {
                return;
            }

            int m = Math.Min(i_id, j_id); // since we are using smallest element we just find the min between two id's

            for (int k = 1; k < smallest.Length; k++) // scan through array
            {
                if (smallest[k] == i_id || smallest[k] == j_id) // set the same id for i_id and j_id so now they are in same set
                {
                    smallest[k] = m;
                }
            }
        }
    }

    // Union by rank implementation O(log n)
    class DisjointSetUBR
    {
        int[] parent;
        int[] rank; // height of tree

        public DisjointSetUBR()
        {
            parent = new int[61];
            rank = new int[61];
        }

        public DisjointSetUBR(int[] arr)
        {
            parent = new int[arr.Length +1];
            rank = new int[arr.Length + 1];
        }

        public void MakeSet(int i)
        {
            parent[i] = i;
            //rank[i] = 0; not needed in C# 
        }

        public int Find(int i)
        {
            while (i!=parent[i]) // If i is not root of tree we set i to his parent until we reach root (parent of all parents)
            {
                i = parent[i]; 
            }
            return i;
        }

        // Path compression, O(log*n). For practical values of n, log* n <= 5
        public int FindPath(int i)
        {
            if (i!=parent[i])
            {
                parent[i] = FindPath(parent[i]);
            }
            return parent[i];
        }

        public void Union(int i, int j)
        {
            int i_id = Find(i); // Find the root of first tree (set) and store it in i_id
            int j_id = Find(j); // // Find the root of second tree (set) and store it in j_id

            if (i_id == j_id) // If roots are equal (they have same parents) than they are in same tree (set)
            {
                return;
            }

            if (rank[i_id] > rank[j_id]) // If height of first tree is larger than second tree
            {
                parent[j_id] = i_id; // We hang second tree under first, parent of second tree is same as first tree
            }
            else
            {
                parent[i_id] = j_id; // We hang first tree under second, parent of first tree is same as second tree
                if (rank[i_id] == rank[j_id]) // If heights are same
                {
                    rank[j_id]++; // We hang first tree under second, that means height of tree is incremented by one
                }
            }
        }
    }
}