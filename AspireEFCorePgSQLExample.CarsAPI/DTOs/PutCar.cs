namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class PutCar(Guid guid, string name, int releaseYear, Guid makerGuid)
{
    public Guid Guid { get; set; } = guid;
    public string Name { get; set; } = name;
    public int ReleaseYear { get; set; } = releaseYear;
    public Guid MakerGuid { get; set; } = makerGuid;
}
