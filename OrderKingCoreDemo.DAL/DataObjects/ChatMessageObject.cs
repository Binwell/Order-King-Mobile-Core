namespace OrderKingCoreDemo.DAL.DataObjects
{
    public class ChatMessageObject
    {
        public string WriterId { get; set; }
		public string MessageText { get; set; }
		public MessageStatus Status { get; set; }
    }
}