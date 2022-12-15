using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TaskList.Data.Contexts;
using TaskList.Data.DAL.Interfaces;
using TaskList.Data.Models;

namespace TaskList.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FolderController : ControllerBase
    {
        private readonly IFolderDAO _dao;


        private readonly List<string> folderNamesThatCannotBeDeletedNorUpdated = new()
            {
                "Important", "Completed", "Recurring", "Planned", "Tasks"
            };

        public FolderController(IFolderDAO dao)
        {
            _dao = dao;
        }

        // https://localhost:7086/Folder/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Folder f = _dao.GetById(id);

            if (f != null)
                return Ok(f);
            else
                return NotFound();
        }

        // https://localhost:7086/Folder
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Folder> fList = _dao.GetAll();

            if (fList != null && fList.Any())
                return Ok(fList);
            else
                return NotFound();
        }

        // https://localhost:7086/Folder/Name/Career
        [HttpGet("Name/{folderName}")]
        public IActionResult GetByFolderName(string folderName)
        {
            Folder f = _dao.GetByFolderName(folderName);

            if (f != null)
                return Ok(f);
            else
                return NotFound();
        }

        // https://localhost:7086/Folder
        [HttpPost]
        public IActionResult Create(Folder newFolder)
        {
            IActionResult result = BadRequest();

            Folder created = _dao.Create(newFolder);

            if (created != null && created.Id > 0)
                result = Created("Folder/" + created.Id, created);

            return result;
        }

        // https://localhost:7086/Folder/1
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Folder deleted = _dao.GetById(id);

            if (deleted == null)
                return NotFound("Folder not found");

            if (this.folderNamesThatCannotBeDeletedNorUpdated.Contains(deleted.FolderName))
                return StatusCode(500);

            bool success = _dao.Delete(id);

            return success ? NoContent() : StatusCode(500); //if delete is successful, return NoContent, otherwise 500 internal server error
        }

        // https://localhost:7086/Folder/1
        [HttpPut("{id}")]
        public IActionResult Update(int id, Folder modifiedFolder)
        {
            Folder existing = _dao.GetById(id);

            if (existing == null) //we didn't find anything matching that id
                return NotFound("Folder not found");

            if (this.folderNamesThatCannotBeDeletedNorUpdated.Contains(modifiedFolder.FolderName))
                return StatusCode(500);

            Folder updated = _dao.Update(id, modifiedFolder);
            
            return updated != null ? Ok(updated) : StatusCode(500); //if delete is successful, return NoContent, otherwise 500 internal server error
        }
    }
}
