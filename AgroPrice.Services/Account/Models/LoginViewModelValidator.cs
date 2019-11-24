using FluentValidation;

namespace AgroPrice.Services.Account.Models
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(model => model.Email).NotEmpty().MaximumLength(256);
            RuleFor(model => model.Password).NotEmpty().MinimumLength(8)
                      .MaximumLength(16);
        }
    }
}
