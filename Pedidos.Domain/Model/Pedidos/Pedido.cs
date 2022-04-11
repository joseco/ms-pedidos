﻿using Pedidos.Domain.Event;
using Pedidos.Domain.Model.Pedidos.ValueObjects;
using Pedidos.Domain.ValueObjects;
using ShareKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Domain.Model.Pedidos
{
    public class Pedido : AggregateRoot<Guid>
    {
        public Guid ClienteId { get; private set; }
        public NumeroPedido NroPedido { get; private set; }
        public PrecioValue Total { get; private set; }
        public ICollection<DetallePedido> Detalle { get; private set; }

        public Pedido(string nroPedido)
        {
            Id = Guid.NewGuid();
            NroPedido = nroPedido;
            Total = new PrecioValue(0m);
            Detalle = new List<DetallePedido>();
        }

        public void AgregarItem(Guid productoId, int cantidad, decimal precio, string instrucciones)
        {
            var detallePedido = Detalle.FirstOrDefault(x => x.ProductoId == productoId);
            if (detallePedido is null)
            {
                detallePedido = new DetallePedido(productoId, instrucciones, cantidad, precio);
                Detalle.Add(detallePedido);
            }
            else
            {
                detallePedido.ModificarPedido(cantidad, precio);
            }

            Total = Total + detallePedido.SubTotal;

            AddDomainEvent(new ItemPedidoAgregado(productoId, precio, cantidad));
        }

        public void ConsolidarPedido()
        {
            var evento = new PedidoCreado(Id, NroPedido);
            AddDomainEvent(evento);
        }
    }
}
