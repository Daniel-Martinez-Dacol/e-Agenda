﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.Compartilhado
{
    public interface IRepositorio<T> where T : EntidadeBase
    {
        string Inserir(T entidade);
        bool Editar(Predicate<T> condicao, T novaEntidade);
        bool Excluir(Predicate<T> condicao);
        bool ExisteRegistro(Predicate<T> condicao);
        List<T> SelecionarTodos();
        List<T> Filtrar(Predicate<T> condicao);
        T SelecionarRegistro(Predicate<T> condicao);

    }
}
