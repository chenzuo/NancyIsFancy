using System;
using System.Collections.Generic;
using System.Linq;

namespace NancyIsFancy.Models
{
    public static class Db
    {
        static Db()
        {
            ProductStorage = new Dictionary<int, Product>();
        }

        private static int _nextProductIdentifier;
        private static readonly Dictionary<int, Product> ProductStorage;
        public static IQueryable<Product> Products { get { return ProductStorage.Values.AsQueryable(); } }

        public static void Add(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = ++_nextProductIdentifier;
            }

            ProductStorage[product.Id] = product;
        }

        public static void Remove(Product product)
        {
            if (!ProductStorage.ContainsKey(product.Id))
            {
                throw new DbException("Product not found with key " + product.Id);
            }
            ProductStorage.Remove(product.Id);
        }
    }

    [Serializable]
    public class DbException : Exception
    {
        public DbException(string message) : base(message)
        {            
        }
    }
}