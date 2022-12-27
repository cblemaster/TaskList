using TaskList.UI.Services.Models;
using Task = TaskList.UI.Services.Models.Task;

namespace TaskList.UI.Services.Validation
{
    public class ModelValidation
    {
        public static bool IsNewFolderNameUnique(string newFolderName, List<string> existingFolderNames)
            => !existingFolderNames.Contains(GetCapitalizedFolderName(newFolderName));

        public static string GetCapitalizedFolderName(string folderName)
        {
            string firstChar = folderName[0].ToString();

            if (firstChar.ToUpper() == folderName[0].ToString())
                return folderName;
            return string.Concat(firstChar.ToUpper(), folderName.AsSpan(1));
        }

        public static bool IsDueDateTodayOrFuture(DateTime? dueDate)
            => dueDate == null || (dueDate.HasValue && dueDate >= DateTime.Today);

        public static bool IsFolderOrTaskNameNoMoreThanDbAllowedLength(string name)
            => name.Length <= 100;

        public static bool IsNoteNoMoreThanDbAllowedLength(string? note)
            => note == null || note.Length <= 255;

        public static bool IsRequiredStringValid(string inputString)
            => !string.IsNullOrEmpty(inputString) && !string.IsNullOrWhiteSpace(inputString);

        public static (bool isValid, List<string> validationErrors) ValidateTask(Task task)
        {
            List<string> validationErrors = new();

            if (IsRequiredStringValid(task.TaskName))
                validationErrors.Add("Task name cannot be null, empty string, or whitespace.");
            if (IsFolderOrTaskNameNoMoreThanDbAllowedLength(task.TaskName))
                validationErrors.Add("Max length for task name is 100 characters.");
            if (IsDueDateTodayOrFuture(task.DueDate))
                validationErrors.Add("Due date must be today or after.");
            if (IsNoteNoMoreThanDbAllowedLength(task.Note))
                validationErrors.Add("Max length for note is 255 characters.");

            if (validationErrors.Any())
                return (false, validationErrors);
            else
                return (true, validationErrors);
        }

        public static (bool isValid, List<string> validationErrors) ValidateFolder(Folder folder)
        {
            List<string> validationErrors = new();

            folder.FolderName = GetCapitalizedFolderName(folder.FolderName);

            if (IsFolderOrTaskNameNoMoreThanDbAllowedLength(folder.FolderName))
                validationErrors.Add("Max length for folder name is 100 characters.");
            if (IsRequiredStringValid(folder.FolderName))
                validationErrors.Add("Folder name cannot be null, empty string, or whitespace.");
            
            FolderService fs = new();
            List<Folder> folders = new();
            folders = fs.GetAll();
            if (IsNewFolderNameUnique(folder.FolderName, folders.Select(f => f.FolderName).ToList()))
                validationErrors.Add("Specified folder name is already used.");

            if (validationErrors.Any())
                return (false, validationErrors);
            else
                return (true, validationErrors);
        }
    }
}
