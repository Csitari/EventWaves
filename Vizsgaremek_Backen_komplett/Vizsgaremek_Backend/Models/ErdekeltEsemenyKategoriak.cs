using System;
using System.Collections.Generic;

namespace Vizsgaremek_Backend.Models;

public partial class ErdekeltEsemenyKategoriak
{
    public Guid FelhasznaloId { get; set; }

    public int KategoriaId { get; set; }

    public int? KategoriaPont { get; set; }

    public virtual Felhasznalok Felhasznalo { get; set; } = null!;

    public virtual EsemenyKategoriak Kategoria { get; set; } = null!;
}
