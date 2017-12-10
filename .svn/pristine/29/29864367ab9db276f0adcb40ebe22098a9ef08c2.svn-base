using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AMS.Models.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public string ShortOrderNumber { get; set; }
        //public string UUID { get; set; }
        public int UserId { get; set; }
        public int MerchantId { get; set; }
        //public OrderState State { get; set; }
        //public string CourierName { get; set; }
        public double Total { get; set; }
        public double GrandTotal { get; set; }
        public double Tip { get; set; }
        public string Notes { get; set; }
        public bool IsSchedule { get; set; }
        //public DateTime StartDateTime { get; set; }
        public DateTime ScheduleDateTime { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreationDate { get; set; }
        public List<OrderProducts> Products { get; set; }
    }

    public class OrderProducts
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
    public enum PaymentMethod
    {
        Cash = 1,
        Visa
    }
    public enum OrderState
    {
        Pending = 1,
        Accepted,
        Ready,
        Canceled,
        Delivered
    }
    public class OrderDetails
    {
        public int Id { get; set; }
        public OrderState State { get; set; }

    }
}
