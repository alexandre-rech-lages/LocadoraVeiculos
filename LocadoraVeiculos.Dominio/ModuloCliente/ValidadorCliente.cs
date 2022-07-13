﻿using FluentValidation;
using System.Text.RegularExpressions;

namespace LocadoraVeiculos.Dominio.ModuloCliente
{
    public class ValidadorCliente : AbstractValidator<Cliente>
    {
        public ValidadorCliente()
        {
            RuleFor(x => x.Nome)
                .NotNull().WithMessage("O campo 'Nome' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Nome' é obrigatório!");

            When(x => string.IsNullOrEmpty(x.Nome) == false, () =>
            {
                RuleFor(x => x.Nome.Length)
                .GreaterThan(2)
                .WithMessage("O campo 'Nome' deve ter no mínimo 3 caracteres!");

                RuleFor(x => x.Nome)
                .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
                .WithMessage("Caracteres especiais não são permitidos!");
            });

            RuleFor(x => x.Rua)
                .NotNull().WithMessage("O campo 'Rua' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Rua' é obrigatório!");

            When(x => string.IsNullOrEmpty(x.Rua) == false, () =>
            {
                RuleFor(x => x.Rua.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Rua' deve ter no mínimo 5 caracteres!");

                RuleFor(x => x.Rua)
                .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
                .WithMessage("Caracteres especiais não são permitidos!");

            });

            RuleFor(x => x.Bairro)
               .NotNull().WithMessage("O campo 'Bairro' é obrigatório!")
               .NotEmpty().WithMessage("O campo 'Bairro' é obrigatório!");

            When(x => string.IsNullOrEmpty(x.Bairro) == false, () =>
            {
                RuleFor(x => x.Bairro.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Bairro' deve ter no mínimo 5 caracteres!");

                RuleFor(x => x.Bairro)
                .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
                .WithMessage("Caracteres especiais não são permitidos!");
            });

            RuleFor(x => x.Cidade)
                .NotNull().WithMessage("O campo 'Cidade' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Cidade' é obrigatório!");

            When(x => string.IsNullOrEmpty(x.Cidade) == false, () =>
            {
                RuleFor(x => x.Cidade.Length)
                .GreaterThan(4)
                .WithMessage("O campo 'Cidade' deve ter no mínimo 5 caracteres!");

                RuleFor(x => x.Cidade)
               .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
               .WithMessage("Caracteres especiais não são permitidos!");

            });

            RuleFor(x => x.Estado)
                .NotNull().WithMessage("O campo 'Estado' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Estado' é obrigatório!");

            When(x => string.IsNullOrEmpty(x.Estado) == false, () =>
            {
                RuleFor(x => x.Estado.Length)
                    .Equal(2)
                    .WithMessage("O campo 'Estado' deve ter somente 2 caracteres!");

                RuleFor(x => x.Estado)
               .Matches(@"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]*$")
               .WithMessage("Caracteres especiais não são permitidos!");

            });


            When(x => x.TipoCliente == TipoCliente.PessoaJuridica, () =>
            {
                RuleFor(x => x.Cnpj)
                .NotNull().WithMessage("O campo 'CNPJ' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'CNPJ' é obrigatório!");

                RuleFor(x => x.Cnpj)
                .Custom((cnpj, context) =>
                {
                    if (string.IsNullOrEmpty(cnpj) == false)
                    {
                        if (Regex.IsMatch(cnpj, @"^[0-9]{2}[\.][0-9]{3}[\.][0-9]{3}[\/][0-9]{4}[-][0-9]{2}", RegexOptions.IgnoreCase) == false)
                            context.AddFailure("O campo 'CNPJ' deve ser válido!");
                    }
                });
            });

            When(x => x.TipoCliente == TipoCliente.PessoaFisica, () =>
            {
                RuleFor(x => x.Cpf)
                .NotNull().WithMessage("O campo 'CPF' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'CPF' é obrigatório!");

                RuleFor(x => x.Cpf)
                .Custom((cpf, context) =>
                {
                    if (string.IsNullOrEmpty(cpf) == false)
                    {
                        if (Regex.IsMatch(cpf, @"^[0-9]{3}[\.][0-9]{3}[\.][0-9]{3}[\-][0-9]{2}", RegexOptions.IgnoreCase) == false)
                            context.AddFailure("O campo 'CPF' deve ser válido!");
                    }
                });
            });

            RuleFor(x => x.Telefone)
                .NotNull().WithMessage("O campo 'Telefone' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Telefone' é obrigatório!");

            RuleFor(x => x.Telefone)
              .Custom((telefone, context) =>
              {
                  if (string.IsNullOrEmpty(telefone) == false)
                  {
                      if ((Regex.IsMatch(telefone, @"^\([0-9]{2}\) [0-9]{5}\-[0-9]{4}$")) == false)
                          context.AddFailure("O campo 'Telefone' deve ser válido!");
                  }
              });

            RuleFor(x => x.Email)
                .NotNull().WithMessage("O campo 'Email' é obrigatório!")
                .NotEmpty().WithMessage("O campo 'Email' é obrigatório!");

            RuleFor(x => x.Email)
              .Custom((email, context) =>
              {
                  if (string.IsNullOrEmpty(email) == false)
                  {
                      if (System.Net.Mail.MailAddress.TryCreate(email, out _) == false)
                          context.AddFailure("O campo 'Email' deve ser válido!");
                  }
              });



        }
    }
}