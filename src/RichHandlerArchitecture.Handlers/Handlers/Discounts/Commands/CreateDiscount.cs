using RichHandlerArchitecture.Domain.Discounts;
using RichHandlerArchitecture.Handlers.Abstractions;
using RichHandlerArchitecture.Handlers.Handlers.Orders.Models;

namespace RichHandlerArchitecture.Handlers.Handlers.Discounts.Commands;

public static class CreateDiscount
{
    public sealed record Command(string DiscountCode, DateTime ValidUntilUtc) : ICommand<IHandlerResponse>;

    public sealed record Response(OrderListModel Order);

    public sealed class CommandValidator : Validator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.DiscountCode)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.ValidUntilUtc)
                .GreaterThan(DateTime.UtcNow)
                .NotEmpty();
        }
    }

    public sealed class Handler(IHandlerContext context) : CommandHandler<Command>(context)
    {
        public override async Task<IHandlerResponse> ExecuteAsync(Command command, CancellationToken ct)
        {
            var discount = new Discount(command.DiscountCode, command.ValidUntilUtc);

            await DbContext.Discounts.AddAsync(discount, ct);
            await DbContext.SaveChangesAsync();

            return Success();
        }
    }
}