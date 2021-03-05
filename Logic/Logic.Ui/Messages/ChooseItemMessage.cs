namespace Videothek.Logic.Ui.Messages {

    public class ChooseItemMessage {
        public string TableName { get; set; }
        public int Id { get; set; }

        public ChooseItemMessage(string tableName, int id) {
            TableName = tableName;
            Id = id;
        }
    }
}
