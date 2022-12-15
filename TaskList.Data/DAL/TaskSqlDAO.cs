using Microsoft.Data.SqlClient;
using System.Data;
using TaskList.Data.DAL.Interfaces;
using TaskList.Data.Helpers;
using Task = TaskList.Data.Models.Task;

namespace TaskList.Data.DAL
{
    public class TaskSqlDAO : ITaskDAO
    {
        private readonly string connectionString;

        public TaskSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Task Create(Task newTask)
        {
            try
            {
                int newId = -1;
                
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.Create]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@taskName", newTask.TaskName);
                cmd.Parameters.AddWithValue("@dueDate", newTask.DueDate == null ? DBNull.Value : newTask.DueDate);
                cmd.Parameters.AddWithValue("@recurrenceId", newTask.RecurrenceId);
                cmd.Parameters.AddWithValue("@isImportant", newTask.IsImportant);
                cmd.Parameters.AddWithValue("@isComplete", newTask.IsComplete);
                cmd.Parameters.AddWithValue("@folderId", newTask.FolderId);
                cmd.Parameters.AddWithValue("@note", newTask.Note == null ? DBNull.Value : newTask.Note);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@taskId",
                    Value = newId,
                    IsNullable = false,
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.Output
                });

                int rowsAffected = (int)cmd.ExecuteNonQuery();

                newId = (int)cmd.Parameters["@taskId"].Value;

                return GetById(newId);
            }
            catch (SqlException) {throw;}
        }

        public bool Delete(int id)
        {
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.Delete]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);

                int rowsAffected = (int)cmd.ExecuteNonQuery();

                return rowsAffected == 1;
            }
            catch (SqlException) { throw; }
        }

        public Task GetById(int id)
        {
            Task t = null!;
            
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.GetById]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {
                    t = GetTaskFromReader(reader);
                }                
            }
            catch (SqlException) { throw; }

            return t;
        }

        public List<Task> GetImportant()
        {
            List<Task> tList = new();

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.GetImportant]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Task t = GetTaskFromReader(reader);
                    tList.Add(t);
                }
            }
            catch (SqlException) { throw; }

            return tList;
        }

        public List<Task> GetCompleted()
        {
            List<Task> tList = new();

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.GetCompleted]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Task t = GetTaskFromReader(reader);
                    tList.Add(t);
                }
            }
            catch (SqlException) { throw; }

            return tList;
        }

        public List<Task> GetRecurring()
        {
            List<Task> tList = new();

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.GetRecurring]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Task t = GetTaskFromReader(reader);
                    tList.Add(t);
                }
            }
            catch (SqlException) { throw; }

            return tList;
        }

        public List<Task> GetPlanned()
        {
            List<Task> tList = new();

            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.GetPlanned]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Task t = GetTaskFromReader(reader);
                    tList.Add(t);
                }
            }
            catch (SqlException) { throw; }

            return tList;
        }

        private static Task GetTaskFromReader(SqlDataReader reader)
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
                    Id = reader.GetFieldValue<int>("FolderId"),
                    FolderName = reader.GetFieldValue<string>("FolderName")
                }
            };

            return t;
        }

        public Task Update(int id, Task modifiedTask)
        {
            try
            {
                using SqlConnection conn = new(connectionString);
                conn.Open();

                SqlCommand cmd = new("[Tasks.Update]", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@taskName", modifiedTask.TaskName);
                cmd.Parameters.AddWithValue("@dueDate", modifiedTask.DueDate);
                cmd.Parameters.AddWithValue("@recurrenceId", modifiedTask.RecurrenceId);
                cmd.Parameters.AddWithValue("@isImportant", modifiedTask.IsImportant);
                cmd.Parameters.AddWithValue("@isComplete", modifiedTask.IsComplete);
                cmd.Parameters.AddWithValue("@folderId", modifiedTask.FolderId);
                cmd.Parameters.AddWithValue("@note", modifiedTask.Note);

                cmd.ExecuteNonQuery();

                return GetById(id);
            }
            catch (SqlException) { throw; }
        }
    }
}
