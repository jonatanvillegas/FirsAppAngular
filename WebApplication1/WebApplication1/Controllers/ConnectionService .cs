using Microsoft.Extensions.Configuration;

public interface IConnectionService
{
    string GetConnectionString();
    void SetConnectionString(string connectionString);
}

public class ConnectionService : IConnectionService
{
    private readonly IConfiguration _configuration;

    public ConnectionService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection");
    }

    public void SetConnectionString(string connectionString)
    {
        _configuration.GetSection("ConnectionStrings")["DefaultConnection"] = connectionString;
    }
}
