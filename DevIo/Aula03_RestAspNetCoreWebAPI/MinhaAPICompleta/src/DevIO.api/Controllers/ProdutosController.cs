﻿using AutoMapper;
using DevIO.api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DevIO.api.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService,
                                  IEnderecoRepository enderecoRepository,
                                  IMapper mapper,INotificador notificador):base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]

        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null) return NotFound();
            return produtoViewModel;

        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar (ProdutoViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;

            if (!UploadArquivo(produtoViewModel.ImagemUpload,imagemNome))
            {
                return CustomResponse(produtoViewModel);
            }

            produtoViewModel.Imagem = imagemNome;

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }

        [HttpPost("adicionar")]
        public async Task<ActionResult<ProdutoImagemViewModel>> AdicionarAlternativo (ProdutoImagemViewModel produtoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imgPrefixo = Guid.NewGuid() + "_" ;

            if (!await UploadArquivoAlternativo(produtoViewModel.ImagemUpload,imgPrefixo))
            { 
                return CustomResponse(ModelState);
            }

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            return CustomResponse(produtoViewModel);
        }

        [DisableRequestSizeLimit]//Tirando o Limite do tamanho de Request sem abrir brecha com base64
        [RequestSizeLimit(40000000)] // Limitando tamanho para 40 MB
        [HttpPost("imagem")]
        public async Task<ActionResult> AdicionarImagem(IFormFile file)
        {
            return Ok(file);
        }



        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            var produto = await ObterProduto(id);
            if (produto == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse(produto);
            
        }

        [HttpPut("{id:guid}")]

        public async Task<IActionResult> Atualizar(Guid id ,ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) {
                NotificarErro("Os ids informados não são iguais");
                // return NotFound();
                return CustomResponse();
            };

            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (produtoViewModel.ImagemUpload != null)
            {
                var imagemNome = Guid.NewGuid() + "_" + produtoViewModel.Imagem;
                if (!UploadArquivo(produtoViewModel.ImagemUpload,imagemNome))
                {
                    return CustomResponse(ModelState);
                }

                produtoAtualizacao.Imagem = imagemNome;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            return CustomResponse(produtoViewModel);


        }



        private bool UploadArquivo(string arquivo,string imgNome)
        {
            var imageDataByteArray = Convert.FromBase64String(arquivo);

            if (string.IsNullOrEmpty(arquivo))
            {
              //  ModelState.AddModelError(string.Empty, "Forneça uma imagem para este produto!");
                NotificarErro("Forneça uma imagem para este produto!");
                return false;
            }
            //Pegar a combinação do diretorio atual da aplicação , + wwwroot/imagens + o nome da imagem e gerar um path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgNome);

            if (System.IO.File.Exists(filePath))
            {
                //ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome");
                NotificarErro("Já existe um arquivo com este nome");
                return false;
            }
            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);

            return true;
        }

        private async Task<bool> UploadArquivoAlternativo(IFormFile arquivo,string imgPrefixo)
        {

            if (arquivo == null || arquivo.Length <=0)
            {
                //  ModelState.AddModelError(string.Empty, "Forneça uma imagem para este produto!");
                NotificarErro("Forneça uma imagem para este produto!");
                return false;
            }
            //Pegar a combinação do diretorio atual da aplicação , + wwwroot/imagens + o nome da imagem e gerar um path
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName); 

            if (System.IO.File.Exists(path))
            {
                //ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome");
                NotificarErro("Já existe um arquivo com este nome");
                return false;
            }
            using (var stream = new FileStream(path,FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }


        public async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            //var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            //return produto;

            return _mapper.Map< ProdutoViewModel> (await _produtoRepository.ObterProdutoFornecedor(id));
        }






    }
}
