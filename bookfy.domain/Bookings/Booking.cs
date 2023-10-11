using bookfy.domain.Abstractions;
using bookfy.domain.Apartaments;
using bookfy.domain.Bookings.Events;

namespace bookfy.domain.Bookings
{
    public sealed class Booking : Entity
    {
        private Booking(
            Guid id,
            Guid apartamentId,
            Guid userId,
            DateRange duration,
            Money cleaningFee,
            Money priceForPeriod,
            Money amentitiesUpCharge,
            Money totalPrice,
            BookingStatus status,
            DateTime createdOnUtc) : base(id)
        {
            apartamentId = apartamentId;
            UserId = userId;
            Duration = duration;
            CleaningFee = cleaningFee;
            CleaningFee = amentitiesUpCharge;
            TotalPrice = totalPrice;
            Status = status;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid  ApartamentId { get; private set; }

        public Guid UserId { get; private set; }

        public DateRange Duration { get; private set; }

        public Money PriceForPeriod { get; private set; }

        public Money CleaningFee { get; private set; }

        public Money AmenitiesUpCharge { get; private set; }

        public Money TotalPrice { get; private set; }

        public BookingStatus Status { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public DateTime ConfirmedOnUtc { get; private set; }

        public DateTime RejectedOnUtc { get; private set; }

        public DateTime CompleteddOnUtc { get; private set; }

        public DateTime CancelledOnUtc { get; private set; }

        public static Booking Reserve(
            Apartament apartament, 
            Guid userId, 
            DateRange duration, 
            DateTime utcNow, 
            PriceService priceService)
        {
            var pricingDetails = priceService.CalculatePrice(apartament, duration);

            var booking = new Booking(
                Guid.NewGuid(),
                apartament.Id, 
                userId, 
                duration,
                pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee,
                pricingDetails.AmenititesUpCharge,
                pricingDetails.TotalPrice,
                BookingStatus.Reserved,
                utcNow);

            booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

            apartament.LastBookedOnUtc = utcNow;

            return booking;
        }

        public Result Confirm(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
                return Result.Failure(BookingErrors.NotReserved);

            Status = BookingStatus.Confirmed;
            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new BookingReservedDomainEvent(Id));

            return Result.Success();
        }

        public Result Reject(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
                return Result.Failure(BookingErrors.NotReserved);

            Status = BookingStatus.Rejected;
            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new BookingReservedDomainEvent(Id));

            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
                return Result.Failure(BookingErrors.NotReserved);

            Status = BookingStatus.Completed;
            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new BookingReservedDomainEvent(Id));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != BookingStatus.Reserved)
                return Result.Failure(BookingErrors.NotReserved);

            var currentDate = DateOnly.FromDateTime(utcNow);

            if(currentDate > Duration.Start)
                return Result.Failure(BookingErrors.AlreadyStarted);

            Status = BookingStatus.Cancelled;
            ConfirmedOnUtc = utcNow;

            RaiseDomainEvent(new BookingReservedDomainEvent(Id));

            return Result.Success();
        }
    }
}
