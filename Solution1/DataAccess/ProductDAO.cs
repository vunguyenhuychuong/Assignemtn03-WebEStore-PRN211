using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetListProducts()
        {
            var listProducts = new List<Product>();
            using (var db = new SalesManagementDBContext())
            {
                listProducts = db.Products.ToList();
            }
            return listProducts;
        }

        public Product GetProductById(int id)
        {
            Product product = null;
            using (var db = new SalesManagementDBContext())
            {
                product = db.Products.Find(id);
            }
            return product;
        }

        

        public Product GetProductByID(int productID)
        {
            Product product = null;
            try
            {
                using var context = new SalesManagementDBContext();
                product = context.Products.SingleOrDefault(p => p.ProductId == productID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return product;
        }

        public void InsertProduct(Product product)
        {
            Product check = GetProductById(product.ProductId);
            if (check == null)
            {
                using (var db = new SalesManagementDBContext())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Product exists already!");
            }
        }

        public void UpdateProduct(Product product)
        {
            Product check = GetProductById(product.ProductId);
            if (check != null)
            {
                using (var db = new SalesManagementDBContext())
                {
                    db.Products.Update(product);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Product does not exist!");
            }
        }

        public void RemoveProduct(int id)
        {
            Product check = GetProductById(id);
            if (check != null)
            {
                using (var db = new SalesManagementDBContext())
                {
                    db.Products.Remove(check);
                    db.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Product does not exist!");
            }
        }

        //public Product GetProductsById_Name(int id, string productName)
        //{
        //    Product product = null;
        //    using (var db = new SalesManagementDBContext())
        //    {
        //        product = db.Products.Find(id);
        //        if (!product.ProductName.Contains(productName))
        //        {
        //            product = null;
        //        }
        //    }
        //    return product;
        //}

        //public List<Product> GetProductsByUnitPrice_UnitInStock(int unitPrice, int unitslnStock)
        //{
        //    List<Product> list = new List<Product>();
        //    using (var db = new SalesManagementDBContext())
        //    {
        //        list = db.Products.Where(pro => pro.UnitPrice == unitPrice && pro.UnitslnStock == unitslnStock).ToList();
        //    }
        //    return list;
        //}

        //public List<Product> GetFilteredProduct(string tag)
        //{
        //    List<Product> filtered = new List<Product>();
        //    foreach(Product product in GetListProducts())
        //    {
        //        int add = 0;
        //        if (product.ProductId.ToString().Contains(tag))
        //            add = 1;
        //        if (product.ProductName.Contains(tag))
        //            add = 1;
        //        if (product.UnitPrice.ToString().Contains(tag))
        //            add = 1;
        //        if (product.UnitslnStock.ToString().Contains(tag))
        //            add = 1;
        //        if (add == 1)
        //            filtered.Add(product);
        //    }
        //    return filtered;
        //}

        public static List<Product> getProductByUnitPrice(string unitPrice)
        {
            List<Product> listPro = null;
            try
            {
                using (var dbContext = new SalesManagementDBContext())
                {
                    listPro = dbContext.Products.Where(product => product.UnitPrice.ToString().Contains(unitPrice.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPro;
        }

        public static List<Product> getProductByUnitsSlnStock(string unitSlnStock)
        {
            List<Product> listPro = null;
            try
            {
                using (var dbContext = new SalesManagementDBContext())
                {
                    listPro = dbContext.Products.Where(product => product.UnitslnStock.ToString().Contains(unitSlnStock.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPro;
        }

        public static List<Product> getProductByName(string productName)
        {
            List<Product> listPro = null;
            try
            {
                using (var context = new SalesManagementDBContext())
                {
                    listPro = context.Products.Where(pro => pro.ProductName.ToLower().Contains(productName.ToLower())).ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPro;
        }
    }
}
