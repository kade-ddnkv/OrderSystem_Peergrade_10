using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

// Этот файл отвечает за рандомизатор директорий и товаров.

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_
{
    public static class Randomizing
    {
        /// <summary>
        /// Создает массив из случайно созданных директорий и товаров (потом этот массив засовывается в treeView1).
        /// </summary>
        /// <param name="dirCount"></param>
        /// <param name="productCount"></param>
        /// <returns></returns>
        public static TreeNode[] CreateRandomTreeNodeArray(uint dirCount, uint productCount)
        {
            Directory rootDir = new Directory("rootDir");
            Directory selectedDir;

            // Создаю директории.
            for (uint i = 0; i < dirCount; i++)
            {
                // Выбираю рандомную директорию из уже существующих.
                selectedDir = GetRandomExistingDirectory(rootDir);
                // И в ней создаю новую поддиректорию.
                selectedDir.Nodes.Add(new Directory($"dir_{i}"));
            }

            // Расставляю товары по директориям.
            for (uint i = 0; i < productCount; i++)
            {
                // Выбираю рандомную директорию, кроме корневой.
                selectedDir = GetRandomExistingDirectory(rootDir, rootDir);
                // В нее добавляю товар с рандомными значениями (кроме артикула и имени).
                selectedDir.Products.Add(CreateRandomProduct(i));
            }

            return rootDir.Nodes.Cast<Directory>().ToArray();
        }

        /// <summary>
        /// Получает случайную директорию из уже существующих.
        /// </summary>
        /// <param name="rootDir"></param>
        /// <param name="exeptDirectory"></param>
        /// <returns></returns>
        public static Directory GetRandomExistingDirectory(Directory rootDir, Directory exeptDirectory = null)
        {
            // Алгоритм не оптимизирован, но зато настоящий рандом ).
            // Основан на том, что у каждой директории есть вес (кол-во вершин дерева под ней + 1).
            // Т.е. если я стою в одной точке и выбираю дочернюю директорию, у дир. с большим весом будет больший шанс быть выбранной.
            Directory currentDir = rootDir;
            bool directoryIsChosen = false;
            Random rand = new Random();

            // Пока не выберется директория или пока не дойдем до конца текущей ветки.
            while (directoryIsChosen == false && currentDir.Nodes.Count != 0)
            {
                // Создаю список из интов, каждое число соответствует определенной директории.
                // Последнее число = 1 - это текущая директория.
                // Числа по индексам от 0 до числа детей - это вес каждой из директорий-детей.
                List<int> weights = new List<int>();
                weights.AddRange(currentDir.Nodes.Cast<TreeNode>().Select(child => GetDirectoryWeightRecurse(child)));
                if (currentDir != exeptDirectory)
                {
                    weights.Add(1);
                }

                // Выбираю рандомное число от 0 до веса текущей директории.
                // В зависимости от того, в какой промежуток попадет рандомое число, такую директорию выбираю.
                int chosenNumb = rand.Next(weights.Sum());
                int chosenDirIndex = 0;
                int sum = 0;
                for (int i = 0; sum <= chosenNumb; i++)
                {
                    sum += weights[i];
                    // Сумма в какой-то момент превысит выбранное число, записываю индекс, на котором это произошло.
                    if (sum > chosenNumb)
                    {
                        chosenDirIndex = i;
                        break;
                    }
                }
                // Если выбрана текущая директория, выхожу.
                if (chosenDirIndex == weights.Count - 1 && currentDir != exeptDirectory)
                {
                    directoryIsChosen = true;
                }
                else
                {
                    // Иначе продолжю цикл, выбрав одну из поддиректорий.
                    currentDir = currentDir.Nodes[chosenDirIndex] as Directory;
                }
            }

            return currentDir;
        }

        /// <summary>
        /// Получает "вес" директории через рекурсию.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        private static int GetDirectoryWeightRecurse(TreeNode dir)
        {
            // Вес директории = вес дерева под ним + 1 (эта вершина).
            // Вес листка дерева = 1.
            if (dir.Nodes.Count == 0)
            {
                return 1;
            }
            return 1 + dir.Nodes.Cast<TreeNode>().Select(subdir => GetDirectoryWeightRecurse(subdir)).Sum();
        }

        /// <summary>
        /// Создает продукт со случайными текущим кол-вом, минимальным кол-вом и ценой. 
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        private static Product CreateRandomProduct(uint article)
        {
            Random rand = new Random();
            string name = $"Item_{article}";
            uint count = (uint)rand.Next(10);
            uint minCount = (uint)rand.Next(10);
            double price = Math.Round(GetRandomDoubleNumber(0, 20), 3);
            return new Product(name, article, count, minCount, price);
        }

        /// <summary>
        /// Получает случайное вещественное число от min до max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private static double GetRandomDoubleNumber(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}
