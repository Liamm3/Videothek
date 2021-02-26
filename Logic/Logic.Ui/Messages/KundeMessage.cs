using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui.Messages {

    public class KundeMessage : Result<Kunde> {

        public KundeMessage(bool success, Kunde record) : base(success, record) {
        }
    }
}
