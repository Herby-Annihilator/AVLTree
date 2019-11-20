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
        public class Node<T>
        {
            public T Data { get; set; }
            public int Key { get; set; }
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
                Key = 0;
            }
            public Node(T data = default(T), int key = 0, Node<T> leftChild = null, Node<T> rightChild = null, string trace = "", int balanceFactor = 0)
            {
                Data = data;
                Key = key;
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
        public Node<T> Root;

        /// <summary>
        /// Высота данного дерева
        /// </summary>
        private int height;
        public int Height
        {
            get { return height; }
        }
        //
        // Конструктор
        //
        public AVLTree()
        {
            Root = null;
            height = 0;
        }

        /// <summary> 
        /// Добавляет элемент с заданным ключом в дерево
        /// как ключ используется поле key
        /// </summary>
        public void Add(T data = default(T), int key = 0)
        {          
            if (Root == null)
            {
                Root = new Node<T>(data, key);
                height++;
                return;
            }
            Node<T> current = Root;
            // добавить слева
            Add(Root, data, key);

        }
        /// <summary>
        /// Для использования внутри основного метода Add.
        /// Используется рекурсия
        /// </summary>
        /// <param name="currentNode">следующий узел для обработки</param>
        /// <param name="data">ваши данные</param>
        /// <param name="key">ключ</param>
        private Node<T> Add(Node<T> currentNode, T data, int key)
        {
            if (currentNode != null)
            {
                if (key < currentNode.Key)
                {
                    currentNode.LeftChild = Add(currentNode.LeftChild, data, key);
                }
                else
                {
                    currentNode.RightChild = Add(currentNode.RightChild, data, key);
                }
            }
            else
            {
                Node<T> node = new Node<T>(data, key);
                return node;
            }
            return currentNode;
        }
        /// <summary> 
        /// Поворачивает дерево вокруг заданного узла влево.
        /// Вернет ссылку на нового потомка для родительского
        /// узла (вызывает родительский узел).
        /// </summary>
        public Node<T> LeftRotation(Node<T> currentNode)
        {
            if (currentNode.RightChild == null)
                return currentNode;
            Node<T> toReturn = currentNode.RightChild;
            currentNode.RightChild = toReturn.LeftChild;
            toReturn.LeftChild = currentNode;
            return toReturn;
        }
        /// <summary> 
        /// Поворачивает дерево вокруг заданного узла вправо.
        /// Вернет ссылку на нового потомка для родительского
        /// узла (вызывает родительский узел).
        /// </summary>
        public Node<T> RightRotation(Node<T> currentNode)
        {
            if (currentNode.LeftChild == null)
                return currentNode;
            Node<T> toReturn = currentNode.LeftChild;
            currentNode.LeftChild = toReturn.RightChild;
            toReturn.RightChild = currentNode;
            return toReturn;
        }
    }
}
