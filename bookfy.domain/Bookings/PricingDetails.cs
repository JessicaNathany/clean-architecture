using bookfy.domain.Apartaments;
namespace bookfy.domain.Bookings
{
    public record PricingDetails(Money PriceForPeriod, Money CleaningFee, Money AmenititesUpCharge, Money TotalPrice);
}
