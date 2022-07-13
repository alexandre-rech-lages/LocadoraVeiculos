﻿using LocadoraVeiculos.Dominio.Compartilhado;
using System;

namespace LocadoraVeiculos.Dominio.ModuloCliente
{
    public class Cliente : EntidadeBase<Cliente>
    {
        public Cliente()
        {
        }

        public Cliente(string nome, string telefone, string email,
            TipoCliente tipoCliente, string cpf, string cnpj,
            int numero, string rua, string bairro, string cidade, string estado)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            TipoCliente = tipoCliente;
            Cpf = cpf;
            Cnpj = cnpj;
            Numero = numero;
            Rua = rua;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public TipoCliente TipoCliente { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public int Numero { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public string Documento
        {
            get
            {
                return TipoCliente == TipoCliente.PessoaFisica ? Cpf : Cnpj;
            }
        }

        public Cliente Clone()
        {
            return MemberwiseClone() as Cliente;
        }

        public override bool Equals(object obj)
        {
            return obj is Cliente cliente &&
                   Id == cliente.Id &&
                   Nome == cliente.Nome &&
                   Telefone == cliente.Telefone &&
                   Email == cliente.Email &&
                   TipoCliente == cliente.TipoCliente &&
                   Cpf == cliente.Cpf &&
                   Cnpj == cliente.Cnpj &&
                   Numero == cliente.Numero &&
                   Rua == cliente.Rua &&
                   Bairro == cliente.Bairro &&
                   Cidade == cliente.Cidade &&
                   Estado == cliente.Estado;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Nome);
            hash.Add(Telefone);
            hash.Add(Email);
            hash.Add(TipoCliente);
            hash.Add(Cpf);
            hash.Add(Cnpj);
            hash.Add(Numero);
            hash.Add(Rua);
            hash.Add(Bairro);
            hash.Add(Cidade);
            hash.Add(Estado);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"Nome: {Nome} e Email: {Email}";
        }
    }
}