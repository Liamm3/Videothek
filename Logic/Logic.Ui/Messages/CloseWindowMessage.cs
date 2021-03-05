namespace Videothek.Logic.Ui.Messages {

    public class CloseWindowMessage {
        public bool Success { get; set; }

        public CloseWindowMessage(bool success) => Success = success;
    }
}
