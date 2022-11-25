using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaret.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaret.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var user = new Domain.Entities.AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.UserName,
                Email = request.Email,
                NameSurname = request.NameSurname
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            CreateUserCommandResponse response = new() { Succeded = result.Succeeded };

            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
                return response;
            }
            else
            {
                response.Message = string.Join(',', result.Errors.Select(c => c.Description));
                return response;
            }
        }
    }
}
