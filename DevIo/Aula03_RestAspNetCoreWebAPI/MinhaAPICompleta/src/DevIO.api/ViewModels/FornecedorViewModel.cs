﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevIO.api.ViewModels
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [StringLength(50, ErrorMessage = " O campo {0} precisa ter entre {2} e { 1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Documento { get; set; }

        public int TipoFornecedor { get; set; }

        public EnderecoViewModel MyProperty { get; set; }

        public bool Ativo { get; set; }

       public IEnumerable<ProdutoViewModel> Produtos { get; set; }

    }
}
