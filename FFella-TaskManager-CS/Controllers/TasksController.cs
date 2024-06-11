
using Microsoft.AspNetCore.Mvc;                  // Provide access to core MVC framework
using Microsoft.EntityFrameworkCore;             // Provide access to Entity Framework ORM
using FFella_TaskManager_CS.Models;              // Provide access to application data component
using Task = FFella_TaskManager_CS.Models.Task;  // Explicity specify "Task" refers to a class in this application
                                                 //           instead of System.Threading.Task

namespace FFella_TaskManager_CS.Controllers
{
    [ApiController]               // Specify that this class contains REST API controllers/methods
    [Route("api")]        // Specify root for API URLs
    public class TasksController : ControllerBase     // This controller is a subclass of the .NET Core controller class 
    {
        private readonly TasksDbContext _context;     // Define a reference to th context to be used by Enity Framework

        public TasksController(TasksDbContext context) // Constructor with Dependency Injected context
        {
            _context = context;  // Assign app context reference to DI injected context object 
        }


        
        // Method to handle HTTP GET for URL path: "/api/tasks"  - root path ("api") defined above
        [HttpGet("tasks")]
        public async Task<List<Models.Task>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync(); // Call entity Framework to return all data instance in data source as a List
        }

        // Method to handle HTTP GET for URL path: "/api/tasks/id" where :id" is the specific id of the task to be returns
        [HttpGet("tasks/{id}")]
        public async Task<Models.Task> TaskDetailsAsync(int? id)
        {
            // Call Entity Framework to retrieve task with id passed in URL
            var toDoItem = await _context.Tasks
                .FirstOrDefaultAsync(m => m.TaskId == id);

            return toDoItem;  // Return task retrieved from data source
        }

        // Method to handle HTTP POST for URL path: "/api/tasks/create" - root path "api" defined above
        //     Data to be added to the data source will be passed in the body of the HTTP POST request as JSON
        [HttpPost("tasks/create")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.Task>> Create(
            [Bind("TaskId,DueDate,Description,Iscomplete")] Models.Task toDoItem)  // Instantiate a Task object using JSON in request
        {
            if (ModelState.IsValid)                   // If data passed passes all validity checks...
            {
                _context.Add(toDoItem);               //      Call Entity Framework to add data to data source
                await _context.SaveChangesAsync();    //      Call Entity Framework to save new data to data source

                return toDoItem;                      //      Return object passed to method and added to data source
            }

            return BadRequest(toDoItem);  // Data passed was invalid, Bad Request (400) HTTP status code
        }

        // Method to handle HTTP PUT for URL path: "/api/tasks/update" - root path "api" defined above
        //     Data to be added to the data source will be passed in the body of the HTTP PUT request as JSON
        //     Instantiates a Task object using JSON in request
        public async Task<IActionResult> Edit([Bind("TaskId,DueDate,Description,Iscomplete")] Task toDoItem)
        {
            if (ModelState.IsValid)   // I data passed passes all validity checks....
            {
                try                                     // Attempt to...
                {
                    _context.Update(toDoItem);          //     Call Entity Framework to update data in data source
                    await _context.SaveChangesAsync();  //     Call Entity Framework to save updated data in data source
                }
                catch (DbUpdateConcurrencyException)    // If unexpected data base error occurs..
                {
                    if (TaskExists(toDoItem.TaskId))    //      and update item exists...
                    {
                        throw;                          //          throw the exception to next process in call stack
                    }
                    else                                //      and update item does not exist...
                    {
                        return NotFound();              //          return with Not Found (404) HTPP status code
                    }
                }
                return Ok();                            // If update was successful, return OK (200) HTTP status code
            }
            return BadRequest();                        // Data wassed was invalid - return Bad Request (400) HTTP status code
        }

        

        // Method to handle HTTP DELETE for URL path: "/api/tasks/delete/id - where id is the task id to be deleted
        [HttpDelete("tasks/delete/{id}"), ActionName("Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<int>> DeleteConfirmedAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);  // Call Entity Framework to see if tsk to be delete exists in the data source

            if (task != null)                               // If task does exist...
            {
                _context.Tasks.Remove(task);                //     Call Entity Framework to remove it from the data soure
            }
            
            await _context.SaveChangesAsync();              // Call Entity Framework tp save changes 

            return Ok(id);                               // Return HTTP OK (200) status code
        }

        // Helper method to determine is a particular task exists in the data source
        private bool TaskExists(int id)
        {
          // Call Entity Framework to determine is there is any task in data source with id passed
          //      and return what it returns (true if there is, false if there is not)
          return (_context.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
}

}
