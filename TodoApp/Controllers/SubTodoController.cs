using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    [EnableCors("SubTodoControllerLevel")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SubTodoController : ControllerBase
    {
        private readonly DBContext context;

        public SubTodoController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubTodo>>> GetSubTodos()
        {
            return await context.SubTodo.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SubTodo>> GetSubTodo(int id)
        {
            var subTodo = await context.SubTodo.FindAsync(id);

            if (subTodo == null)
            {
                return NotFound();
            }

            return subTodo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<SubTodo>>> UpdateSubTodo(int id, SubTodo subTodo)
        {
            if (id != subTodo.Id)
            {
                return BadRequest();
            }

            context.Entry(subTodo).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubTodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await context.SubTodo.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<SubTodo>> InsertSubTodo(SubTodo subTodo)
        {
            context.SubTodo.Add(subTodo);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetSubTodo", new { id = subTodo.Id }, subTodo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<SubTodo>> DeleteSubTodo(int id)
        {
            var subTodo = await context.SubTodo.FindAsync(id);
            if (subTodo == null)
            {
                return NotFound();
            }

            context.SubTodo.Remove(subTodo);
            await context.SaveChangesAsync();

            return subTodo;
        }

        private bool SubTodoExists(int id)
        {
            return context.SubTodo.Any(e => e.Id == id);
        }
    }
}
