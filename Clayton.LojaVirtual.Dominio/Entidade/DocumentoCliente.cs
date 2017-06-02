﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Clayton.LojaVirtual.Dominio.Entidade
{
    public class DocumentoCliente
    {
        [Key, ForeignKey("Id")]
        public virtual Cliente Cliente { get; set; }

        public string Id { get; set; }
        public TipoDocumento Tipo { get; set; }
        public long Valor { get; set; }
        public DateTime DataNascimento { get; set; }
    }

    public enum TipoDocumento
    {
        CPF
    }
}
