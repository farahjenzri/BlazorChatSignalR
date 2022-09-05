namespace BlazorChatSignalR.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string contenu { get; set; }=String.Empty;
       
        public string UserId { get; set; }= String.Empty;
    }
}
