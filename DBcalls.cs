using System;
using System.Text;
using System.Data.SqlClient;

namespace SQLChores
{
    class DBcalls
    {
        private string connectionString;
        private string sql;
        public DBcalls(string connection)
        {
            connectionString = connection;
        }
        public void CreateDB()
        {
            Console.Write("Creating Chore DataBase...");
            string sql = "DROP DATABASE IF EXISTS [ChoresDB]; CREATE DATABASE [ChoresDB]";
            RunQuery(sql);
        }
        public void CreateTable()
        {
            Console.Write("Creating Chores Table...");
            StringBuilder sb = new StringBuilder();
            sb.Append("USE ChoresDB; ");
            sb.Append("CREATE TABLE Chores ( ");
            sb.Append("ChoreID INT IDENTITY(1,1) NOT NULL PRIMARY KEY, ");
            sb.Append("ChoreName NVARCHAR(50),");
            sb.Append("ChoreAssignment NVARCHAR(50)); ");
            sql = sb.ToString();
            RunQuery(sql);
        }
        public void AddChore(string choreName, string choreAssignment)
        {
            Console.Write("Adding Chore...");
            StringBuilder sb = new StringBuilder();
            sb.Append("USE ChoresDB; ");
            sb.Append("INSERT INTO Chores (ChoreName, ChoreAssignment) VALUES");
            sb.Append($"('{choreName}', '{choreAssignment}');");
            sql = sb.ToString();
            RunQuery(sql);
        }
        public void UpdateChore(string choreAssignment, string choreName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE ChoresDB; ");
            sb.Append($"UPDATE Chores SET ChoreName = '{choreName}' ");
            sb.Append($"WHERE ChoreAssignment = '{choreAssignment}'; ");
            sql = sb.ToString();
            RunQuery(sql);
        }
        public void DeleteChore(int userID)
        {
            Console.WriteLine("Deleting Chore...");
            sql = $"USE ChoresDB DELETE FROM Chores WHERE ChoreID = {userID};";
            RunQuery(sql);
        }
        public void GetChore()
        {
            Console.WriteLine("Listing Chores...");
            sql = "USE ChoresDB SELECT * FROM Chores;";
            ListQuery(sql);
        }

        public void RunQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine(" Done");
                }
            }
        }
        public void ListQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("|{0}|{1}|{2}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        }
                    }
                }
            }
        }
    }
}