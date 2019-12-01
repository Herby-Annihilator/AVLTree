/*Удалить ключ с заданным следом из АВЛ-дерева.*/


using System;
using System.IO;

namespace AlgLab7
{
    class Program
    {
        static void Main(string[] args)
        {
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
                                } while (!Int32.TryParse(Console.ReadLine(), out numberOfNodes));
                            } while (numberOfNodes < 1 || numberOfNodes > 100);
                            Random random = new Random();
                            for (int i = 0; i < numberOfNodes; i++)
                                avlTree.Add(random.Next(-50, 50), random.Next(-50, 50));
                            random = null;
                            Subroutines.SaveTreeInFile(avlTree, "input.dat");
                            StreamWriter writer = new StreamWriter("output.dat", true);
                            writer.WriteLine("===============================================================================");
                            writer.WriteLine("После добавления узлов получилось дерево следующего вида\n");
                            writer.WriteLine("===============================================================================");
                            writer.Close();
                            Subroutines.AddFromFileToFile("input.dat", "output.dat");
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
                                } while (!Int32.TryParse(Console.ReadLine(), out numberOfNodes));
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
                            StreamWriter writer = new StreamWriter("output.dat", true);
                            writer.WriteLine("===============================================================================");
                            writer.WriteLine("После добавления узлов получилось дерево следующего вида\n");
                            writer.WriteLine("===============================================================================");
                            writer.Close();
                            Subroutines.AddFromFileToFile("input.dat", "output.dat");
                            Console.WriteLine("\nДобавление прошло успешно, нажмите что-нибудь!");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // * d - удалить узел с заданным следом
                    //
                    case 'd':
                        {
                            string trace;
                            do
                            {
                                Console.Write("Введите интересующий вас след: ");
                                trace = Console.ReadLine();
                            } while (!Subroutines.IsTrace(trace));

                            try
                            {
                                avlTree.DeleteThisTrace(trace);
                            }
                            catch(NullReferenceException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("\n===========Нажмите что-нибудь===========");
                                Console.ReadKey();
                            }
                            catch(FormatException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("\n===========Нажмите что-нибудь===========");
                                Console.ReadKey();
                            }
                            catch(InvalidOperationException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("\n===========Нажмите что-нибудь===========");
                                Console.ReadKey();
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("\n===========Нажмите что-нибудь===========");
                                Console.ReadKey();
                            }
                            StreamWriter writer = new StreamWriter("output.dat", true);
                            writer.WriteLine("===============================================================================");
                            writer.WriteLine("После удаления узла со следом " + trace + " получилось дерево следующего вида\n");
                            writer.WriteLine("===============================================================================");
                            writer.Close();
                            try
                            {
                                Subroutines.AddLinksTableToFile(avlTree, "output.dat");
                            }
                            catch(FileNotFoundException e)
                            {
                                Console.WriteLine(e.Message);
                                Console.WriteLine("\n===========Нажмите что-нибудь===========");
                                Console.ReadKey();
                            }
                            Console.WriteLine("Удаление прошло успешно. Нажмите что-нибудь");
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
                        else
                        {
                            Console.WriteLine("\n===========Нажмите что-нибудь===========");
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
                        else
                        {
                            Console.WriteLine("\n===========Нажмите что-нибудь===========");
                            Console.ReadKey();
                        }
                        break;
                    //
                    // h - Получить высоту дерева
                    //
                    case 'h':
                        Console.WriteLine(" Высота данного дерева = " + avlTree.Height);
                        Console.ReadKey();
                        break;
                    //
                    // v - получить информацию о корне
                    //
                    case 'v':
                        Console.WriteLine("\n");
                        avlTree.GetRootInfo();
                        Console.ReadKey();
                        break;
                    //
                    // * n - установить правильные следы в дереве
                    //
                    case 'n':
                        {
                            if (avlTree.PutTrace() == false)
                            {
                                Console.WriteLine("Невозможно восстановить следы");
                                Console.WriteLine("\n===========Нажмите что-нибудь===========");
                                Console.ReadKey();
                            }
                            break;
                        }
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
