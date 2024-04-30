using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class Telepulesek
{
    public int TelepulesId { get; set; }

    public string TelepulesNev { get; set; } = null!;

    public int? MegyeId { get; set; }

    public int Iranyitoszam { get; set; }

    public virtual ICollection<Esemenyek> Esemenyeks { get; set; } = new List<Esemenyek>();

    public virtual ICollection<Felhasznalok> Felhasznaloks { get; set; } = new List<Felhasznalok>();

    public virtual Megyek? Megye { get; set; }
}
