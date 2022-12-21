namespace TodoApp.Models
{
    public class SubTodo
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public int ParentID { get; set; }
        public bool Completed { get; set; }
    }
}
