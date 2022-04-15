using System;
using e_Agenda.ConsoleApp.ModuloTarefa;
using e_Agenda.ConsoleApp.ModuloContatos;
using e_Agenda.ConsoleApp.ModuloCompromissos;


namespace e_Agenda.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private string opcaoSelecionada;

        private IRepositorio<Tarefa> _repositorioTarefa;
        private TelaCadastroTarefa _telaCadastroTarefa;
        private IRepositorio<Contato> _repositorioContato;
        private TelaCadastroContato _telaCadastroContato;
        private IRepositorio<Compromisso> _repositorioCompromisso;
        private TelaCadastroCompromisso _telaCadastroCompromisso;

        public TelaMenuPrincipal(Notificador notificador)
        {
            _repositorioTarefa = new RepositorioTarefa();
            _repositorioContato = new RepositorioContato();
            _repositorioCompromisso = new RepositorioCompromisso();

            _telaCadastroTarefa = new TelaCadastroTarefa(notificador, _repositorioTarefa);
            _telaCadastroContato = new TelaCadastroContato(notificador, _repositorioContato);
            _telaCadastroCompromisso = new TelaCadastroCompromisso(notificador,_repositorioCompromisso,_repositorioContato,_telaCadastroContato);
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("e-Agenda...\n");

            Console.WriteLine("-Digite 1 para Gerenciar Tarefas. ");
            Console.WriteLine("-Digite 2 para Gerenciar Contatos. ");
            Console.WriteLine("-Digite 3 para Gerenciar Compromissos. ");
            Console.WriteLine("-Digite s para sair.\n");

            Console.Write("Digite a opção desejada: ");
            opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }
        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = _telaCadastroTarefa;

            else if (opcao == "2")
                tela = _telaCadastroContato;

            else if (opcao == "3")
                tela = _telaCadastroCompromisso;


            return tela;
        }
    }
}
