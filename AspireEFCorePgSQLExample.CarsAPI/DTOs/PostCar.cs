namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class PostCar
{
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public Guid MakerGuid { get; set; }
}
