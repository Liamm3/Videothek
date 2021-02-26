using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Videothek.Logic.Ui.Model {

    public class Artikel : Record<Artikel> {
        public override string TABLE_NAME => TableNames.Artikel;
        public int Id { get; set; }
        public int KategorieId { get; set; }
        public string Bezeichnung { get; set; }
        public int Menge { get; set; }
        public double Leihpreis { get; set; }

        public override List<Artikel> FromDataTable(DataTable dt) =>
            (from rw in dt.AsEnumerable()
             select new Artikel() {
                 Id = Convert.ToInt32(rw["ID"]),
                 KategorieId = Convert.ToInt32(rw["KategorieID"]),
                 Bezeichnung = Convert.ToString(rw["Bezeichnung"]),
                 Menge = Convert.ToInt32(rw["Menge"]),
                 Leihpreis = Convert.ToDouble(rw["Leihpreis"])
             }).ToList();

        public override Dictionary<string, string> ToDict() {
            var dict = new Dictionary<string, string>();
            dict.Add("KategorieID", KategorieId.ToString());
            dict.Add("Bezeichnung", Bezeichnung);
            dict.Add("Menge", Menge.ToString());
            dict.Add("Leihpreis", Leihpreis.ToString());

            return dict;
        }
    }
}
