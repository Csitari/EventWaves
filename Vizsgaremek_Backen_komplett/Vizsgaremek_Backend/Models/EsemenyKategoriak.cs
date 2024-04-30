using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class EsemenyKategoriak
{
    public int KategoriaId { get; set; }

    public string KategoriaNev { get; set; } = null!;

    public virtual ICollection<Esemenyek> Esemenyeks { get; set; } = new List<Esemenyek>();
}
