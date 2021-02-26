using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Videothek.Logic.Ui.Model {

    internal class ArtikelAusgeliehen : Record<ArtikelAusgeliehen> {
        public override string TABLE_NAME => TableNames.AusgelieheneArtikel;
        public int Id { get; set; }
        public int ArtikelId { get; set; }
        public int KundeId { get; set; }
        public DateTime Leihdatum { get; set; }
        public DateTime Abgabedatum { get; set; }

        public override List<ArtikelAusgeliehen> FromDataTable(DataTable dt) =>
            (from rw in dt.AsEnumerable()
             select new ArtikelAusgeliehen() {
                 Id = Convert.ToInt32(rw["ID"]),
                 ArtikelId = Convert.ToInt32(rw["ArtikelID"]),
                 KundeId = Convert.ToInt32(rw["KundeID"]),
                 Leihdatum = Convert.ToDateTime(rw["Leihdatum"]),
                 Abgabedatum = Convert.ToDateTime(rw["Abgabedatum"])
             }).ToList();

        public override Dictionary<string, string> ToDict() {
            var dict = new Dictionary<string, string>();
            dict.Add("ArtikelID", ArtikelId.ToString());
            dict.Add("KundeID", KundeId.ToString());
            dict.Add("Abgabedatum", Abgabedatum.ToString());
            dict.Add("Leihdatum", Leihdatum.ToString());

            return dict;
        }
    }
}
