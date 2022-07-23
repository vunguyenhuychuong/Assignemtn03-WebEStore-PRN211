using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IProductRepository
    {
        public List<Product> GetListProducts();
        public Product GetOrderById(int id);
        public void Insert(Product product);
        public void Update(Product product);
        public void Remove(int id);

        List<Product> getProductByName(string productName);
        List<Product> getProductByUnitPrice(string unitPrice);

        List<Product> getProductByUnitsSlnStock(string unitSlnStock);


    }
}
