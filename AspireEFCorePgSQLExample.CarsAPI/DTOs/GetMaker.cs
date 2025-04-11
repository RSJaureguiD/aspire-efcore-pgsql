namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class GetMaker(Guid guid, string name, string country)
{
    public Guid Guid { get; set; } = guid;
    public string Name { get; set; } = name;
    public string Country { get; set; } = country;
}
