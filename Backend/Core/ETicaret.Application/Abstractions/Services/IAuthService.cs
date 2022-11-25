﻿using ETicaret.Application.Abstractions.Services.Authentications;
using ETicaret.Application.DTOs;
using ETicaret.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        

    }
}
