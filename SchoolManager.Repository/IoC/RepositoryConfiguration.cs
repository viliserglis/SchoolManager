namespace SchoolManager.Repository.IoC;

public class RepositoryConfiguration
{
    public string ConnectionString { get; set; }
    public bool IsAutoMigrate { get; set; }
}