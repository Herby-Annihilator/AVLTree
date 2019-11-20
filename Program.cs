/*Удалить ключ с заданным следом из АВЛ-дерева.*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab7
{
    class Program
    {
        static void Main(string[] args)
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Add(15, 20);
            tree.Add(15, 15);
            tree.Add(15, 25);
            tree.Root = tree.LeftRotation(tree.Root);
        }
    }
}
