using FluentValidation;
using YakShop.Core.Commands;

namespace YakShop.Core.Validation
{
    public class OrderValidator : AbstractValidator<OrderCommand>
    {
        public OrderValidator() 
        {
            RuleFor(command => command.Customer).NotEmpty();

            RuleFor(command => command.Order.Skins)
            .GreaterThanOrEqualTo(0);

            RuleFor(command => command.Order.Milk)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x)
            .Must(command => command.Order.Skins > 0 || command.Order.Milk > 0)
            .WithMessage("Order must contain at least some Milk or Skins.");
        }
    }
}
