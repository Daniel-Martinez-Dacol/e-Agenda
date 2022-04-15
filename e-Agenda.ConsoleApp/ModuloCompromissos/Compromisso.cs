using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;
using e_Agenda.ConsoleApp.ModuloContatos;

namespace e_Agenda.ConsoleApp.ModuloCompromissos
{
    /*
    Assunto, local, data do compromisso,
    hora de início e término.
    A maioria dos compromissos 
    estão relacionados com algum contato de sua agenda.
     */
    public class Compromisso : EntidadeBase
    {
        private readonly string _assunto;
        private readonly string _local;
        private readonly DateTime _dataCompromisso;
        private readonly DateTime _dataInicio;
        private readonly DateTime _dataTermino;
        private readonly Contato _contato;

        public string Assunto => _assunto;
        public string Local => _local;
        public DateTime DataCompromisso => _dataCompromisso;
        public DateTime DataInicio => _dataInicio;
        public DateTime DataTermino => _dataTermino;
        public Contato Contato => _contato;

        public Compromisso(string assunto, string local, DateTime dataCompromisso, DateTime dataInicio, DateTime dataTermino, Contato contato)
        {
            this._assunto = assunto;
            this._local = local;
            this._dataCompromisso = dataCompromisso;
            this._dataInicio = dataInicio;
            this._dataTermino = dataTermino;
            this._contato = contato;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Assunto))
                erros.Add("Por favor insira o assunto do Compromisso!");

            if (string.IsNullOrEmpty(Local))
                erros.Add("É necessário inserir o local do Compromisso!");
            
            if(Contato.Equals(null))
                erros.Add("É necessário inserir o contato do Compromisso!");
            
            return new ResultadoValidacao(erros);
        }       
    }
}
