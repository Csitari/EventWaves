using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class EsemenyInterakcio
{
    public int InterakcioId { get; set; }

    public Guid? FelhasznaloId { get; set; }

    public Guid? EsemenyId { get; set; }

    public bool JelentkezettE { get; set; }

    public bool KedveltE { get; set; }

    public bool MentettE { get; set; }

    public DateTime? JelentkezesDatum { get; set; }

    public virtual Esemenyek? Esemeny { get; set; }

    public virtual Felhasznalok? Felhasznalo { get; set; }
}
