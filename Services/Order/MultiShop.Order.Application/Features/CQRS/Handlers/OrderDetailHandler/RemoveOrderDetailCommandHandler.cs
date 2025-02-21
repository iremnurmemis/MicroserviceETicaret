using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailsCommand;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetailResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandler
{
    public class RemoveOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveOrderDetailByIdCommand command)
        {
            var value=await _repository.GetByIdAsync(command.Id);
            if (value != null)
            {
                await _repository.DeleteAsync(value);
            }

        }

    }
}
