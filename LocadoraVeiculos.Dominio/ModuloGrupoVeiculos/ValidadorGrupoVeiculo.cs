using FluentValidation;

namespace LocadoraVeiculos.Dominio.ModuloGrupoVeiculos
{
    public class ValidadorGrupoVeiculo : AbstractValidator<GrupoVeiculo>
    {
        public ValidadorGrupoVeiculo()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O campo 'Nome' é obrigatório!")
                .NotNull().WithMessage("O campo 'Nome' é obrigatório!")
                .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
                .WithMessage("Caracteres especiais não são permitidos!");
        }
    }
}