using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskList.Data.Contexts;
using TaskList.Data.DAL.Interfaces;
using TaskList.Data.Models;
using Task = TaskList.Data.Models.Task;

namespace TaskList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskDAO _dao;

        public TaskController(ITaskDAO dao)
        {
            _dao = dao;
        }

        // https://localhost:7086/Task/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Task t = _dao.GetById(id);

            if (t != null)
                return Ok(t);
            else
                return NotFound();
        }

        // https://localhost:7086/Task/Important
        [HttpGet("Important")]
        public IActionResult GetImportant()
        {
            List<Task> tList = _dao.GetImportant();

            if (tList != null && tList.Any())
                return Ok(tList);
            else
                return NotFound();
        }

        // https://localhost:7086/Task/Completed
        [HttpGet("Completed")]
        public IActionResult GetCompleted()
        {
            List<Task> tList = _dao.GetCompleted();

            if (tList != null && tList.Any())
                return Ok(tList);
            else
                return NotFound();
        }

        // https://localhost:7086/Task/Recurring
        [HttpGet("Recurring")]
        public IActionResult GetRecurring()
        {
            List<Task> tList = _dao.GetRecurring();

            if (tList != null && tList.Any())
                return Ok(tList);
            else
                return NotFound();
        }

        // https://localhost:7086/Task/Planned
        [HttpGet("Planned")]
        public IActionResult GetPlanned()
        {
            List<Task> tList = _dao.GetPlanned();

            if (tList != null && tList.Any())
                return Ok(tList);
            else
                return NotFound();
        }

        // https://localhost:7086/Task
        [HttpPost]
        public IActionResult Create(Task newTask)
        {
            IActionResult result = BadRequest();

            Task created = _dao.Create(newTask);
            
            if (created != null && created.Id > 0)
                result = Created("Task/" + created.Id, created);

            return result;
        }

        // https://localhost:7086/Task/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Task deleted = _dao.GetById(id);
                
            if (deleted == null)
                return NotFound("Task not found");

            bool success = _dao.Delete(id);

            return success ? NoContent() : StatusCode(500); //if delete is successful, return NoContent, otherwise 500 internal server error
        }

        // https://localhost:7086/Task/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Task modifiedTask)
        {
            Task existing = _dao.GetById(id);

            if (existing == null) //we didn't find anything matching that id
                return NotFound("Task not found");

            Task updated = _dao.Update(id, modifiedTask);
            
            return updated != null ? Ok(updated) : StatusCode(500); //if delete is successful, return NoContent, otherwise 500 internal server error
        }
    }
}
