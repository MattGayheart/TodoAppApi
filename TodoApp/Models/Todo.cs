using Microsoft.VisualBasic;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string MoreDetails { get; set; }
        public string DueDate { get; set; }
        public bool Completed { get; set; }
    }
}
