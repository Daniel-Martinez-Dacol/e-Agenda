using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;

namespace e_Agenda.ConsoleApp.ModuloContatos
{
    /*
     Desta forma, para cada contato,
    José Pedro gostaria de armazenar o nome, e-mail,
    telefone, empresa e o cargo da pessoa
    e claro ele terá a
possibilidade de registrar novos contatos, visualizar, editar e excluir contatos existentes;
     */
    public class Contato : EntidadeBase
    {
        private readonly string _nome;
        private readonly string _email;
        private readonly string _telefone;
        private readonly string _empresa;
        private readonly string _cargo;

        public string Nome => _nome;
        public string Email => _email;
        public string Telefone => _telefone;
        public string Empresa => _empresa;
        public string Cargo => _cargo;



        public Contato(string nome, string email, string telefone, string empresa, string cargo)
        {
            this._nome = nome;
            this._email = email;
            this._telefone = telefone;
            this._empresa = empresa;
            this._cargo = cargo;
        }
        public override string ToString()
        {
            return "Número: " + numero + Environment.NewLine +
                "Nome: " + Nome + Environment.NewLine +
                "E-mail: " + Email + Environment.NewLine +
                "Telefone: " + Telefone + Environment.NewLine +
                "Empresa: " + Empresa + Environment.NewLine +
                "Cargo: " + Cargo + Environment.NewLine;
        }
        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Nome))
                erros.Add("Por favor insira o nome do contato!");

            if (string.IsNullOrEmpty(Email))
                erros.Add("É necessário inserir o e-mail do contato!");
           
            if (string.IsNullOrEmpty(Telefone))
                erros.Add("É necessário inserir o telfone do contato!");
            
            if (string.IsNullOrEmpty(Empresa))
                erros.Add("É necessário inserir o nome da empresa do contato!");

            if (string.IsNullOrEmpty(Cargo))
                erros.Add("É necessário inserir o cargo do contato!");


            return new ResultadoValidacao(erros);
        }
    }
}
