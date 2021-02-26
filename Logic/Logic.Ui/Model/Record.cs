using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videothek.Logic.Ui.Interfaces;

namespace Videothek.Logic.Ui.Model {

    public abstract class Record<T> : IRecordable {
        public abstract string TABLE_NAME { get; }

        public abstract List<T> FromDataTable(DataTable dt);

        public abstract Dictionary<string, string> ToDict();
    }
}