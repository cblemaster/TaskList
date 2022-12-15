using Microsoft.Data.SqlClient;
using System.Data;
using TaskList.Data.DAL.Interfaces;
using TaskList.Data.Helpers;
using TaskList.Data.Models;
using Task = TaskList.Data.Models.Task;

namespace TaskList.Data.DAL
{
    public class FolderSqlDAO : IFolderDAO
    {
        private readonly string connectionString;

        public FolderSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Folder Create(Folder newFolder)
        {
            try
            {
                int newId = -1;

                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Folders.Create]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@folderName", newFolder.FolderName);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@folderId",
                    Value = newId,
                    IsNullable = false,
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                });

                int rowsAffected = cmd.ExecuteNonQuery();

                newId = (int)cmd.Parameters["@folderId"].Value;

                return GetById(newId);
            }
            catch (SqlException) { throw; }
        }

        public bool Delete(int id)
        {
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Folders.Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = (int)cmd.ExecuteNonQuery();

                return rowsAffected == 1;
            }
            catch (SqlException) { throw; }
        }

        public List<Folder> GetAll()
        {
            List<Folder> fList = new();

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Folders.GetAll]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = cmd.ExecuteReader();
                fList = GetFoldersFromReader(reader);
            }
            catch (SqlException) { throw; }

            return fList;
        }

        public Folder GetByFolderName(string folderName)
        {
            Folder f = null!;

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Folders.GetByFolderName]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@folderName", folderName);

                SqlDataReader reader = cmd.ExecuteReader();
                f = GetFolderFromReader(reader);
            }
            catch (SqlException) { throw; }

            return f;
        }

        public Folder GetById(int id)
        {
            Folder f = null!;

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Folders.GetById]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                f = GetFolderFromReader(reader);

            }
            catch (SqlException) { throw; }

            return f;
        }

        private static List<Folder> GetFoldersFromReader(SqlDataReader reader)
        {
            List<Folder> fList = new();
            Dictionary<(int id, string name), List<Task>> fDict = new();
            
            while (reader.Read())
            {
                (int id, string name) key = (reader.GetFieldValue<int>("FolderId"), reader.GetFieldValue<string>("FolderName"));

                if (!fDict.ContainsKey(key))
                {
                    fDict.Add(key, new List<Task>());
                }

                if (reader.GetFieldValue<int>("TaskId") != 0)
                {
                    Task t = new()
                    {
                        Id = reader.GetFieldValue<int>("TaskId"),
                        TaskName = reader.GetFieldValue<string>("TaskName"),
                        DueDate = reader.GetFieldValue<DateTime?>("DueDate"),
                        RecurrenceId = reader.GetFieldValue<int>("RecurrenceId"),
                        IsImportant = reader.GetFieldValue<bool>("IsImportant"),
                        IsComplete = reader.GetFieldValue<bool>("IsComplete"),
                        FolderId = reader.GetFieldValue<int>("FolderId"),
                        Note = reader.GetFieldValue<string?>("Note"),
                        Folder = new()
                        {
                            Id = key.id,
                            FolderName = key.name
                        }
                    };

                    fDict[key].Add(t);
                }
            }                       

            foreach (KeyValuePair<(int id, string name), List<Task>> kvp in fDict)
            {
                Folder f = new()
                {
                    Id = kvp.Key.id,
                    FolderName = kvp.Key.name,
                    Tasks = kvp.Value
                };

                fList.Add(f);
            }

            return fList;
        }

        private static Folder GetFolderFromReader(SqlDataReader reader)
        {
            Folder f = new();
            Dictionary<(int id, string name), List<Task>> fDict = new();
            (int id, string name) key = (0, string.Empty);

            while (reader.Read())
            {
                key = (reader.GetFieldValue<int>("FolderId"), reader.GetFieldValue<string>("FolderName"));

                if (!fDict.ContainsKey(key))
                {
                    fDict.Add(key, new List<Task>());
                }

                if (reader.GetFieldValue<int>("TaskId") != 0)
                {
                    Task t = new()
                    {
                        Id = reader.GetFieldValue<int>("TaskId"),
                        TaskName = reader.GetFieldValue<string>("TaskName"),
                        DueDate = reader.GetFieldValue<DateTime?>("DueDate"),
                        RecurrenceId = reader.GetFieldValue<int>("RecurrenceId"),
                        IsImportant = reader.GetFieldValue<bool>("IsImportant"),
                        IsComplete = reader.GetFieldValue<bool>("IsComplete"),
                        FolderId = reader.GetFieldValue<int>("FolderId"),
                        Note = reader.GetFieldValue<string?>("Note"),
                        Folder = new()
                        {
                            Id = key.id,
                            FolderName = key.name
                        }
                    };

                    fDict[key].Add(t);
                }
            }
            
            f.Id = key.id;
            f.FolderName = key.name;
            f.Tasks = fDict[key];            

            return f;
        }

        public Folder Update(int id, Folder modifiedFolder)
        {
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Folders.Update]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@folderName", modifiedFolder.FolderName);

                cmd.ExecuteNonQuery();

                return GetById(id);
            }
            catch (SqlException) { throw; }
        }
    }
}
