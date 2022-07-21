using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void Delete(int orderId, int productId)
        {
            throw new NotImplementedException();
        }

        public OrderDetail GetDetailById(int orderId)
            => OrderDetailDAO.Instance.GetDetailByOrderId(orderId);


        public OrderDetail GetDetailByProduct(int productId)
            => OrderDetailDAO.Instance.GetDetailByProductId(productId);

        //public List<OrderDetail> GetOrderDetailsByOrder(int orderId)
        //    => OrderDetailDAO.Instance.GetOrderDetailList(orderId);

        //public void Insert(OrderDetail orderDetail)
        //    => OrderDetailDAO.Instance.InsertOrderDetail(orderDetail);

        //public void Update(OrderDetail orderDetail)
        //    => OrderDetailDAO.Instance.UpdateOrderDetail(orderDetail);
    }
}
