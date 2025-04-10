﻿namespace AspireEFCorePgSQLExample.CarsAPI.DAL.Models;

public class Car
{
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public int ReleaseYear { get; set; }
    public Guid MakerGuid { get; set; }
    public DateTime Update { get; set; }
    public DateTime Insert { get; set; }


    public virtual Maker Maker { get; set; } = null!;

    public Car(Guid guid, string name, int releaseYear, Guid makerGuid, DateTime update, DateTime insert)
    {
        Guid = guid;
        Name = name;
        ReleaseYear = releaseYear;
        MakerGuid = makerGuid;
        Update = update;
        Insert = insert;
    }
}
