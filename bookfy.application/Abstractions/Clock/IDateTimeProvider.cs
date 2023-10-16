namespace bookfy.application.Abstractions.Clock
{
    public  interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
