using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class Esemenyek
{
    public Guid EsemenyId { get; set; }

    public string Cim { get; set; } = null!;

    public string? BoritoKep { get; set; }

    public string? Leiras { get; set; }

    public int KategoriaId { get; set; }

    public DateTime? Idopont { get; set; }

    public Guid? SzervezoId { get; set; }

    public int? Korhatar { get; set; }

    public string? Statusz { get; set; }

    public DateTime Letrehozva { get; set; }

    public int? VarosId { get; set; }

    public int LikeSzamlalo { get; set; }

    public int DislikeSzamlalo { get; set; }

    public virtual ICollection<EsemenyHozzaszolasok> EsemenyHozzaszolasoks { get; set; } = new List<EsemenyHozzaszolasok>();

    public virtual ICollection<EsemenyInterakcio> EsemenyInterakcios { get; set; } = new List<EsemenyInterakcio>();

    public virtual EsemenyKategoriak Kategoria { get; set; } = null!;

    public virtual Felhasznalok? Szervezo { get; set; }

    public virtual Telepulesek? Varos { get; set; }
}
