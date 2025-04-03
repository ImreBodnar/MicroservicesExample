using MediatR;

namespace BuildingBlocks.CQRS
{
    public interface ICommand<out Tresponse> : IRequest<Tresponse>
    {
    }

    public interface ICommand : ICommand<Unit>
    {
    }
}