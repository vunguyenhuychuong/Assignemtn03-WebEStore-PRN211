﻿using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetOrders();

        public Order GetOrderById(int id);

        public void Insert(Order order);

        public void Update(Order order);

        public void Remove(int OrderId);

        public List<Order> GetOrdersByDate(DateTime start, DateTime end);
    }
}
