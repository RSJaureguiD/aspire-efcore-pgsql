namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class PostCar(string name, int releaseYear, Guid makerGuid)
{
    public string Name { get; set; } = name;
    public int ReleaseYear { get; set; } = releaseYear;
    public Guid MakerGuid { get; set; } = makerGuid;
}
