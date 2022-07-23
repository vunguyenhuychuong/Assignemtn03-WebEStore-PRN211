using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetListProducts() => ProductDAO.Instance.GetListProducts();


        public Product GetOrderById(int id) => ProductDAO.Instance.GetProductById(id);


        public List<Product> getProductByName(string productName) => ProductDAO.getProductByName(productName);


        public List<Product> getProductByUnitPrice(string unitPrice) => ProductDAO.getProductByUnitPrice(unitPrice);


        public List<Product> getProductByUnitsSlnStock(string unitSlnStock) => ProductDAO.getProductByUnitsSlnStock(unitSlnStock);

        public void Insert(Product product) => ProductDAO.Instance.InsertProduct(product);


        public void Remove(int id) => ProductDAO.Instance.RemoveProduct(id);


        public void Update(Product product) => ProductDAO.Instance.UpdateProduct(product);


        //public List<Product> GetFilteredProducts(string tag) => ProductDAO.Instance.GetFilteredProduct(tag);

        public Product GetProductByID(int ProductID) => ProductDAO.Instance.GetProductByID(ProductID);

    }
}
