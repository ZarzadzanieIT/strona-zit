namespace ZIT.Infrastructure.Options;

public enum DatabaseProvider
{
    SQLite,
    InMemory
}

public class DatabaseOptions
{
    public DatabaseProvider DatabaseProvider { get; set; }
}