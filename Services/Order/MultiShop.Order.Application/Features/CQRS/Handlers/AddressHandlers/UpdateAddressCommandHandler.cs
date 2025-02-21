using AutoMapper;
using MultiShop.Order.Application.Features.CQRS.Commands.AddressCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class UpdateAddressCommandHandler
    {
        private readonly IRepository<Address> _repository;

        public UpdateAddressCommandHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }
        
        public async Task Handle(UpdateAddressCommand command)
        {
            var existingAddress = await _repository.GetByIdAsync(command.AddressId);
            if (existingAddress == null)
            {
                throw new Exception("Adres bulunamadı.");
            }

            existingAddress.AddressId = command.AddressId;
            existingAddress.UserId = command.UserId;
            existingAddress.Detail = command.Detail;
            existingAddress.City = command.City;
            existingAddress.District = command.District;
             
            await _repository.UpdateAsync(existingAddress);

        }

    }
}
