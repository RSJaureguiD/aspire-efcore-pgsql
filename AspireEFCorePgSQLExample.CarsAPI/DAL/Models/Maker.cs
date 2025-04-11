namespace AspireEFCorePgSQLExample.CarsAPI.DAL.Models;

public class Maker
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public DateTime? Update { get; set; }
    public DateTime? Insert { get; set; }

    public virtual IList<Car> Cars { get; set; } = [];

    public Maker(Guid guid, string name, string country)
    {
        Guid = guid;
        Name = name;
        Country = country;
    }
}
