using LocadoraVeiculos.Dominio.Compartilhado;
using LocadoraVeiculos.Dominio.ModuloCliente;
using System;
using System.Collections.Generic;

namespace LocadoraVeiculos.Dominio.ModuloCondutor
{
    public class Condutor : EntidadeBase<Condutor>
    {
        public Condutor()
        {
        }

        public Condutor(string nome, string telefone, string email,
            string cpf, string cnh, DateTime dataValidadeCnh, Cliente cliente)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Cpf = cpf;
            Cnh = cnh;
            DataValidadeCnh = dataValidadeCnh;
            Cliente = cliente;
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Cnh { get; set; }
        public DateTime DataValidadeCnh { get; set; }
        public Cliente Cliente { get; set; }
        public bool ClienteCondutor
        {
            get
            {
                bool retorno = false;

                if (Cliente != null)
                    if (Cpf == Cliente.Documento)
                        retorno = true;
                    else
                        retorno = false;

                return retorno;
            }
        }

        public Condutor Clone()
        {
            return MemberwiseClone() as Condutor;
        }

        public override bool Equals(object obj)
        {
            return obj is Condutor condutor &&
                   Id.Equals(condutor.Id) &&
                   Nome == condutor.Nome &&
                   Telefone == condutor.Telefone &&
                   Email == condutor.Email &&
                   Cpf == condutor.Cpf &&
                   Cnh == condutor.Cnh &&
                   DataValidadeCnh == condutor.DataValidadeCnh &&
                   EqualityComparer<Cliente>.Default.Equals(Cliente, condutor.Cliente) &&
                   ClienteCondutor == condutor.ClienteCondutor;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(Id);
            hash.Add(Nome);
            hash.Add(Telefone);
            hash.Add(Email);
            hash.Add(Cpf);
            hash.Add(Cnh);
            hash.Add(DataValidadeCnh);
            hash.Add(Cliente);
            hash.Add(ClienteCondutor);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            return $"Nome: {Nome} Telefone: {Telefone} Email: {Email}" +
                $" CPF: {Cpf} CNH: {Cnh} Data de validade CNH: {DataValidadeCnh}" +
                $" Cliente: {Cliente.Nome} - {Cliente.Documento}";
        }
    }
}
