using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [EnableCors("TodoControllerLevel")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly DBContext context;

        public TodoController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await context.Todo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
            var todo = await context.Todo.FindAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Todo>>> UpdateTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }
            context.Entry(todo).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await context.Todo.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> InsertTodo(Todo todo)
        {
            context.Todo.Add(todo);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> DeleteTodo(int id)
        {
            var todo = await context.Todo.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            context.Todo.Remove(todo);
            await context.SaveChangesAsync();

            return todo;
        }

        private bool TodoExists(int id)
        {
            return context.Todo.Any(e => e.Id == id);
        }
    }
}
