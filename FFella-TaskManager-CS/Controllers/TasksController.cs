
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FFella_TaskManager_CS.Models;
using Task = FFella_TaskManager_CS.Models.Task;

namespace FFella_TaskManager_CS.Controllers
{
    [ApiController]
    [Route("api")]
    public class TasksController : Controller
    {
        private readonly TasksDbContext _context;

        public TasksController(TasksDbContext context)
        {
            _context = context;
        }

        // GET: tasks
        [HttpGet("tasks"), ActionName("Index")]
        //public async ToDoItem<IActionResult> Index()
        public async Task<List<Models.Task>> IndexAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: Tasks/5
        [HttpGet("tasks/{id}")]
        public async Task<Models.Task> TaskDetailsAsync(int? id)
        {
            var toDoItem = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);

            //return View(toDoItem);
            return toDoItem;
        }

        // POST: tasks/create
        [HttpPost("tasks/create")]
        public async Task<Models.Task> Create(
            [Bind("TaskId,DueDate,Description,Iscomplete")] Models.Task toDoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toDoItem);
                await _context.SaveChangesAsync();

                return toDoItem;
            }

            return toDoItem;
        }

        // PUT: Tasks/Update
        [HttpPut("tasks/update")]
        public async Task<IActionResult> Edit([Bind("TaskId,DueDate,Description,Iscomplete")] Task toDoItem)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (TaskExists(toDoItem.TaskId))
                    {
                        throw;
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return Ok();
            }
            return Ok();
        }

        private bool TaskExists(object taskId)
        {
            throw new NotImplementedException();
        }

        // DELETE: Tasks/Delete/5
        [HttpDelete("tasks/delete/{id}"), ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> DeleteConfirmedAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
            
            await _context.SaveChangesAsync();

            return Ok(id);
        }

        private bool TaskExists(int id)
        {
          return (_context.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
