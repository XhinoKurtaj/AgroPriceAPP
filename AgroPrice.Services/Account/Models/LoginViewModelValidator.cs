using FluentValidation;

namespace AgroPrice.Services.Account.Models
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(model => model.Email).NotEmpty().MaximumLength(256);
            RuleFor(model => model.Password).NotEmpty().WithMessage("Ju lutem plotesoni fjalekalimin").MinimumLength(8).WithMessage("Fjalekalimi duhet 8-16 karaktere")
                .MaximumLength(16).WithMessage("Fjalekalimi duhet 8-16 karaktere");
        }
    }
}
