using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizsgaremek_WPF
{
    public class Esemenyek
    {
        public Guid EsemenyId { get; set; }
        public string Cim { get; set; }
        public string? BoritoKep { get; set; }
        public string? Leiras { get; set; }
        public int KategoriaId { get; set; }
        public DateTime? Idopont { get; set; }
        public Guid? SzervezoId { get; set; }
        public DateTime Letrehozva { get; set; }
        public int? VarosId { get; set; }
        public int LikeSzamlalo { get; set; }
        public int DislikeSzamlalo { get; set; }

        // Konstruktor
        public Esemenyek(Guid esemenyId, string cim, string boritoKep, string leiras, int kategoriaId, DateTime idopont, Guid szervezoId, DateTime letrehozva, int varosId, int likeSzamlalo, int dislikeSzamlalo)
        {
            EsemenyId = esemenyId;
            Cim = cim;
            BoritoKep = boritoKep;
            Leiras = leiras;
            KategoriaId = kategoriaId;
            Idopont = idopont;
            SzervezoId = szervezoId;
            Letrehozva = letrehozva;
            VarosId = varosId;
            LikeSzamlalo = likeSzamlalo;
            DislikeSzamlalo = dislikeSzamlalo;
        }

        // Alapértelmezett konstruktor
        public Esemenyek()
        {
        }
    }
}
