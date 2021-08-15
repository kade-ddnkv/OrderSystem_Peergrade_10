using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

// Этот файл отвечает за анализ товаров. (Получение csv файла с недостающими по количеству).

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_
{
    public partial class Product
    {
        /// <summary>
        /// Полный путь к товару по директориям
        /// Не сделал свойство публичным, чтобы рефлексия его не трогала.
        /// </summary>
        private string DirectoryPath { get; set; }

        /// <summary>
        /// Устанавливает значение для для DirectoryPath.
        /// </summary>
        /// <param name="path"></param>
        public void SetDirectoryPath(string path)
        {
            DirectoryPath = path;
        }

        /// <summary>
        /// Сохраняет список недостающих товаров из списка в файл по переданному пути.
        /// </summary>
        /// <param name="products"></param>
        /// <param name="fileName"></param>
        public static void CsvAnalyseSave(List<Product> products, string fileName)
        {
            var productsRunningOutOf = products.Where(product => product.Count < product.MinCount);

            // Запись происходит очень просто с помощью CsvHelper.
            try
            {
                using (var writer = new StreamWriter(fileName))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // Запись ручная, чтобы самому выбирать колонки.
                    foreach (string columnName in new string[] { "DirectoryPath", "Article", "Name", "Count" })
                    {
                        csv.WriteField(columnName);
                    }
                    csv.NextRecord();

                    foreach(Product product in productsRunningOutOf)
                    {
                        csv.WriteField(product.DirectoryPath);
                        csv.WriteField(product.Article);
                        csv.WriteField(product.Name);
                        csv.WriteField(product.Count);
                        csv.NextRecord();
                    }
                }
            }
            catch (IOException ex)
            {
                MainForm.ShowExceptionMessage(ex);
            }
            // Возникать не должно, но на всякий случай.
            catch (CsvHelperException ex)
            {
                MainForm.ShowExceptionMessage(ex);
            }
        }
    }
}
