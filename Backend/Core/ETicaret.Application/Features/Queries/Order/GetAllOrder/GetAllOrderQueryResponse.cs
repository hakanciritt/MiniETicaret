﻿using ETicaret.Domain.Enums;

namespace ETicaret.Application.Features.Queries.Order.GetAllOrder
{
    public class GetAllOrderQueryResponse
    {

        public int TotalOrderCount { get; set; }
        public List<OrderResponseModel> Orders { get; set; }

    }
    public class OrderResponseModel
    {
        public Guid Id { get; set; }
        public string OrderNo { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}



