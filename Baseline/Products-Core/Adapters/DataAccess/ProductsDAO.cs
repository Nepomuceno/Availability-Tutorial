using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Products_Core.Model;
using Simple.Data;

namespace Products_Core.Adapters.DataAccess
{
    public class ProductsDAO : IProductsDAO
    {
        private readonly dynamic _db;

        public ProductsDAO()
        {
            _db = Database.OpenConnection("data source =.; initial catalog = Products; integrated security-true");
        }

        public dynamic BeginTransaction()
        {
            return _db.BeginTransaction();
        }

        public Product Add(Product newProduct)
        {
            return _db.Products.Insert(newProduct);
        }

        public void Clear()
        {
            _db.Products.DeleteAll();
        }

        public void Delete(int productId)
        {
            _db.Products.DeleteById(productId);
        }

        public IEnumerable<Product> FindAll()
        {
            return _db.Products.All().ToList<Product>();
        }

        public Product FindById(int id)
        {
            return _db.Products.FindById(id);
        }

        public void Update(Product product)
        {
            _db.Products.UpdateById(product);
        }

    }
}
