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
            /// <summary>
            /// Проверяет узел на существование
            /// </summary>
            /// <returns>true в случае существования</returns>
            public bool IsExist()
            {
                if (this == null)
                    return false;
                return true;
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
        /// Вернет высоту дерева
        /// </summary>
        public int Height
        {
            get { return GetRelativeHeight(Root); }
        }
        //
        // Конструктор
        //
        public AVLTree()
        {
            Root = null;
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
                    currentNode.BalanceFactor = GetRelativeHeight(currentNode.RightChild) - GetRelativeHeight(currentNode.LeftChild);
                    if (currentNode.BalanceFactor == -2)
                    {
                        if (currentNode.LeftChild.BalanceFactor == -1)
                            currentNode = RightRotation(currentNode);   // нужно изменить алгоритм поворота
                        else if (currentNode.LeftChild.BalanceFactor == 1)
                        {
                            // лево-правый поворот
                        }
                    }
                }
                else
                {
                    currentNode.RightChild = Add(currentNode.RightChild, data, key);
                    currentNode.BalanceFactor = GetRelativeHeight(currentNode.RightChild) - GetRelativeHeight(currentNode.LeftChild);
                    if (currentNode.BalanceFactor == 2)
                    {
                        if (currentNode.RightChild.BalanceFactor == 1)
                            currentNode = LeftRotation(currentNode);      // нужно изменить алгоритм поворота
                        else if (currentNode.RightChild.BalanceFactor == -1)
                        {
                            // прово-левый поворот
                        }
                    }
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
        /// Считает относительную выстоту (относительно заданного узла дерева)
        /// если узла не существует, то вернет 0, высота корня = 1
        /// </summary>
        /// <param name="startNode"></param>
        /// <returns></returns>
        private int GetRelativeHeight(Node<T> startNode)
        {
            int height = 0;
            if (startNode != null)
            {
                height = 1;
                if (startNode.LeftChild != null)
                {
                    height += GetRelativeHeight(startNode.LeftChild);
                }
                if (startNode.RightChild != null)
                {
                    height += GetRelativeHeight(startNode.RightChild);
                }
            }
            return height;
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
        /// <summary>
        /// Устанавливает след для всех узлов дерева
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="nodeTrace"></param>
        private void PutTrace(Node<T> currentNode, string nodeTrace)
        {
            if (currentNode == null)
                return;
            else if (currentNode == Root)
                currentNode.Trace = "1";
            if (currentNode.LeftChild != null)
                PutTrace(currentNode.LeftChild, nodeTrace + "0");
            if (currentNode.RightChild != null)
                PutTrace(currentNode.RightChild, nodeTrace + "1");
        }
    }
}
