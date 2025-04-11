namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class PostMaker(string name, string country)
{
    public string Name { get; set; } = name;
    public string Country { get; set; } = country;
}
