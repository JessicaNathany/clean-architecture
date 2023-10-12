using System.Data;

namespace bookfy.application.Abstractions.Data
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreatedConnection();
    }
}
