using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgLab7
{
    class Subroutines
    {
        /// <summary>
        /// Печатает меню 
        /// </summary>
        /// <returns></returns>
        public static char PrintMenu()
        {
            char symbol;
            do
            {
                Console.Clear();
                Console.WriteLine("* c - создать дерево и заполнить его случайными величинами");
                Console.WriteLine("* b - добавить элементы в дерево вручную");
                Console.WriteLine("* p - показать дерево (сделать обход, без связей)");
                Console.WriteLine("* r - показать таблицу связей дерева");
                Console.WriteLine("* h - Получить высоту дерева");
                Console.WriteLine("* v - получить информацию о корне");
                Console.WriteLine("* ESC - выход");
                Console.Write("Ваш выбор - ");
                symbol = Convert.ToChar(Console.ReadKey(true).KeyChar);
            } while (symbol != 'c' && symbol != 'b' && symbol != 'p' && symbol != 'r' && symbol != 'h' && symbol != 'v' && symbol != 27);
            return symbol;
        }
        //
        // Сохранить в input.dat
        //
        public static void SaveTreeInFile(AVLTree<int> tree, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            
            writer.Close();
        }
        //
        // Переписать из файла в файл
        //
        /// <summary>
        /// Переписывает содержимое одного файла в другой
        /// </summary>
        /// <param name="fileNameFrom">откуда переписывать</param>
        /// <param name="fileNameTo">куда переписывать</param>
        public static void WriteFromFileToFile(string fileNameFrom, string fileNameTo)
        {
            if (fileNameTo == fileNameFrom)
            {
                throw new Exception("Невозможно переписать из файла в тот же файл");
            }
            StreamReader reader = new StreamReader(fileNameFrom);
            if (reader == null)
            {
                throw new Exception("Ошибка открытия файла для чтения");
            }
            StreamWriter writer = new StreamWriter(fileNameTo);
            if (writer == null)
            {
                throw new Exception("Ошибка открытия файла для записи");
            }
            string toRewrite;
            while ((toRewrite = reader.ReadLine()) != null)
            {
                writer.WriteLine(toRewrite);
            }
            writer.Close();
            reader.Close();
        }
        //
        // Дополнить содержание одного файла содержаением другого
        //
        /// <summary>
        /// Из первого файла дописывает данные во второй файл
        /// </summary>
        /// <param name="fileNameFrom">откуда переписывать</param>
        /// <param name="fileNameTo">куда переписывать</param>
        public static void AddFromFileToFile(string fileNameFrom, string fileNameTo)
        {
            if (fileNameTo == fileNameFrom)
            {
                throw new Exception("Невозможно переписать из файла в тот же файл");
            }
            StreamReader reader = new StreamReader(fileNameFrom);
            StreamWriter writer = new StreamWriter(fileNameTo, true);
            string toRewrite;
            while ((toRewrite = reader.ReadLine()) != null)
            {
                writer.WriteLine(toRewrite);
            }
            writer.Close();
            reader.Close();
        }
        //
        // Получить целое число
        //
        public static int GetInt()
        {
            int number;
            string strNum;
            do
            {
                strNum = Console.ReadLine();
            } while (Int32.TryParse(strNum, out number) == false);
            return number;
        }
        /// <summary>
        /// Сравнивает указанные строки-следы. Вернет 0, если эти следы эдентичны,
        /// -1, если искомый след находится в левом поддереве, 1, если искомый
        /// след находится в правом поддереве.
        /// </summary>
        /// <param name="currentTrace">текущий след</param>
        /// <param name="desiredTrace">след, который надо найти</param>
        /// <returns></returns>
        public static int CompareTraces(string currentTrace, string desiredTrace)
        {
            if (currentTrace == null || desiredTrace == null)
                throw new NullReferenceException("Несуществующая строка!");
            else if (currentTrace.Length == 0 || desiredTrace.Length == 0)
                throw new FormatException("Длина одной из строк == 0");
            if (currentTrace.Length > desiredTrace.Length)
                throw new Exception("Текущая строка не может быть длиннее заданной строки");

            int isEquals = 0;
            if (currentTrace == desiredTrace)
                isEquals = 0;              // данные следы идентичны
            else
            {
                if (desiredTrace.Substring(0, currentTrace.Length) == currentTrace)
                {
                    if (desiredTrace[currentTrace.Length + 1] == '1')
                        isEquals = 1;                     // идем вправо
                    else if (desiredTrace[currentTrace.Length] == '0')
                        isEquals = -1;               // идем влево
                }
                else
                    return -2;     // значит такого следа нет вообще
            }
            return isEquals;
        }
    }
}
