using MediatR;
using MultiShop.Order.Application.Features.Mediator.Command.OrderingCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;

namespace MultiShop.Order.Application.Features.Mediator.Handlers.OrderingHandlers
{
    public class UpdateOrderingCommandHandler : IRequestHandler<UpdateOrderingCommand>
    {
        private readonly IRepository<Ordering> _repository;

        public UpdateOrderingCommandHandler(IRepository<Ordering> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderingCommand request, CancellationToken cancellationToken)
        {
            var value=await _repository.GetByIdAsync(request.OrderingId);
            if (value != null)
            {
                value.OrderDate=request.OrderDate;
                value.TotalPrice=request.TotalPrice;
                value.UserId=request.UserId;

                await _repository.UpdateAsync(value);
            }
        }
    }
}
