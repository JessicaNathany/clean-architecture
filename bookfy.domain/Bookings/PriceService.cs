using bookfy.domain.Apartaments;

namespace bookfy.domain.Bookings
{
    public class PriceService
    {
        public PricingDetails CalculatePrice(Apartament apartament, DateRange period)
        {
            var currency = apartament.Price.Currency;

            var priceForPeriod = new Money(apartament.Price.Amount * period.LengthInDays, currency);

            decimal percentageUpCharge = 0;

            foreach (var amenity in apartament.Amenities)
            {
                percentageUpCharge += amenity switch
                {
                    Amenity.GardenView or Amenity.MontaimView => 0.05m,
                    Amenity.AirConditioning => 0.01m,
                    Amenity.Parking => 0.01m,
                    _ => 0
                }; ;
            }

            var amenititesUpCharge = Money.Zero(currency);
            
            if(percentageUpCharge > 0)
            {
                amenititesUpCharge = new Money(priceForPeriod.Amount * percentageUpCharge, currency);
            }

            var totalPrice = Money.Zero();

            totalPrice += priceForPeriod;

            if(!apartament.CleaningFee.IsZero())
            {
                totalPrice += apartament.CleaningFee;
            }

            totalPrice += amenititesUpCharge;
            return new PricingDetails(priceForPeriod, apartament.CleaningFee, amenititesUpCharge, totalPrice);
        }
    }
}
