﻿using System;
using e_Agenda.ConsoleApp.Compartilhado;
using e_Agenda.ConsoleApp.ModuloTarefa;
using e_Agenda.ConsoleApp.ModuloContatos;
using e_Agenda.ConsoleApp.ModuloCompromissos;
/*
 José Pedro é uma pessoa que tem muito o que fazer e desta forma precisa controlar todas as suas tarefas diárias, semanais e mensais. Ele
tentou utilizar as agendas de papel, entretanto esta ferramenta não consegue ser tão dinâmica quanto as boas listas no estilo “To Do”.

José Pedro já tentou anotar milhares de post-its, enviou e-mail para si mesmo lotando a sua caixa de entrada e diante destas tentativas que
falharam, ele decidiu pedir ajuda para os futuros desenvolvedores que estão na 9ª academia do programador para desenvolverem uma
aplicação que auxilie em seu controle de tarefas diárias, semanais e mensais.

Para cada tarefa, José Pedro registra uma prioridade. Ou seja, quanto maior for este valor, mais relevância terá a tarefa. Uma ideia para a
prioridade é classificá-la em 3 categorias: Alta, Normal e Baixa.
Além da prioridade, uma tarefa deve conter: o título, a data de criação, data de conclusão e o percentual concluído.
Para cada tarefa há uma lista de itens que descrevem sua execução e para cada item, deve-se informar a descrição e se está pendente ou
concluído.

O percentual de cada tarefa é calculado nos percentuais dos itens de execução. Quando uma tarefa receber 100% de execução, esta deve
ser movida automaticamente para a lista de tarefas concluídas, podendo ser apagada, se for o caso.

O José Pedro gostaria também de visualizar as tarefas pendentes separadas das tarefas concluídas. JP terá a possibilidade de cadastrar novas
tarefas e editar/excluir as tarefas já existentes.

Na edição das tarefas não poderá ser alterado a data de criação.
José Pedro gostaria de visualizar as tarefas finalizadas e as tarefas pendentes de forma separada e claro, as tarefas deverão ser
apresentadas ordenadas por prioridade.

José Pedro também gosta de participar em eventos, palestras e congressos de tecnologia. E Depois de horas de networking é normal ele
voltar para casa com vários cartões com contatos de seus novos colegas. É bastante comum ele deixar estes cartões guardados, que podem
ser esquecidos no fundo de uma gaveta...

Para isto, será necessário fazer uma gestão de contatos inteligente e JP pretende fazer isso utilizando um sistema.
Desta forma, para cada contato, José Pedro gostaria de armazenar o nome, e-mail, telefone, empresa e o cargo da pessoa e claro ele terá a
possibilidade de registrar novos contatos, visualizar, editar e excluir contatos existentes;

O sistema não pode permitir o cadastro caso o e-mail ou o telefone estejam inválidos.

A aplicação deverá possibilitar a visualização da lista e contatos agrupados por cargo.

José Pedro tem a necessidade de saber com precisão quais são os seus compromissos, e marcá-los sempre para horários em que será possível
cumpri-los pontualmente, isso mostra que ele quer fazer parte do grupo das pessoas produtivas e responsáveis.

Desta forma, além do controle de tarefas e cadastro de contatos, José Pedro pretende controlar seus Compromissos tendo uma visibilidade
semanal e diária.

Para cada compromisso, José Pedro gostaria de armazenar: Assunto, local, data do compromisso, hora de início e término. A maioria dos
compromissos estão relacionados com algum contato de sua agenda.
O JP terá a possibilidade de registrar novos compromissos, editar e excluir os já existentes;
A aplicação deverá disponibilizar a José Pedro a possibilidade de visualizar os compromissos que já passaram e que vão acontecer de forma
separada.

Para os compromissos futuros, deverá ser disponibilizado a possibilidade de filtrá-los por período.
É muito importante aparecer o nome do contato na visualização do compromissos.
 */
namespace e_Agenda.ConsoleApp
{
    internal class Program
    {
        static Notificador notificador = new Notificador();
        static TelaMenuPrincipal menuPrincipal = new TelaMenuPrincipal(notificador);
        static void Main(string[] args)
        {
            while (true)
            {
                TelaBase telaSelecionada = menuPrincipal.ObterTela();

                if (telaSelecionada is null)
                    return;

                string opcaoSelecionada = telaSelecionada.MostrarOpcoes();

                if (telaSelecionada is ITelaCadastravel)
                    GerenciarCadastroBasico(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroTarefa)
                    GerenciarCadastroTarefas(telaSelecionada, opcaoSelecionada);

                else if (telaSelecionada is TelaCadastroContato)
                    GerenciarCadastroContato(telaSelecionada, opcaoSelecionada);
                
                else if (telaSelecionada is TelaCadastroTarefa)
                    GerenciarCadastroCompromisso(telaSelecionada, opcaoSelecionada);
            }
        }

        private static void GerenciarCadastroCompromisso(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroCompromisso telaCadastroCompromisso = telaSelecionada as TelaCadastroCompromisso;

            if (telaCadastroCompromisso is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroCompromisso.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroCompromisso.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroCompromisso.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroCompromisso.VisualizarRegistros("Tela");
        }

        private static void GerenciarCadastroContato(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroContato telaCadastroContato = telaSelecionada as TelaCadastroContato;

            if (telaCadastroContato is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroContato.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroContato.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroContato.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroContato.VisualizarRegistros("Tela");
        }

        private static void GerenciarCadastroTarefas(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            TelaCadastroTarefa telaCadastroTarefa = telaSelecionada as TelaCadastroTarefa;

            if (telaCadastroTarefa is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroTarefa.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroTarefa.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroTarefa.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroTarefa.VisualizarRegistros("Tela");
        }

        private static void GerenciarCadastroBasico(TelaBase telaSelecionada, string opcaoSelecionada)
        {
            ITelaCadastravel telaCadastroBasico = telaSelecionada as ITelaCadastravel;

            if (telaCadastroBasico is null)
                return;

            if (opcaoSelecionada == "1")
                telaCadastroBasico.InserirRegistro();

            else if (opcaoSelecionada == "2")
                telaCadastroBasico.EditarRegistro();

            else if (opcaoSelecionada == "3")
                telaCadastroBasico.ExcluirRegistro();

            else if (opcaoSelecionada == "4")
                telaCadastroBasico.VisualizarRegistros("Tela");

        }
    }
}
