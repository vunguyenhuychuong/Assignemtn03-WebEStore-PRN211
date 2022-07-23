using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetail> GetOrderDetails(int orderId);

        //public OrderDetail GetDetailById(int orderId);

        //public OrderDetail GetDetailByProduct(int productId);

        public void Insert(OrderDetail orderDetail);

        //public void Update(OrderDetail orderDetail);

        //public void Delete(int orderId, int productId);
    }
}
