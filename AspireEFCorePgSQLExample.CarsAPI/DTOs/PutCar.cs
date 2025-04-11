namespace AspireEFCorePgSQLExample.CarsAPI.DTOs;

public class PutCar
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public Guid MakerGuid { get; set; }
}
