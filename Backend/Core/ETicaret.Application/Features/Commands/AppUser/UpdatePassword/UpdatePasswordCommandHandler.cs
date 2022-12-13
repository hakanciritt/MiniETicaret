using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Exceptions;
using MediatR;

namespace ETicaret.Application.Features.Commands.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
    {
        private readonly IUserService _userService;

        public UpdatePasswordCommandHandler(IUserService userService )
        {
            _userService = userService;
        }
        public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Password != request.PasswordConfirm) throw new UserFriendlyException("Lütfen şifreyi doğru girdiğinizden emin olunuz.");

             await _userService.UpdatePassword(request.UserId, request.ResetToken, request.Password);

            return new() { };

        }
    }
}
