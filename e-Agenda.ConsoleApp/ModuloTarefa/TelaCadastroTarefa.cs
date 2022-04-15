using e_Agenda.ConsoleApp.Compartilhado;
using System;
using System.Collections.Generic;


namespace e_Agenda.ConsoleApp.ModuloTarefa
{
    public class TelaCadastroTarefa : TelaBase, ITelaCadastravel
    {
        private readonly Notificador _notificador;
        private readonly IRepositorio<Tarefa> _repositorioTarefa;

        public TelaCadastroTarefa(Notificador notificador, IRepositorio<Tarefa> repositorioTarefa)
            : base("Cadastro de Tarefas...")
        {
            this._notificador = notificador;
            this._repositorioTarefa = repositorioTarefa;
        }

        public void EditarRegistro()
        {
            MostrarTitulo("Editando Tarefa...");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando...");

            if (temTarefasCadastradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrado para poder editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroTarefa = ObterNumeroTarefa();

            Tarefa tarefaAtualizada = ObterTarefa();

            bool conseguiuEditar = _repositorioTarefa.Editar(x => x.numero == numeroTarefa, tarefaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Tarefa editada com sucesso", TipoMensagem.Sucesso);
        }


        public void ExcluirRegistro()
        {
            MostrarTitulo("Excluindo Tarefa...");

            bool temTarefasCadastradas = VisualizarRegistros("Pesquisando...");

            if (temTarefasCadastradas == false)
            {
                _notificador.ApresentarMensagem("Nenhuma tarefa cadastrada para excluir!",TipoMensagem.Atencao);
                return;
            }
            int numeroTarefa = ObterNumeroTarefa();

            _repositorioTarefa.Excluir(x => x.numero == numeroTarefa);
            _notificador.ApresentarMensagem("Tarefa excluída com sucesso", TipoMensagem.Sucesso);
        }

        private int ObterNumeroTarefa()
        {
            int numeroTarefa;
            bool numeroTarefaEncontrado;

            do
            {
                Console.Write("Digite o número da tarefa que deseja selecionar: ");
                numeroTarefa = Convert.ToInt32(Console.ReadLine());

                numeroTarefaEncontrado = _repositorioTarefa.ExisteRegistro(x => x.numero == numeroTarefa);

                if (numeroTarefaEncontrado == false)
                    _notificador.ApresentarMensagem("Número de tarefa não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroTarefaEncontrado == false);
            return numeroTarefa;

        }

        public void InserirRegistro()
        {
            MostrarTitulo("Inserindo Nova Tarefa...");

            Tarefa novaTarefa = ObterTarefa();
            string statusValidacao = _repositorioTarefa.Inserir(novaTarefa);
            if (statusValidacao == "REGISTRO_VALIDO")
                _notificador.ApresentarMensagem("Caixa cadastrada com sucesso!", TipoMensagem.Sucesso);
            else
                _notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
        }

        private Tarefa ObterTarefa()
        {
            Console.WriteLine("Digite prioridade da tarefa: ");
            string prioridade = Console.ReadLine();

            Console.WriteLine("Digite o titulo da tarefa: ");
            string titulo = Console.ReadLine();

            Console.WriteLine("Digite a data para a conclusão da tarefa: ");
            DateTime dataDeConclusao = DateTime.Parse(Console.ReadLine());
            DateTime dataDeCriacao = DateTime.Today;

            List<String> novaLista = CriarNovaLista();

            int precentualDeConclusao = 0;
            
            bool pendencia = true;

            Tarefa tarefa = new Tarefa(prioridade,titulo,dataDeCriacao,dataDeConclusao,precentualDeConclusao,novaLista, pendencia);

            return tarefa;
        }
        private List<String> CriarNovaLista()
        {

            List<String> listaDaTerefa = new List<String>();

            bool addItens = true;

            while (addItens == false)
            {

                Console.WriteLine("Digite a item da tarefa que deve ser feita: ");
                string descricaoDoItem = Console.ReadLine();
                listaDaTerefa.Add(descricaoDoItem);

                Console.WriteLine("Deseja adicionar mais algum item a tarefa?(s/n)");
                string opcao = Console.ReadLine();
                if (opcao == "s")
                    addItens = false;
                
            }

            return listaDaTerefa;
        }
        public bool VisualizarRegistros(string tipoVisualizado)
        {
            if (tipoVisualizado == "Tela")
                MostrarTitulo("Visualização de Tarefas...");

            List<Tarefa> tarefas = _repositorioTarefa.SelecionarTodos();

            if (tarefas.Count == 0)
            {
                _notificador.ApresentarMensagem("Não há nenhuma tarefa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Tarefa tarefa in tarefas)
                Console.WriteLine(tarefa.ToString());

            Console.ReadLine();

            return true;
        }
    }
}
