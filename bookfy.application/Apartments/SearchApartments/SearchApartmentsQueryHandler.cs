using bookfy.application.Abstractions.Data;
using bookfy.application.Abstractions.Messaging;
using bookfy.domain.Abstractions;
using bookfy.domain.Bookings;
using Dapper;

namespace bookfy.application.Apartments.SearchApartments
{
    internal sealed class SearchApartmentsQueryHandler
        : IQueryHandler<SearchApartmentsQuery, IReadOnlyList<ApartmentsResponse>>
    {
        private static readonly int[] ActiveBookingStatuses =
        {
            (int)BookingStatus.Reserved,
            (int)BookingStatus.Confirmed,
            (int)BookingStatus.Completed,
        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public SearchApartmentsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyList<ApartmentsResponse>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {
            if (request.StartDate > request.EndDate)
                return new List<ApartmentsResponse>();

            using var connection = _sqlConnectionFactory.CreatedConnection();

            const string sql = """
             SELECT 
                a.id AS Id,
                a.name AS Name,
                a.description AS Description,
                a.price_amount AS Price,
                a.price_currency AS Currency,
                a.address_state AS State,
                a.address_zip_code AS ZipCode,
                a.address_city As City,
                a.address_street AS Street,
             FROM  apartments AS a
             WHERE NOT EXISTS
             (
                SELECT 1
                FROM bookings as b
                WHERE 
                    b.apartments_id = a.id AND
                    b.duration_start <= @EndDate AND
                    b.duration_end   >= @StartDate AND
                    b.status = ANY(@ActiveBookinStatuses)
             )
             """;

            var apartments = await connection.QueryAsync<ApartmentsResponse, AddressResponse, ApartmentsResponse>(
                sql, (apartment, address) =>
                {
                    apartment.Address = address;
                    return apartment;
                },
                new
                {
                    request.StartDate,
                    request.EndDate,
                    ActiveBookingStatuses
                },
                splitOn: "Country");

            return apartments.ToList();
        }
    }
}
