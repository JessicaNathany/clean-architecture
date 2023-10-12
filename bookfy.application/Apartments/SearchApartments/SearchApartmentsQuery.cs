using bookfy.application.Abstractions.Messaging;

namespace bookfy.application.Apartments.SearchApartments
{
    public sealed record SearchApartmentsQuery(DateOnly StartDate, DateOnly EndDate)
        : IQuery<IReadOnlyList<ApartmentsResponse>>;
}