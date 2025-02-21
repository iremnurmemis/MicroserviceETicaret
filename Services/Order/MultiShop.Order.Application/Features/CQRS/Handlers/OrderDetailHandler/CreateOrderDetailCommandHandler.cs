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
    public class CreateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public CreateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateOrderDetailCommand command)
        {
            await _repository.CreateAsync(new OrderDetail
            {
                ProductTotalPrice = command.ProductTotalPrice,
                ProductName = command.ProductName,
                ProductPrice = command.ProductPrice,
                ProductId = command.ProductId,
                ProductAmount = command.ProductAmount,
                OrderingId = command.OrderingId,
                
            });

        }

    }
}
