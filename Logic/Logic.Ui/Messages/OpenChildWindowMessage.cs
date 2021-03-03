using GalaSoft.MvvmLight.Messaging;

namespace Videothek.Logic.Ui.Messages {

    public class OpenChildWindowMessage : NotificationMessage {

        public OpenChildWindowMessage(string notification) : base(notification) {
        }
    }
}
