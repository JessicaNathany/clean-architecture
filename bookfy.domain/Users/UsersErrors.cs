using bookfy.domain.Abstractions;

namespace bookfy.domain.Users
{
    public static  class UsersErrors // review this errors messages
    {
        public static Error NotFound = new("User.Found",
            "The apartment with the specified identifier was not found");
    }
}
