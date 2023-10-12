using bookfy.application.Abstractions.Clock;
using bookfy.application.Abstractions.Messaging;
using bookfy.domain.Abstractions;
using bookfy.domain.Apartaments;
using bookfy.domain.Bookings;
using bookfy.domain.Users;

namespace bookfy.application.Bookings.ReserveBooking
{
    internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IApartamentRepository _apartmentRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PricingService _picingService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public ReserveBookingCommandHandler(
            IUserRepository userRepository, 
            IApartamentRepository apartmentRepository, 
            IBookingRepository bookingRepository, 
            IUnitOfWork unitOfWork, 
            PricingService picingService,
            IDateTimeProvider dateTimeProvider)
        {
            _userRepository = userRepository;
            _apartmentRepository = apartmentRepository;
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _picingService = picingService;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
                return Result.Failure<Guid>(UsersErrors.NotFound);

            var apartment = await _apartmentRepository.GetByIdAsync(request.ApartmentId, cancellationToken);

            if(apartment == null)
                return Result.Failure<Guid>(ApartamentsErrors.NotFound);

            var duration = DateRange.Create(request.StartDate, request.EndDate);

            if (await _bookingRepository.IsOverlappingAsync(apartment, duration, cancellationToken))
            {
                return Result.Failure<Guid>(BookingErrors.Overlap);
            }

            var booking = Booking.Reserve(apartment, user.Id, duration, _dateTimeProvider.UtcNow, _picingService);

            await _bookingRepository.Add(booking);

            return booking.Id;
        }
    }
}
