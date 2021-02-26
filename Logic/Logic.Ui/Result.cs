using GalaSoft.MvvmLight.Messaging;

namespace Videothek.Logic.Ui {

    public class Result<T> {
        public bool Success { get; set; }

        public T Record { get; set; }

        public Result(bool success, T record) {
            Success = success;
            Record = record;
        }
    }
}
