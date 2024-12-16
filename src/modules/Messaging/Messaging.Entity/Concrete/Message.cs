using Core.Entities;
namespace Books.Entity.Concrete
{
    public class Message : IEntity
    {
       public int MessageId { get; set; }
       public int SenderId {get;set;}
       public int ReceiverId {get;set;}
       public string Content { get; set; }
       public bool IsRead { get; set; }
       public DateTime CreatedAt {get;set;}

    }

 
}