using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;

namespace e_Agenda.ConsoleApp.ModuloContatos
{
    public class TelaCadastroContato : TelaBase, ITelaCadastravel
    {
        private readonly Notificador _notificador;
        private readonly IRepositorio<Contato> _repositorioContato;

        public TelaCadastroContato(Notificador notificador, IRepositorio<Contato> repositorioContato)
            : base("Cadastro de Contatos...")
        {
            this._notificador = notificador;
            this._repositorioContato = repositorioContato;
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa...");

            bool temContatoCadastrados = VisualizarRegistros("Pesquisando...");

            if (temContatoCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum contato cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroContato = ObterNumeroContato();

            Contato contatoAtualizada = ObterContato();

            bool conseguiuEditar = _repositorioContato.Editar(x => x.numero == numeroContato, contatoAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
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

            Contato contato = new Contato(nome, email,telefone,empresa,cargo);

            return contato;
        }

        private int ObterNumeroContato()
        {
            int numeroContato;
            bool numeroContatoEncontrado;

            do
            {
                Console.Write("Digite o número do contato que deseja selecionar: ");
                numeroContato = Convert.ToInt32(Console.ReadLine());

                numeroContatoEncontrado = _repositorioContato.ExisteRegistro(x => x.numero == numeroContato);

                if (numeroContatoEncontrado == false)
                    _notificador.ApresentarMensagem("Número do contato não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroContatoEncontrado == false);
            return numeroContato;
        }

        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Contato...");

            bool temContatoCadastrados = VisualizarRegistros("Pesquisando...");

            if (temContatoCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhum contato cadastrado para excluir!", TipoMensagem.Atencao);
                return;
            }
            int numeroContato = ObterNumeroContato();

            _repositorioContato.Excluir(x => x.numero == numeroContato);
            _notificador.ApresentarMensagem("Contato excluído com sucesso", TipoMensagem.Sucesso);
        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Novo Contato...");

            Contato novoContato = ObterContato();
            string statusValidacao = _repositorioContato.Inserir(novoContato);
            if (statusValidacao == "REGISTRO_VALIDO")
                _notificador.ApresentarMensagem("Contato cadastrado com sucesso!", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de Contatos...");

            List<Contato> contatos = _repositorioContato.SelecionarTodos();

            if (contatos.Count == 0)
            {
                _notificador.ApresentarMensagem("Não há nenhum contato disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Contato tarefa in contatos)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
