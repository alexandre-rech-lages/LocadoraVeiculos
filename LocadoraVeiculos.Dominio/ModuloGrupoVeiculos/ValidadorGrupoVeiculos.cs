using FluentValidation;

namespace LocadoraVeiculos.Dominio.ModuloGrupoVeiculos
{
    public class ValidadorGrupoVeiculos : AbstractValidator<GrupoVeiculos>
    {
        public ValidadorGrupoVeiculos()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo 'Nome' é obrigatório!")
                .NotNull().WithMessage("O campo 'Nome' é obrigatório!")
                .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
                .WithMessage("Caracteres especiais não são permitidos!");
        }
    }
}