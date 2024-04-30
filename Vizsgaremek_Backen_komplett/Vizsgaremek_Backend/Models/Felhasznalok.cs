using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class Felhasznalok
{
    public Guid FelhasznaloId { get; set; }

    public string Felhasznalonev { get; set; } = null!;

    public string JelszoHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Avatar { get; set; }

    public string? Telefonszam { get; set; }

    public string? Leiras { get; set; }

    public DateTime Letrehozva { get; set; }

    public int? SzerepId { get; set; }

    public int? VarosId { get; set; }

    public int LikeSzamlalo { get; set; }



    public virtual ICollection<EsemenyHozzaszolasok> EsemenyHozzaszolasoks { get; set; } = new List<EsemenyHozzaszolasok>();

    public virtual ICollection<EsemenyInterakcio> EsemenyInterakcios { get; set; } = new List<EsemenyInterakcio>();

    public virtual ICollection<Esemenyek> Esemenyeks { get; set; } = new List<Esemenyek>();

    public virtual Szerepek? Szerep { get; set; }

    public virtual Telepulesek? Varos { get; set; }
}
