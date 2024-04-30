using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class Megyek
{
    public int MegyeId { get; set; }

    public string Megyenev { get; set; } = null!;

    public virtual ICollection<Telepulesek> Telepuleseks { get; set; } = new List<Telepulesek>();
}
