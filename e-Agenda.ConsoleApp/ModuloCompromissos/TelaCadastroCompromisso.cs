using e_Agenda.ConsoleApp.Compartilhado;
using e_Agenda.ConsoleApp.ModuloContatos;
using System;
using System.Collections.Generic;

namespace e_Agenda.ConsoleApp.ModuloCompromissos
{
    public class TelaCadastroCompromisso : TelaBase, ITelaCadastravel
    {
        private readonly Notificador _notificador;
        private readonly IRepositorio<Compromisso> _repositorioCompromisso;
        private readonly IRepositorio<Contato> _repositorioContato;
        private readonly TelaCadastroContato _telaCadastroContato;

        public TelaCadastroCompromisso(Notificador notificador, IRepositorio<Compromisso> repositorio, IRepositorio<Contato> repositorioContato, TelaCadastroContato telaCadastroContato)
            : base("Cadastro de Compromissos...")
        {
            this._notificador = notificador;
            this._repositorioCompromisso = repositorio;
            this._repositorioContato = repositorioContato;
            this._telaCadastroContato = telaCadastroContato;

        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Compromisso...");

            bool temCompromissosCadastrados = VisualizarRegistros("Pesquisando...");

            if (temCompromissosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum compromisso cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroCompromisso = ObterNumeroCompromisso();
            Contato contatoSelecionado = ObterContato();
            Compromisso compromissoAtualizada = ObterCompromisso(contatoSelecionado);

            bool conseguiuEditar = _repositorioCompromisso.Editar(x => x.numero == numeroCompromisso, compromissoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);

        }

        private Compromisso ObterCompromisso(Contato contatoSelecionado)
        {
            Console.WriteLine("Digite o assunto do compromisso: ");
            string prioridade = Console.ReadLine();

            Console.WriteLine("Digite o local do compromisso: ");
            string local = Console.ReadLine();

            Console.WriteLine("Digite a data do compromisso: ");
            DateTime dataDeConclusao = DateTime.Parse(Console.ReadLine());
            
            Console.WriteLine("Digite a hora de inicio do compromisso: ");
            DateTime dataInicio = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Digite a hora de termino do compromisso: ");
            DateTime dataTermino = DateTime.Parse(Console.ReadLine());

            Compromisso compromisso = new Compromisso(prioridade,local,dataDeConclusao,dataInicio,dataTermino,contatoSelecionado);

            return compromisso;
        }

        private int ObterNumeroCompromisso()
        {
            int numeroCompromisso;
            bool numeroCompromissoEncontrado;

            do
            {
                Console.Write("Digite o número do compromisso que deseja selecionar: ");
                numeroCompromisso = Convert.ToInt32(Console.ReadLine());

                numeroCompromissoEncontrado = _repositorioCompromisso.ExisteRegistro(x => x.numero == numeroCompromisso);

                if (numeroCompromissoEncontrado == false)
                    _notificador.ApresentarMensagem("Número do compromisso não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroCompromissoEncontrado == false);
            return numeroCompromisso;

        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Empréstimo");

            bool temEmprestimosCadastrados = VisualizarRegistros("Pesquisando");

            if (temEmprestimosCadastrados == false)
            {
                _notificador.ApresentarMensagem(
                    "Nenhum empréstimo cadastrado para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroEmprestimo = ObterNumeroCompromisso();

            bool conseguiuExcluir = _repositorioCompromisso.Excluir(x => x.numero == numeroEmprestimo);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Revista excluída com sucesso", TipoMensagem.Sucesso);
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo novo Compromisso...");
            Contato contatoSelecionado = ObterContato();

            Compromisso comprimisso = ObterCompromisso(contatoSelecionado);

            string statusValidacao = _repositorioCompromisso.Inserir(comprimisso);

            if (statusValidacao == "REGISTRO_VALIDO")
                _notificador.ApresentarMensagem("Empréstimo cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        private Contato ObterContato()
        {
            Console.WriteLine("Digite o nome do contato: ");
            string nome = Console.ReadLine();

            Console.WriteLine("Digite o email do contato: ");
            string email = Console.ReadLine();

            Console.WriteLine("Digite o telefone do contato: ");
            string telefone = Console.ReadLine();

            Console.WriteLine("Digite o nome da empresa do contato: ");
            string empresa = Console.ReadLine();

            Console.WriteLine("Digite o cargo do contato: ");
            string cargo = Console.ReadLine();

            Contato contato = new Contato(nome, email, telefone, empresa, cargo);

            return contato; 
        }

        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de Compromissos");

            List<Compromisso> compromissos = _repositorioCompromisso.SelecionarTodos();

            if (compromissos.Count == 0)
            {
                _notificador.ApresentarMensagem("Não há nenhum compromisso disponível", TipoMensagem.Atencao);
                return false;
            }

            foreach (Compromisso compromisso in compromissos)
                Console.WriteLine(compromisso.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
