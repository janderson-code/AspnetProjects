﻿using DevIO.api.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DevIO.api.ViewModels
{

    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder),Name= "Produto")]
    public class ProdutoImagemViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [StringLength(200, ErrorMessage = " O campo {0} precisa ter entre {2} e { 1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [StringLength(1000, ErrorMessage = " O campo {0} precisa ter entre {2} e { 1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }


        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
       
        public decimal Valor { get; set; }
         
        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        [ScaffoldColumn(false)]
        public string NomeFornecedor { get; set; }


    }
}