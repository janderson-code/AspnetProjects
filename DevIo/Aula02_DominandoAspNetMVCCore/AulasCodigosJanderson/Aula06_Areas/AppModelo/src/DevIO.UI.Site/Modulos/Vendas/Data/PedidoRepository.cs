﻿using DevIO.UI.Site.Modulos.Vendas.Models;

namespace DevIO.UI.Site.Modulos.Vendas.Data
{
    public class PedidoRepository : IPedidoRepository
    {
        public Pedido ObterPedido()
        {
            return new Pedido();
        }
    }
}
 