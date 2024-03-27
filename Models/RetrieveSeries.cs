using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace ChipsMovieLogz.Models
{
    public class RetrieveSeries
    {
        private readonly string connectionString;

        public RetrieveSeries(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }


        public List<Series> GetSyncedSeries(string title, string genre)
        {
            List<Series> seriesList = new List<Series>();

            // Define the SQL query to retrieve series based on the provided criteria
            string sqlQuery = "SELECT * FROM Series WHERE ";
            List<string> conditions = new List<string>();

            // Add conditions for each parameter that is not null or empty
            if (!string.IsNullOrEmpty(title))
                conditions.Add("Name = @Title");

            if (!string.IsNullOrEmpty(genre))
                conditions.Add("Genre = @Genre");


            // Add other conditions for the remaining parameters

            // Combine conditions with "AND" and build the full SQL query
            sqlQuery += string.Join(" AND ", conditions);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand command = new OleDbCommand(sqlQuery, connection))
                {
                    // Set parameters based on provided values
                    if (!string.IsNullOrEmpty(title))
                        command.Parameters.AddWithValue("@Title", title);

                    if (!string.IsNullOrEmpty(genre))
                        command.Parameters.AddWithValue("@Genre", genre);


                    // Set parameters for the remaining parameters

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Series series = new Series
                            {
                                // Map columns from the database to properties of the Series object
                                Title = reader["Name"].ToString(),
                                Genre = reader["Genre"].ToString(),
                                PremierDate = DateTime.Parse(reader["PremierDate"].ToString()),
                                About = reader["About"].ToString(),
                                IMDBRating = Convert.ToInt32(reader["IMDBRating"]),
                                Certification = reader["Certification"].ToString(),
                                Runtime = Convert.ToInt32(reader["Runtime"]),
                                Director = reader["Director"].ToString(),
                                Writer = reader["Writer"].ToString(),
                                TopCast = reader["TopCast"].ToString(),
                                Episodes = Convert.ToInt32(reader["Episodes"])
                            };

                            seriesList.Add(series);
                        }
                    }
                }
            }

            return seriesList;
        }
    }
}
