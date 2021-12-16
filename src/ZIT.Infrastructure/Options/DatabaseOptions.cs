namespace ZIT.Infrastructure.Options;

public enum DatabaseProvider
{
    Sqlite,
    InMemory
}

public class DatabaseOptions
{
    public DatabaseProvider DatabaseProvider { get; set; }
}