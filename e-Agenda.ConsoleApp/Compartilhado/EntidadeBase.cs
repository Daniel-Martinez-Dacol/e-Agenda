﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Agenda.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase
    {
        public int numero;

        public abstract ResultadoValidacao Validar();
    }
}
