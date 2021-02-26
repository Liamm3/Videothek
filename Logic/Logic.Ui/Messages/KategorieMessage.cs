using Videothek.Logic.Ui.Model;

namespace Videothek.Logic.Ui.Messages {

    public class KategorieMessage : Result<Kategorie> {

        public KategorieMessage(bool success, Kategorie record) : base(success, record) {
        }
    }
}
