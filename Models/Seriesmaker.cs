using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace ChipsMovieLogz.Models
{
    public class Moviemaker
    {
        public List<Movie> GetSyncedMovies(string title, string genre, DateTime releaseDate, string about, int imdbRating, int motionPictureRating, int metaScore, string director, string writer, int runtime, string topCast)
        {
            List<Movie> movies = new List<Movie>();

            // Define the SQL query to retrieve movies based on the provided criteria
            string sqlQuery = "SELECT * FROM Movies WHERE ";
            List<string> conditions = new List<string>();

            // Add conditions for each parameter that is not null or empty
            if (!string.IsNullOrEmpty(title))
                conditions.Add("Title = @Title");

            if (!string.IsNullOrEmpty(genre))
                conditions.Add("Genre = @Genre");

            if (releaseDate != DateTime.MinValue)
                conditions.Add("ReleaseDate = @ReleaseDate");

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
                    if (!string.IsNullOrEmpty(title))
                        command.Parameters.AddWithValue("@Title", title);

                    if (!string.IsNullOrEmpty(genre))
                        command.Parameters.AddWithValue("@Genre", genre);

                    if (releaseDate != DateTime.MinValue)
                        command.Parameters.AddWithValue("@ReleaseDate", releaseDate);

                    if (!string.IsNullOrEmpty(about))
                        command.Parameters.AddWithValue("@About", about);

                    // Set parameters for the remaining parameters

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Movie movie = new Movie
                            {
                                // Map columns from the database to properties of the Movie object
                                Title = reader["Title"].ToString(),
                                Genre = reader["Genre"].ToString(),
                                ReleaseDate = DateTime.Parse(reader["ReleaseDate"].ToString()),
                                About = reader["About"].ToString(),
                                IMDBRating = Convert.ToInt32(reader["IMDBRating"]),
                                MotionPictureRating = Convert.ToInt32(reader["MotionPictureRating"]),
                                MetaScore = Convert.ToInt32(reader["MetaScore"]),
                                Director = reader["Director"].ToString(),
                                Writer = reader["Writer"].ToString(),
                                Runtime = Convert.ToInt32(reader["Runtime"]),
                                TopCast = reader["TopCast"].ToString()
                            };

                            movies.Add(movie);
                        }
                    }
                }
            }

            return movies;
        }
    }
}
