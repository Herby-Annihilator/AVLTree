/*Удалить ключ с заданным следом из АВЛ-дерева.*/

// by Rukin Danil (AltSTU, PI-81)
// Rukin2018D@yandex.ru


using System;
using System.Collections.Generic;

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
            public bool IsLeaf()
            {
                if (this.LeftChild == null && this.RightChild == null)
                    return true;
                return false;
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
            Root = Add(Root, data, key);
            PutTrace(Root, "1");
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
                            currentNode = RightRotation(currentNode);
                        else if (currentNode.LeftChild.BalanceFactor == 1)
                        {
                            currentNode = LeftRightRotation(currentNode);
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
                            currentNode = LeftRotation(currentNode);
                        else if (currentNode.RightChild.BalanceFactor == -1)
                        {
                            currentNode = RightLeftRotation(currentNode);
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
            int leftHeight = 0;
            int rightHeight = 0;
            if (startNode != null)
            {
                height = 1;
                if (startNode.LeftChild != null)
                {
                    leftHeight += GetRelativeHeight(startNode.LeftChild);
                }
                if (startNode.RightChild != null)
                {
                    rightHeight += GetRelativeHeight(startNode.RightChild);
                }
            }
            if (leftHeight > rightHeight)
                return leftHeight + height;
            else
                return rightHeight + height;
        }

        /// <summary> 
        /// Поворачивает дерево вокруг заданного узла влево.
        /// Вернет ссылку на нового потомка для родительского
        /// узла (вызывает родительский узел).
        /// </summary>
        public Node<T> LeftRotation(Node<T> currentNode)
        {
            if (currentNode.RightChild == null)
            {
                currentNode.BalanceFactor = 0;
                return currentNode;
            }
            Node<T> toReturn = currentNode.RightChild;
            currentNode.RightChild = toReturn.LeftChild;
            toReturn.LeftChild = currentNode;
            currentNode.BalanceFactor = 0;
            toReturn.BalanceFactor = 0;
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
            {
                currentNode.BalanceFactor = 0;
                return currentNode;
            }
            Node<T> toReturn = currentNode.LeftChild;
            currentNode.LeftChild = toReturn.RightChild;
            toReturn.RightChild = currentNode;
            currentNode.BalanceFactor = 0;
            toReturn.BalanceFactor = 0;
            return toReturn;
        }
        /// <summary>
        /// Лево-правый поворот (большой правый)
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public Node<T> LeftRightRotation(Node<T> currentNode)
        {
            if (currentNode == null)
            {
                return null;
            }
            Node<T> leftChildOfCurrentNode = currentNode.LeftChild;
            Node<T> toReturn = leftChildOfCurrentNode.RightChild;
            if (toReturn.BalanceFactor > 0)
            {
                leftChildOfCurrentNode.BalanceFactor = -1;
            }
            else
            {
                leftChildOfCurrentNode.BalanceFactor = 0;
            }
            if (toReturn.BalanceFactor < 0)
            {
                currentNode.BalanceFactor = 1;
            }
            else
            {
                currentNode.BalanceFactor = 0;
            }
            leftChildOfCurrentNode.RightChild = toReturn.LeftChild;
            toReturn.LeftChild = leftChildOfCurrentNode;

            currentNode.LeftChild = toReturn.RightChild;
            toReturn.RightChild = currentNode;

            return toReturn;
        }
        /// <summary>
        /// Право-левый поворот (большой левый)
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        public Node<T> RightLeftRotation(Node<T> currentNode)
        {
            if (currentNode == null)
                return null;
            Node<T> rightChildOfCurrentNode = currentNode.RightChild;
            Node<T> toReturn = rightChildOfCurrentNode.LeftChild;
            if (toReturn.BalanceFactor > 0)
                currentNode.BalanceFactor = -1;
            else
                currentNode.BalanceFactor = 0;
            if (toReturn.BalanceFactor < 0)
                rightChildOfCurrentNode.BalanceFactor = 1;
            else
                rightChildOfCurrentNode.BalanceFactor = 0;

            rightChildOfCurrentNode.LeftChild = toReturn.RightChild;
            toReturn.RightChild = rightChildOfCurrentNode;

            currentNode.RightChild = toReturn.LeftChild;
            toReturn.LeftChild = currentNode;

            return toReturn;
        }
        public void PutRightBalance(Node<T> currentNode)
        {
            if (currentNode == null)
                return;
            currentNode.BalanceFactor = GetRelativeHeight(currentNode.RightChild) - GetRelativeHeight(currentNode.LeftChild);
            PutRightBalance(currentNode.LeftChild);
            PutRightBalance(currentNode.RightChild);
        }
        /// <summary>
        /// Устанавливает след для всех узлов дерева, начиная с заданного
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
            currentNode.Trace = nodeTrace;
        }
        /// <summary>
        /// Устанавливает след для всех узлов дерева
        /// </summary>
        /// <returns></returns>
        public bool PutTrace()
        {
            if (Root == null)
                return false;
            PutTrace(Root, "1");
            return true;
        }
        /// <summary>
        /// Выводит иноформацию о корне данного дерева
        /// </summary>
        public void GetRootInfo()
        {
            Console.WriteLine("==========Корень данного дерева==========\n");
            Console.WriteLine("Значение ключа = " + Root.Key + " Данные = " + Root.Data);
            if (Root.LeftChild.IsExist())
                Console.WriteLine("Потомок слева существует: ключ = " + Root.LeftChild.Key + " Данные = " + Root.LeftChild.Data);
            else
                Console.WriteLine("Потомков слева нет");
            if (Root.RightChild.IsExist())
                Console.WriteLine("Потомок справа существует: ключ = " + Root.RightChild.Key + " Данные = " + Root.RightChild.Data);
            else
                Console.WriteLine("Потомков справа нет");
        }
        /// <summary>
        /// Удаляет узел с заданным следом. Выкидывает исключения.
        /// </summary>
        /// <param name="trace"></param>
        public bool DeleteThisTrace(string trace)
        {
            if (Root == null)
                return false;
            Root = DeleteThisTrace(Root, trace);
            //
            // Здесь можно вставить переустановку следов узлов
            //
            return true;
        }
        /// <summary>
        /// Удаляет узел с заданным следом, только для использования внутри основного метода Delete.....().
        /// Возвращает ссылку на новый узел (старый удаляется)
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="trace"></param>
        /// <returns></returns>
        private Node<T> DeleteThisTrace(Node<T> currentNode, string trace)
        {
            if (currentNode != null)
            {
                int path = Subroutines.CompareTraces(currentNode.Trace, trace);
                if (path == -1)
                {
                    currentNode.LeftChild = DeleteThisTrace(currentNode.LeftChild, trace);
                }
                else if (path == 1)
                {
                    currentNode.RightChild = DeleteThisTrace(currentNode.RightChild, trace);
                }
                else if (path == -2)       // здесь по-хорошему бы стек почистить да выйти из метода
                    return currentNode;
                else if (path == 0)
                {
                    if (currentNode.RightChild == null)
                        currentNode = currentNode.LeftChild;     // здесь можно поставить оператор return
                    else
                    {
                        Node<T> currentNodeLeftChild = currentNode.LeftChild;
                        Node<T> currentNodeRightChild = FindMinNode(currentNode.RightChild, ref currentNode); // в currentNode окажется минимальный элемент из правого поддерева
                        currentNode.LeftChild = currentNodeLeftChild;
                        currentNode.RightChild = currentNodeRightChild;
                    }
                }
            }
            else
                throw new InvalidOperationException("Узел с таким следом не найден");
            currentNode = Balance(currentNode);
            return currentNode;
        }
        /// <summary>
        /// Вернет минимальный элемент в дереве, начиная от заданного узла, а также удалит его из его изначального места.
        /// </summary>
        /// <param name="currentNode">точка отсчета</param>
        /// <returns></returns>
        private Node<T> FindMinNode(Node<T> currentNode, ref Node<T> minNode)
        {
            if (currentNode.LeftChild != null)
            {
                currentNode.LeftChild = FindMinNode(currentNode.LeftChild, ref minNode);
                currentNode.BalanceFactor = GetRelativeHeight(currentNode.RightChild) - GetRelativeHeight(currentNode.LeftChild);
                if (currentNode.BalanceFactor == -2)
                {
                    if (currentNode.LeftChild.BalanceFactor == -1)
                        currentNode = RightRotation(currentNode);
                    else if (currentNode.LeftChild.BalanceFactor == 1)
                        currentNode = LeftRightRotation(currentNode);
                }
                else if (currentNode.BalanceFactor == 2)       // сначала не дописал вот этот фрагмент
                {
                    if (currentNode.RightChild.BalanceFactor == -1)
                        currentNode = RightLeftRotation(currentNode);
                    else if (currentNode.RightChild.BalanceFactor == 1)
                        currentNode = LeftRotation(currentNode);
                }
            }
            else
            {
                minNode = currentNode;
                Node<T> toReturn = currentNode.RightChild;
                currentNode = null;
                return toReturn;
            }
            return currentNode;
        }
        /// <summary>
        /// Балансирует авл-дерево относительно заданного узла, но не все дерево.
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private Node<T> Balance(Node<T> currentNode)
        {
            if (currentNode == null)
                return currentNode;
            currentNode.BalanceFactor = GetRelativeHeight(currentNode.RightChild) - GetRelativeHeight(currentNode.LeftChild);
            if (currentNode.BalanceFactor > 2 || currentNode.BalanceFactor < -2)
                throw new InvalidOperationException("Ошибка балансировки");
            if (currentNode.BalanceFactor == -2)
            {
                if (currentNode.LeftChild.BalanceFactor == -1 || currentNode.LeftChild.BalanceFactor == 0)
                    currentNode = RightRotation(currentNode);
                else
                    currentNode = LeftRightRotation(currentNode);
            }
            else if (currentNode.BalanceFactor == 2)
            {
                if (currentNode.RightChild.BalanceFactor == 1 || currentNode.RightChild.BalanceFactor == 0)
                    currentNode = LeftRotation(currentNode);
                else
                    currentNode = RightLeftRotation(currentNode);
            }
            return currentNode;
        }
        /// <summary>
        /// Вид дерева при обходе по принципу лево-корень-право
        /// </summary>
        /// <returns></returns>
        public bool ShowTree()
        {
            if (this.Root == null)
                return false;
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> currentNode = Root;
            Console.WriteLine("================Вид дерева при обходе по принципу лево-корень-право==================\n");
            GetRootInfo();
            Console.WriteLine("\n");
            while (!(currentNode == null && stack.Count == 0))
            {
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = stack.Pop();
                    Console.Write(currentNode.Key + " // ");
                    currentNode = currentNode.RightChild;
                }
            }
            return true;
        }
        /// <summary>
        /// Покажет таблицу ссылок в данном экземпляре авл-дерева
        /// </summary>
        /// <returns></returns>
        public bool ShowTreeLinks()
        {
            if (this.Root == null)
                return false;
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> currentNode = Root;
            Console.WriteLine("\n");
            GetRootInfo();
            Console.WriteLine("\n");
            Console.WriteLine("================Таблица ссылок в данном экземпляре авл-дерева==================\n");
            Console.WriteLine("| Значение в узле + след|  Левый потомок|  Правый потомок|\n");
            while (!(currentNode == null && stack.Count == 0))
            {
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = stack.Pop();
                    Console.Write("  " + currentNode.Key + " \t" + "\"" + currentNode.Trace + "\"" + " \t\t");
                    if (currentNode.LeftChild != null)
                        Console.Write("  " + currentNode.LeftChild.Key + " \t" + "\"" + currentNode.LeftChild.Trace + "\"" + " \t\t");
                    else
                        Console.Write("\t нет\t\t");
                    if (currentNode.RightChild != null)
                        Console.Write("  " + currentNode.RightChild.Key + " \t" + "\"" + currentNode.RightChild.Trace + "\"" + " \n\n");
                    else
                        Console.Write("\t нет\n\n");
                    currentNode = currentNode.RightChild;
                }
            }
            return true;
        }
    }
}
