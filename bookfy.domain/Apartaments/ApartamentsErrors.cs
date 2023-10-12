using bookfy.domain.Abstractions;

namespace bookfy.domain.Apartaments
{
    public static class ApartamentsErrors
    {
        public static Error NotFound = new("Apartament.Found",
           "The apartment with the specified identifier was not found");
    }
}
