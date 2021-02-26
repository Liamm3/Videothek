using GalaSoft.MvvmLight.Messaging;

namespace Videothek.Logic.Ui.Messages {

    public class AddResultMessage {
        public bool Success { get; set; }

        public AddResultMessage(bool success) => Success = success;
    }
}
