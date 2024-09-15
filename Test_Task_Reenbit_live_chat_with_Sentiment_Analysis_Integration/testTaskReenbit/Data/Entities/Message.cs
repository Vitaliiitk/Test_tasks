namespace testTaskReenbit.Data.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public string UserName { get; set; } // The name of the message owner
        public string Text { get; set; } // The contents of the a message
        public DateTime Timestamp { get; set; }
        public string Sentiment { get; set; }
    }
}
