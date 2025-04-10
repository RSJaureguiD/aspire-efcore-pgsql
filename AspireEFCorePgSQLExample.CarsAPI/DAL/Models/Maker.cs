namespace AspireEFCorePgSQLExample.CarsAPI.DAL.Models;

public class Maker
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }

    public virtual IEnumerable<Car> Cars { get; set; } = Enumerable.Empty<Car>();

    public Maker(Guid guid, string name, string country)
    {
        Guid = guid;
        Name = name;
        Country = country;
    }
}
