using GalaSoft.MvvmLight.Messaging;

namespace Videothek.Logic.Ui {

    public class Result : PropertyChangedMessageBase {
        public bool Success { get; set; }

        public Result(bool success, string propertyName) : base(propertyName) => Success = success;
    }
}
