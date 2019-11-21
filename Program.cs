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
            bool goOut;
            AVLTree<int> avlTree = new AVLTree<int>();
            do
            {
                goOut = false;
                switch (Subroutines.PrintMenu())
                {
                    //
                    // c - создать дерево и заполнить его случайными величинами
                    //
                    case 'c':
                        {
                            int numberOfNodes;
                            do
                            {
                                do
                                {
                                    Console.WriteLine("\nУкажите количество узлов в дереве (не более 100 и не менее 1)");
                                } while (Int32.TryParse(Console.ReadLine(), out numberOfNodes));
                            } while (numberOfNodes < 1 || numberOfNodes > 100);
                            Random random = new Random();
                            for (int i = 0; i < numberOfNodes; i++)
                                avlTree.Add(random.Next(-50, 50), random.Next(-50, 50));
                            random = null;
                            Subroutines.SaveTreeInFile(avlTree, "input.dat");
                            Console.WriteLine("\nДобавление прошло успешно, нажмите что-нибудь!");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // b - добавить элементы в дерево вручную
                    //
                    case 'b':
                        {
                            int numberOfNodes;
                            int nodeKey;
                            do
                            {
                                do
                                {
                                    Console.WriteLine("\nУкажите количество узлов в дереве, которое хотите добавить (не более 10 и не менее 1)");
                                } while (Int32.TryParse(Console.ReadLine(), out numberOfNodes));
                            } while (numberOfNodes < 1 || numberOfNodes > 10);
                            for (int i = 0; i < numberOfNodes; i++)
                            {
                                do
                                {
                                    Console.WriteLine("Число №" + i + " = ");
                                } while (Int32.TryParse(Console.ReadLine(), out nodeKey) == false);
                                avlTree.Add(nodeKey, nodeKey);
                            }
                            Subroutines.SaveTreeInFile(avlTree, "input.dat");
                            Console.WriteLine("\nДобавление прошло успешно, нажмите что-нибудь!");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // p - показать дерево (сделать обход, без связей)
                    //
                    case 'p':
                        if (avlTree.ShowTree() == false)
                        {
                            Console.WriteLine("Дерево пусто! Нажмите что-нибудь");
                            Console.ReadKey();
                        }
                        break;
                    //
                    // r - показать таблицу связей дерева
                    //
                    case 'r':
                        if (avlTree.ShowTreeLinks() == false)
                        {
                            Console.WriteLine("Дерево пусто! Нажмите что-нибудь");
                            Console.ReadKey();
                        }
                        break;
                    //
                    // h - Получить высоту дерева
                    //
                    case 'h':
                        Console.WriteLine("Высота данного дерева = " + avlTree.Height);
                        Console.ReadKey();
                        break;
                    //
                    // v - получить информацию о корне
                    //
                    case 'v':
                        avlTree.GetRootInfo();
                        Console.ReadKey();
                        break;
                    //
                    // ESC - выход
                    //
                    case (char)27:
                        goOut = true;
                        break;
                }
            } while (goOut == false);
        }
    }
}
