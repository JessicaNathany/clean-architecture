using bookfy.domain.Apartaments;
using bookfy.domain.Bookings;
using bookfy.domain.Shared;
using bookfy.domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bookfy.infrastructure.Configurations
{
    internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");

            builder.HasKey(apartment => apartment.Id);

            builder.OwnsOne(apartment => apartment.PriceForPeriod, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.OwnsOne(apartment => apartment.CleaningFee, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.OwnsOne(apartment => apartment.TotalPrice, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.OwnsOne(apartment => apartment.AmenitiesUpCharge, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.HasOne(booking => booking.Duration);

            builder.HasOne<Apartament>()
                .WithMany()
                .HasForeignKey(booking => booking.ApartamentId);

            builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(booking => booking.UserId);
        }
    }
}
