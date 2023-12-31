﻿using bookfy.application.Abstractions.Data;
using bookfy.application.Abstractions.Messaging;
using bookfy.domain.Abstractions;
using Dapper;
using MediatR;

namespace bookfy.application.Bookings.GetBookings
{
    internal sealed class GetBookingQueryHandler : IQueryHandler<GetBookingQuery, BookingResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetBookingQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        async Task<Result<BookingResponse>> IRequestHandler<GetBookingQuery, Result<BookingResponse>>.Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreatedConnection();

            const string sql = """
                SELECT
                id AS Id,
                apartment_id AS ApartmentId,
                user_id AS UserId,
                status AS Status,
                price_for_period_amount AS PriceAmount,
                price_for_period_currency AS PriceCurrency,
                cleaning_fee_amount AS CleaningFeeAmount,
                cleaning_fee_currency AS CleaningFeeCurrency,
                amenities_up_charge_amount AS AmenitiesUpChargeAmount,
                amenities_up_charge_currency AS AmenitiesUpChargeCurrency,
                total_price_amount AS TotalPriceAmount,
                total_price_currency AS TotalPriceCurrency,
                duration_start AS DurationStart
                duration_End AS DurationEnd,
                created_on-utc AS CreatedOnUtc
                FROM bookings
                WHERE id = @bookingId
                """;

            var booking = await connection.QueryFirstOrDefaultAsync<BookingResponse>(sql, new { request.BookingID });

            return booking;
        }
    }
}
