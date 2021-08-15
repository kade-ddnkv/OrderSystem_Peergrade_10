using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

// Этот файл отвечает за класс Product.

namespace OrderSystem_Peergrade_10.Storehouse__Old_Peergrade_
{
    /// <summary>
    /// Класс товара (сериализуемый).
    /// </summary>
    [Serializable]
    public partial class Product
    {
        // Все основные свойства товара.
        // Для добавления свойства нужно отредактировать конструктор и класс CreateProductForm.
        public string Name { get; set; }
        public uint Article { get; set; }
        public uint Count { get; set; }
        public uint MinCount { get; set; }
        public double Price { get; set; }

        /// <summary>
        /// Конструктор из списка строк. 1-Name, 2-Article, 3-Count, 4-MinCount, 5-Price. Может вызвать исключение.
        /// </summary>
        /// <exception cref="ArgumentException"> If count of props of Product is incorrect.
        /// Or if Count or Price are not correct numbers.</exception>
        /// <param name="props"></param>
        public Product(List<string> props)
        {
            if (props.Count != 5)
            {
                throw new ArgumentException();
            }
            Name = props[0];
            Article = uint.Parse(props[1]);
            Count = uint.Parse(props[2]);
            MinCount = uint.Parse(props[3]);
            Price = double.Parse(props[4], CultureInfo.InvariantCulture);
            if (Price < 0)
            {
                throw new ArgumentException();
            }
        }

        public Product(string name, uint article, uint count, uint minCount, double price)
        {
            Name = name;
            Article = article;
            Count = count;
            MinCount = minCount;
            Price = price;
        }

        /// <summary>
        /// Получает значения всех свойств экземпляра Product (через рефлексию).
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllProperties()
        {
            Type type = typeof(Product);
            PropertyInfo[] propertyInfos = type.GetProperties();
            return propertyInfos.Select(
                pInfo => pInfo.GetValue(this) is double
                ? Math.Round((double)pInfo.GetValue(this), 3).ToString(CultureInfo.InvariantCulture)
                : pInfo.GetValue(this).ToString())
                .ToList();
        }

        /// <summary>
        /// Получает названия всех свойств класса Product (через рефлексию).
        /// </summary>
        /// <returns></returns>
        static public List<string> GetAllPropertiesNames()
        {
            Type type = typeof(Product);
            PropertyInfo[] propertyInfos = type.GetProperties();
            return propertyInfos.Select(pInfo => pInfo.Name).ToList();
        }

        /// <summary>
        /// Равенство по открытым свойствам Product.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator ==(Product p1, Product p2)
        {
            return p1?.Name     == p2?.Name 
                && p1?.Article  == p2?.Article 
                && p1?.Count    == p2?.Count 
                && p1?.MinCount == p2?.MinCount 
                && p1?.Price    == p2?.Price;
        }

        /// <summary>
        /// Неравенство по открытым свойствам Product.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static bool operator !=(Product p1, Product p2)
        {
            return !(p1 == p2);
        }

        public bool IsEqualByNameArticlePrice(Product other)
        {
            return Name == other.Name 
                && Article == other.Article 
                && Price == other.Price;
        }

        public void ReduceCount()
        {
            if (Count >= 1)
            {
                Count -= 1;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
}
