﻿using LocadoraVeiculos.Dominio.Compartilhado;
using System;

namespace LocadoraVeiculos.Dominio.ModuloFuncionario
{
    public class Funcionario : EntidadeBase<Funcionario>
    {
        public Funcionario()
        {
            this.EstaAtivo = true;
        }

        public Funcionario(string nome, string login, string senha,
            DateTime dataAdmissao, decimal salario, bool ehAdmin, bool estaAtivo)
        {
            Nome = nome;
            Usuario = login;
            Senha = senha;
            DataAdmissao = dataAdmissao;
            Salario = salario;
            EhAdmin = ehAdmin;
            EstaAtivo = estaAtivo;
        }

        public string Nome { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public DateTime DataAdmissao { get; set; }
        public decimal Salario { get; set; }
        public bool EhAdmin { get; set; }
        public bool EstaAtivo { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", Nome, Usuario, Senha, DataAdmissao, Salario, EhAdmin, EstaAtivo);
        }

        public override bool Equals(object obj)
        {
            Funcionario f = obj as Funcionario;
            if (f == null)
                return false;

            return
             f.Id.Equals(Id) &&
             f.Nome.Equals(Nome) &&
             f.Usuario.Equals(Usuario) &&
             f.Senha.Equals(Senha) &&
             f.DataAdmissao.Equals(DataAdmissao) &&
             f.Salario.Equals(Salario) &&
             f.EhAdmin.Equals(EhAdmin) &&
             f.EstaAtivo.Equals(EstaAtivo);

        }
        public Funcionario Clone()
        {
            return MemberwiseClone() as Funcionario;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Nome, Usuario, Senha, DataAdmissao, Salario, EhAdmin, EstaAtivo);
        }
    }
}