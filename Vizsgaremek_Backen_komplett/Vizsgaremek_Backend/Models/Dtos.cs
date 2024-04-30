namespace Vizsgaremek_Backend.Models
{
    public record FelhasznalokDto(Guid Id,string Felhasznalonev,string JelszoHash, string Salt,string Email, string Avatar, string Telefonszam,string Leiras,DateTime Letrehozva,int SzerepId,int VarosId,int LikeSzamlalo);

    public record TelepulesekDto(int TelepulesId, string TelepulesNev, int? MegyeId, int Iranyitoszam);

    public record SzerepekDto(int SzerepId, string Szerepnev);

    public record MegyekDto(int MegyeId, string Megyenev);

    public record EsemenyKategoriakDto(int KategoriaId, string KategoriaNev);

    public record ErdekeletEsemenyKategoriakDto(Guid FelhasznaloId, int KategoriaId, int? KategoriaPont);

    public record EsemenyInterakcioDto(int InterakcioId, Guid? FelhasznaloId, Guid? EsemenyId, bool JelentkezettE, bool KedveltE, bool MentettE, DateTime? JelentkezesDatum);

    public record EsemenyHozzaszolasokDto(Guid HozzaszolasId, Guid? EsemenyId, Guid? HozzaszoloId, string? HozzaszolasSzoveg, DateTime Letrehozva);

    public record EsemenyekDto(Guid EsemenyId, string Cim, string? BoritoKep, string? Leiras, int KategoriaId, DateTime? Idopont, Guid? SzervezoId, int? Korhatar, string? Statusz, DateTime Letrehozva, int? VarosId, int LikeSzamlalo, int DislikeSzamlalo);

    public record Dto(Guid FelhasznaloId, int KategoriaId, int? KategoriaPont);


}
