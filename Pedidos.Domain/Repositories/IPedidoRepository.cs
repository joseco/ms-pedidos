﻿using Pedidos.Domain.Model.Pedidos;
using ShareKernel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido, Guid>
    {
        Task UpdateAsync(Pedido obj);
    }
}
