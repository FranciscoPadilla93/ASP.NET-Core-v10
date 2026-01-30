using Entities.Models;
using FluentValidation;

namespace Business.Validators
{
    public class UserSetValidator : AbstractValidator<UserSet>
    {
        public UserSetValidator()
        {
            RuleFor(x => x.loginUser)
                .NotEmpty().WithMessage("El nombre de Usuario es obligatorio.")
                .Length(4, 20).WithMessage("El login debe ser entre 4 y 20 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo es obligatorio.")
                .EmailAddress().WithMessage("El formato del correo no es válido.");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(3).WithMessage("La contraseña debe tener al menos 3 caracteres.");

            RuleFor(x => x.idRol)
                .GreaterThan(0).WithMessage("Debe seleccionar un rol válido.");
        }
    }
}
