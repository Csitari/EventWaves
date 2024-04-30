using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class Szerepek
{
    public int SzerepId { get; set; }

    public string Szerepnev { get; set; } = null!;

    public virtual ICollection<Felhasznalok> Felhasznaloks { get; set; } = new List<Felhasznalok>();
}
