﻿using Mobile_ecommerce.Areas.Admin.Model;
using Mobile_ecommerce.Models.EF;
using Mobile_ecommerce.Models.ViewModel.Common;
using Mobile_ecommerce.Models.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Mobile_ecommerce.Models.DAL
{
    public class OrderDAL
    {
        private MobileDbContext dbContext;
        public OrderDAL()
        {
            dbContext = new MobileDbContext();
        }
        public List<Detail> GetTotalOrder()
        {
           List<Order> model = dbContext.Orders.ToList();
            var list = model.OrderByDescending(x => x.OrderID).ToList()
              .Select(x => new Detail()
              {
                 Ngay=x.OrderDate.ToString(),
                 OrderID=x.OrderID,           
                 TongTien=x.Total,
              }).ToList();
            return list;
        }

        public async Task<List<Shipping>> GetAllShipping()
        {
            var cate = await dbContext.Shippings.ToListAsync();
            return cate;
        }
        public async Task<List<OrderStatus>> GetAllStatus()
        {
            var cate = await dbContext.OrderStatuses.ToListAsync();
            return cate;
        }
        public async Task<ResultPaging<Detail>> GetList(GetListPaging paging)
        {
            var model = from a in dbContext.Orders
                        join b in dbContext.OrderStatuses
                        on a.OrderStatusID equals b.OrderStatusID
                        select new Detail()
                        {
                            OrderID = a.OrderID,
                            Ngay = a.OrderDate.ToString(),
                            TongTien = a.Total,
                            Status=b.Status,
                        };
            if (!string.IsNullOrEmpty(paging.keyWord))
            {
                model = model.Where(x => x.TongTien.ToString().Contains(paging.keyWord.Trim()));
            }

            int total = await model.CountAsync();

            var items = await model.OrderBy(x => x.OrderID)
                .Skip((paging.PageIndex - 1) * paging.PageSize).Take(paging.PageSize)
                .ToListAsync();
            return new ResultPaging<Detail>()
            {
                Items = items,
                TotalRecord = total
            };
        }
        public async Task<bool> Edit(int id)
        {
            try
            {
                var pro = await dbContext.Orders.FindAsync(id);
                if (pro == null) return false;
                pro.OrderStatusID = 6;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        //Chi tiết hóa đơn
        public async Task<List<OrderInfo>> GetByIdInfo(int id)
        {
            try
            {
                return await (from a in dbContext.Products                         
                              join b in dbContext.OrderDetails
                              on a.ProductID equals b.ProductID
                              join c in dbContext.Orders
                              on b.OrderID equals c.OrderID
                              where c.OrderID.Equals(id)
                              select new OrderInfo()
                              {
                                  Name = a.ProductName,                                  
                                  Price = a.Price,                         
                                  Quantity = b.Quantity,
                              }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        //Hóa đơn
        public async Task<Edit> GetById(int id)
        {
            try
            {
                return await (from a in dbContext.Orders
                              join b in dbContext.OrderStatuses
                              on a.OrderStatusID equals b.OrderStatusID
                              join c in dbContext.Customers
                  on a.CustomerID equals c.CustomerID
                              where a.OrderID.Equals(id)
                              select new Edit()
                              {
                                  Ngay = a.OrderDate.ToString(),
                                  TongTien = a.Total,
                                  CustomerID = a.CustomerID,
                                  OrderID = a.OrderID,
                                  Name = c.CustomerName,
                                  ShippingID = a.ShippingID,
                                  StatusID = a.OrderStatusID,
                              }).SingleOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> UpdatePro(Edit edit,int id)
        {
            try
            {
                var pro = await dbContext.Orders.FindAsync(id);
                if (pro == null) return false;              
                pro.OrderStatusID = edit.StatusID;
                pro.ShippingID = edit.ShippingID;
                return await dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}