using TaskList.Data.Models;

namespace TaskList.Data.DAL.Interfaces
{
    public interface IFolderDAO
    {
        Folder GetById(int id);
        Folder GetByFolderName(string folderName);
        List<Folder> GetAll();
        Folder Create(Folder newFolder);
        bool Delete(int id);
        Folder Update(int id, Folder modifiedFolder);

    }
}
