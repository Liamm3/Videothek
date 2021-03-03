using System.Collections.Generic;

namespace Videothek.Logic.Ui.Messages {

    public class OpenPickDataMessage {
        public List<dynamic> Items { get; set; }

        public OpenPickDataMessage(List<dynamic> items) {
            Items = items;
        }
    }
}
