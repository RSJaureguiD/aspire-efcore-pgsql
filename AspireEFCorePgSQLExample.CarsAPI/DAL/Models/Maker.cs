namespace AspireEFCorePgSQLExample.CarsAPI.DAL.Models;

public class Maker
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public DateTime? Update { get; set; }
    public DateTime? Insert { get; set; }

    public IEnumerable<Car> Cars { get; } = new List<Car>();

    public Maker(Guid guid, string name, string country)
    {
        Guid = guid;
        Name = name;
        Country = country;
    }
}
