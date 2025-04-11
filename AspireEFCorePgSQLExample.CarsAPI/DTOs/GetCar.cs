namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class GetCar(Guid guid, string name, int releaseYear, GetMaker maker)
{
    public Guid Guid { get; set; } = guid;
    public string Name { get; set; } = name;
    public int ReleaseYear { get; set; } = releaseYear;
    public GetMaker Maker { get; set; } = maker;
}
