using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class EsemenyHozzaszolasok
{
    public Guid HozzaszolasId { get; set; }

    public Guid? EsemenyId { get; set; }

    public Guid? HozzaszoloId { get; set; }

    public string? HozzaszolasSzoveg { get; set; }

    public DateTime Letrehozva { get; set; }

    public virtual Esemenyek? Esemeny { get; set; }

    public virtual Felhasznalok? Hozzaszolo { get; set; }
}
