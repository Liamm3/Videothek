using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Videothek.Logic.Ui.Model {

    public class Kunde : Record<Kunde> {
        public override string TABLE_NAME => TableNames.Kunde;
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Strasse { get; set; }
        public string Hausnummer { get; set; }
        public string Ort { get; set; }
        public string Plz { get; set; }

        public override List<Kunde> FromDataTable(DataTable dt) =>
            (from rw in dt.AsEnumerable()
             select new Kunde() {
                 Id = Convert.ToInt32(rw["ID"]),
                 Vorname = Convert.ToString(rw["Vorname"]),
                 Nachname = Convert.ToString(rw["Name"]),
                 Strasse = Convert.ToString(rw["Strasse"]),
                 Hausnummer = Convert.ToString(rw["Hausnummer"]),
                 Ort = Convert.ToString(rw["Ort"]),
                 Plz = Convert.ToString(rw["PLZ"]),
             }).ToList();

        public override Dictionary<string, string> ToDict() {
            var dict = new Dictionary<string, string>();
            dict.Add("Name", Nachname);
            dict.Add("Vorname", Vorname);
            dict.Add("Strasse", Strasse);
            dict.Add("Hausnummer", Hausnummer);
            dict.Add("PLZ", Plz);
            dict.Add("Ort", Ort);

            return dict;
        }
    }
}
