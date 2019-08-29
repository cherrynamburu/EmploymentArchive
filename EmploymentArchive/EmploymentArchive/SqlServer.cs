using System;
using System.Data.SqlClient;


namespace EmploymentArchive
{
    class SqlServer
    {
        private static SqlConnection _sqlConnection = new SqlConnection(Config.ConnectionString);

        static SqlServer()
        {    
            try
            {
                _sqlConnection.Open();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void RetrieveJob()
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            //command.CommandText = "SELECT * from Job, EmploymentHistory WHERE Job.EmploymentHistoryId = EmploymentHistory.EmploymentHistoryId AND JobId = @JobId";
            command.CommandText = "SELECT Job.JobId, Job.JobTitle, Job.Employer, Job.Description, EmploymentHistory.EmploymentStart, EmploymentHistory.EmploymentEnd FROM Job JOIN EmploymentHistory ON Job.EmploymentHistoryId = EmploymentHistory.EmploymentHistoryId WHERE Job.JobId = @JobId";
            command.Parameters.AddWithValue("@JobId", JobModel.JobId);
            try
            {
                var reader = command.ExecuteReader();
                if (reader.Read())
                {

                    Console.WriteLine(reader["JobId"] + " - " + reader["JobTitle"] + " - " + reader["Employer"] + " - " + reader["Description"] + " - " + Convert.ToDateTime(reader["EmploymentStart"]).ToShortDateString() + " - " + Convert.ToDateTime(reader["EmploymentEnd"]).ToShortDateString());          
                }
                else
                {
                    Console.WriteLine("No record with this JobId..!");
                }
                
                
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void RetrieveJobs()
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandText = "SELECT * FROM Job INNER JOIN EmploymentHistory ON Job.EmploymentHistoryId = EmploymentHistory.EmploymentHistoryId";
   
            command.Parameters.AddWithValue("@JobId", JobModel.JobId);
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["JobId"] + " - " + reader["JobTitle"] + " - " + reader["Employer"] + " - " + reader["Description"] + " - " + Convert.ToDateTime(reader["EmploymentStart"]).ToShortDateString() + " - " + Convert.ToDateTime(reader["EmploymentEnd"]).ToShortDateString());
                }
                reader.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        internal static void InsertToEmployementHistory()
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandText = "Insert into EmploymentHistory values(CONVERT(date,@from, 105),CONVERT(date,@to,105))";
            command.Parameters.AddWithValue("@from", EmploymentHistoryModel.From);
            command.Parameters.AddWithValue("@to", EmploymentHistoryModel.To);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("inserting into Employement history is completed..!");
        }

        internal static void DeleteJob()
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandText = "DELETE FROM EmploymentHistory WHERE EmploymentHistory.EmploymentHistoryId = @EmployementHistoryId";
            command.Parameters.AddWithValue("@EmployementHistoryId", JobModel.EmploymentHistoryId);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("All the records related to entered JobId are deleted..!");            
        }

        internal static void InsertJob()
        {
            
            InsertToEmployementHistory();
            SqlCommand command = _sqlConnection.CreateCommand();

            command.CommandText = "SELECT MAX(EmploymentHistoryId) from EmploymentHistory";

            using (SqlDataReader dr = command.ExecuteReader())

            while (dr.Read())
            {
                JobModel.EmploymentHistoryId = dr.GetInt32(0);
                    
            }

            command.CommandText = "Insert into Job values(@JobTitle, @Employer, @Description, @EmploymentHistoryId)";
            command.Parameters.AddWithValue("@JobTitle", JobModel.JobTitle);
            command.Parameters.AddWithValue("@Employer", JobModel.Employer);
            command.Parameters.AddWithValue("@Description", JobModel.Description);
            command.Parameters.AddWithValue("@EmploymentHistoryId", JobModel.EmploymentHistoryId);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Inserting into Job is Completed");
        }

        internal static bool CheckJobExists()
        {
            object employmentHistoryId = null;
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandText = "select * from Job where jobid = @JobId ";
            command.Parameters.AddWithValue("@JobId", JobModel.JobId);
            try
            {
                employmentHistoryId = command.ExecuteScalar();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            if (employmentHistoryId != null)
            {
                Console.WriteLine("JobId exists..!");
                JobModel.EmploymentHistoryId = Convert.ToInt32(employmentHistoryId);
                return true;
            }

            Console.WriteLine("JobId Doesnt Exist..!");
            return false;




        }

        internal static void UpdateJob()
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandText = "UPDATE Job SET JobTitle = '" + JobModel.JobTitle + "',Employer = '" + JobModel.Employer + "',Description = '" + JobModel.Description + "' WHERE JobId = '" + JobModel.JobId + "'";
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Updating job completed..!");
            UpadteEmploymentHistory();

        }

        internal static void UpadteEmploymentHistory()
        {
            SqlCommand command = _sqlConnection.CreateCommand();
            command.CommandText = "UPDATE EmploymentHistory SET EmploymentStart = '" + EmploymentHistoryModel.From + "', EmploymentEnd = '" + EmploymentHistoryModel.To + "' WHERE EmploymentHistoryId = '" + JobModel.EmploymentHistoryId + "' ";
            try
            {
                command.ExecuteNonQuery();
            }
             catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Updating Employment history is completed..!");
        }
    }
}


