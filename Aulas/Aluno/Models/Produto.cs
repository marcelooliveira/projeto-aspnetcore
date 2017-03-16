﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aluno.Models
{
    public class Produto : BaseModel
    {
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }

        public Produto()
        {

        }

        public Produto(int id, string descricao, decimal preco): this(descricao, preco)
        {
            this.Id = id;
        }

        public Produto(string descricao, decimal preco)
        {
            this.Descricao = descricao;
            this.Preco = preco;
        }
    }
}
