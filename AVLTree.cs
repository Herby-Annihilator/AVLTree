/*Удалить ключ с заданным следом из АВЛ-дерева.*/

// by Rukin Danil (AltSTU, PI-81)
// Rukin2018D@yandex.ru


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab7
{
    class AVLTree<T>
    {
        //
        // Сруктура узла АВЛ-дерева
        //
        /// <summary>
        /// Узел дерева
        /// </summary>
        /// <typeparam name="T"></typeparam>
        protected class Node<T>
        {
            private T Data { get; set; }
            public Node<T> LeftChild { get; set; }
            public Node<T> RightChild { get; set; }
            public string Trace { get; set; }
            public int BalanceFactor { get; set; }

            public Node()
            {
                Data = default(T);
                LeftChild = null;
                RightChild = null;
                Trace = "";
                BalanceFactor = 0;
            }
            public Node(T data = default(T), Node<T> leftChild = null, Node<T> rightChild = null, string trace = "", int balanceFactor = 0)
            {
                Data = data;
                LeftChild = leftChild;
                RightChild = rightChild;
                Trace = trace;
                BalanceFactor = balanceFactor;
            }
        }
        //
        // Описание самого дерева
        //

        /// <summary>
        /// Ссылка на корень дерева
        /// </summary> 
        protected Node<T> Root = new Node<T>();

        /// <summary>
        /// Высота данного дерева
        /// </summary>
        private int height;
        public int Height
        {
            get { return height; }
        }

    }
}
