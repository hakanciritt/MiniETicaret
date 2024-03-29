﻿using ETicaret.Application.DTOs.User;
using ETicaret.Domain.Entities;
using ETicaret.Domain.Enums;

namespace ETicaret.Application.DTOs.Basket
{
    public class BasketDto : BaseEntityDto
    {
        public string? UserId { get; set; }
        public AppUserDto User { get; set; }
        public Status BasketStatus { get; set; }
        public bool Lock { get; set; }
        public ICollection<BasketItemDto> BasketItems { get; set; }
    }
}
