using System;
using System.Collections.Generic;
using e_Agenda.ConsoleApp.Compartilhado;
/*
 Para cada tarefa, José Pedro registra uma prioridade. Ou seja, quanto maior for este valor, mais relevância terá a tarefa. Uma ideia para a
prioridade é classificá-la em 3 categorias: Alta, Normal e Baixa.

Além da prioridade, uma tarefa deve conter: o título, a data de criação, data de conclusão e o percentual concluído.

Para cada tarefa há uma lista de itens que descrevem sua execução e para cada item, deve-se informar a descrição e se está pendente ou
concluído.

O percentual de cada tarefa é calculado nos percentuais dos itens de execução. Quando uma tarefa receber 100% de execução, esta deve
ser movida automaticamente para a lista de tarefas concluídas, podendo ser apagada, se for o caso.

O José Pedro gostaria também de visualizar as tarefas pendentes separadas das tarefas concluídas. JP terá a possibilidade de cadastrar novas
tarefas e editar/excluir as tarefas já existentes.

 */
namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class Tarefa : EntidadeBase
    {
        private readonly string _prioridade;
        private readonly string _titulo;
        private readonly DateTime _dataDeCriacao;
        private readonly DateTime _dataDeConclusao;
        private readonly int _percentualConcluido;
        private readonly List<String> _listaDeItens;
        private readonly bool _pendencia;

        public string Prioridade => _prioridade;

        public string Titulo => _titulo;

        public DateTime DataDeCriacao => _dataDeCriacao;

        public DateTime DataDeConclusao => _dataDeConclusao;

        public int PercentualConcluido => _percentualConcluido;

        public List<String> ListaDeItens => _listaDeItens;

        public bool Pendencia => _pendencia;

        public Tarefa(string prioridade, string titulo, DateTime dataDeCriacao, DateTime dataDeConclusao, int percentualConcluido, List<string> listaDeItens, bool pendencia)
        {
            this._prioridade = prioridade;
            this._titulo = titulo;
            this._dataDeCriacao = dataDeCriacao;
            this._dataDeConclusao = dataDeConclusao;
            this._percentualConcluido = percentualConcluido;
            this._listaDeItens = listaDeItens;
            this._pendencia = pendencia;
        }

        public override string ToString()
        {
            string pendenciaDaTarefa = "";
            if (Pendencia == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                pendenciaDaTarefa = "Tarefa Pendente!";
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                pendenciaDaTarefa = "Tarefa Não Pendente!";
                Console.ResetColor();
            }
            return "-Número: " + numero + Environment.NewLine +
                "-Prioridade: " + Prioridade + Environment.NewLine +
                "-Data de Criação: " + DataDeCriacao.ToString("D") + Environment.NewLine +
                "-Data de Conclusão: " + DataDeConclusao.ToString("D") + Environment.NewLine +
                "-Percentual: " + PercentualConcluido + "%" + Environment.NewLine +
                "\n-Lista de Itens: { " + ListaDeItens + " }\n"+ Environment.NewLine +
                "-Pendencia: " + pendenciaDaTarefa + Environment.NewLine;
        }

        public override ResultadoValidacao Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrEmpty(Prioridade))
                erros.Add("Por favor insira a prioridade da tarefa!");

            if (string.IsNullOrEmpty(Titulo))
                erros.Add("É necessário inserir um Titulo para a tarefa!");

            if (PercentualConcluido > 100 || PercentualConcluido < 0)
                erros.Add("Percentual invalido para a tarefa!");

            return new ResultadoValidacao(erros);
        }
    }
}
