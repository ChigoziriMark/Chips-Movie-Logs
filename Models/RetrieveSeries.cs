using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChipsMovieLogz.Models
{
    public class RetrieveSeries
    {
        private readonly string connectionString;

        public RetrieveSeries(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Series> GetSyncedSeries(string name, string genre, DateTime premierDate, string about, int imdbRating, string certification, int runtime, string director, string writer, string topCast, int episodes)
        {
            List<Series> seriesList = new List<Series>();

            // Define the SQL query to retrieve series based on the provided criteria
            string sqlQuery = "SELECT * FROM Series WHERE ";
            List<string> conditions = new List<string>();

            // Add conditions for each parameter that is not null or empty
            if (!string.IsNullOrEmpty(name))
                conditions.Add("Name = @Name");

            if (!string.IsNullOrEmpty(genre))
                conditions.Add("Genre = @Genre");

            if (premierDate != DateTime.MinValue)
                conditions.Add("PremierDate = @PremierDate");

            if (!string.IsNullOrEmpty(about))
                conditions.Add("About = @About");

            // Add other conditions for the remaining parameters

            // Combine conditions with "AND" and build the full SQL query
            sqlQuery += string.Join(" AND ", conditions);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Set parameters based on provided values
                    if (!string.IsNullOrEmpty(name))
                        command.Parameters.AddWithValue("@Name", name);

                    if (!string.IsNullOrEmpty(genre))
                        command.Parameters.AddWithValue("@Genre", genre);

                    if (premierDate != DateTime.MinValue)
                        command.Parameters.AddWithValue("@PremierDate", premierDate);

                    if (!string.IsNullOrEmpty(about))
                        command.Parameters.AddWithValue("@About", about);

                    // Set parameters for the remaining parameters

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Series series = new Series
                            {
                                // Map columns from the database to properties of the Series object
                                Name = reader["Name"].ToString(),
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
