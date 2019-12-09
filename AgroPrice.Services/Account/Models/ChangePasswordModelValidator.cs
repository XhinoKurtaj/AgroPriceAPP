using FluentValidation;

namespace AgroPrice.Services.Account.Models
{
    public class ChangePasswordModelValidator : AbstractValidator<ChangePasswordModel>
    {
        public ChangePasswordModelValidator()
        {
            RuleFor(model => model.Password).NotEmpty().WithMessage("Ju lutem plotesoni fjalekalimin").MinimumLength(8).WithMessage("Fjalekalimi duhet 8-16 karaktere")
                .MaximumLength(16).WithMessage("Fjalekalimi duhet 8-16 karaktere"); ;
            RuleFor(model => model.ConfirmationPassword).NotEmpty().WithMessage("Ju lutem plotesoni fjalekalimin").MinimumLength(8).WithMessage("Fjalekalimi duhet 8-16 karaktere")
                .MaximumLength(16).WithMessage("Fjalekalimi duhet 8-16 karaktere");
            RuleFor(model => model.Password).Must((model, confirmationPassword)
                    => PasswordConfirmation(model.Password, model.ConfirmationPassword))
                .WithMessage("Fjalekalimi nuk perputhet");
        }
        public bool PasswordConfirmation(string password, string confirmationPassword)
        {
            return password == confirmationPassword;
        }
    }
}
