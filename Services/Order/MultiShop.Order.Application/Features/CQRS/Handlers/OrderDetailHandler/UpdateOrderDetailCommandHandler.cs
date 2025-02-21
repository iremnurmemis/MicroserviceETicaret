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
    public class UpdateOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommand command)
        {
            var existingOrderDetail= await _repository.GetByIdAsync(command.OrderDetailId);
            if (existingOrderDetail == null)
            {
                throw new Exception("Sipariş detayı bulunamadı.");
            }

          existingOrderDetail.OrderDetailId = command.OrderDetailId;
          existingOrderDetail.OrderingId = command.OrderingId;
          existingOrderDetail.ProductPrice = command.ProductPrice;
          existingOrderDetail.ProductName = command.ProductName;
          existingOrderDetail.ProductTotalPrice = command.ProductTotalPrice;
          existingOrderDetail.ProductAmount = command.ProductAmount;
          existingOrderDetail.ProductId = command.ProductId;


            await _repository.UpdateAsync(existingOrderDetail);

        }

    }
}
