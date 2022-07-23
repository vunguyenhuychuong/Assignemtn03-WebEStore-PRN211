using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        internal List<OrderDetail> GetOrderDetailList(int orderId)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetDetailByOrderId(int orderId)
        {
            OrderDetail orderDetail = null;
            using(var db = new SalesManagementDBContext())
            {
                orderDetail = db.OrderDetails.Find(orderId);
            }
            return orderDetail;
        }

        public OrderDetail GetDetailByProductId(int productId)
        {
            OrderDetail orderDetail = null;
            using(var db = new SalesManagementDBContext())
            {
                orderDetail= db.OrderDetails.Find(productId);
            }
            return orderDetail;
        }
       
        public static void Remove(OrderDetail orderDetail)
        {   
            
            try
            {
                using (var context = new SalesManagementDBContext())
                {   
                    //OrderDetail order = context.OrderDetails.
                    context.Remove(orderDetail);
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void Update(OrderDetail orderDetail)
        {
            try
            {
                using (var context = new SalesManagementDBContext())
                {
                    context.Entry<OrderDetail>(orderDetail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public static void InsertOrderDetail(Product product, Order order, int quantity)
        //{
        //    try
        //    {
        //        OrderDetail detail = GenerateOrderDetail(product, order, quantity);
        //        using (var context = new SalesManagementDBContext())
        //        {
        //            context.Add(detail);
        //            context.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public OrderDetail GetOrderDetailByID(int OrderID, int ProductID)
        {
            OrderDetail mem = null;
            try
            {
                using var context = new SalesManagementDBContext();
                mem = context.OrderDetails.SingleOrDefault(c => c.OrderId == OrderID && c.ProductId == ProductID);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return mem;
        }

        public void InsertOrderDetail(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail mem = GetOrderDetailByID(orderDetail.OrderId, orderDetail.ProductId);
                if(mem == null)
                {
                    using var context = new SalesManagementDBContext();
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The orderDetail is alredy exist");
                }
            }catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<OrderDetail> GetOrderDetailsByOrder(Order order)
        {
            List<OrderDetail> list = new List<OrderDetail>();
            try
            {
                using (var context = new SalesManagementDBContext())
                {
                    list = context.OrderDetails.Where(order_detail => order_detail.OrderId == order.OrderId).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }

        private static OrderDetail GenerateOrderDetail(Product product, Order order, int quantity)
        {
            OrderDetail detail = new OrderDetail()
            {
                Order = order,
                OrderId = order.OrderId,
                Product = product,
                ProductId = product.ProductId,
                Quantity = quantity,
                UnitPrice = product.UnitPrice
            };
            return detail;
        }

        
    }
}
