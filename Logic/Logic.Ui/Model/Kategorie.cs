using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Videothek.Logic.Ui.Model {

    public class Kategorie : Record<Kategorie> {
        public override string TABLE_NAME => TableNames.Kategorie;

        public int Id { get; set; }
        public string Bezeichnung { get; set; }

        public override List<Kategorie> FromDataTable(DataTable dt) =>
            (from rw in dt.AsEnumerable()
             select new Kategorie() {
                 Id = Convert.ToInt32(rw["ID"]),
                 Bezeichnung = Convert.ToString(rw["Bezeichnung"])
             }).ToList();

        public override Dictionary<string, string> ToDict() {
            var dict = new Dictionary<string, string>();
            dict.Add("Bezeichnung", Bezeichnung);

            return dict;
        }
    }
}
