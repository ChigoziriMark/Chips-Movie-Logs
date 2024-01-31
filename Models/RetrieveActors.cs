using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ChipsMovieLogz.Models
{
    public class RetrieveActors
    {
        private readonly string connectionString;

        public RetrieveActors(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Actor> GetSyncedActors(string firstName, string lastName, int credits, string placeOfBirth, DateTime dateOfBirth, string gender, int age, string biography, string featuresIn, string productionCredit, string crewCredit, string writingCredit)
        {
            List<Actor> actorsList = new List<Actor>();

            // Define the SQL query to retrieve actors based on the provided criteria
            string sqlQuery = "SELECT * FROM Actors WHERE ";
            List<string> conditions = new List<string>();

            // Add conditions for each parameter that is not null or empty
            if (!string.IsNullOrEmpty(firstName))
                conditions.Add("FirstName = @FirstName");

            if (!string.IsNullOrEmpty(lastName))
                conditions.Add("LastName = @LastName");

            if (credits > 0)
                conditions.Add("Credits = @Credits");

            if (!string.IsNullOrEmpty(placeOfBirth))
                conditions.Add("PlaceOfBirth = @PlaceOfBirth");

            if (dateOfBirth != DateTime.MinValue)
                conditions.Add("DateOfBirth = @DateOfBirth");

            // Add other conditions for the remaining parameters

            // Combine conditions with "AND" and build the full SQL query
            sqlQuery += string.Join(" AND ", conditions);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    // Set parameters based on provided values
                    if (!string.IsNullOrEmpty(firstName))
                        command.Parameters.AddWithValue("@FirstName", firstName);

                    if (!string.IsNullOrEmpty(lastName))
                        command.Parameters.AddWithValue("@LastName", lastName);

                    if (credits > 0)
                        command.Parameters.AddWithValue("@Credits", credits);

                    if (!string.IsNullOrEmpty(placeOfBirth))
                        command.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);

                    if (dateOfBirth != DateTime.MinValue)
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                    // Set parameters for the remaining parameters

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Actor actor = new Actor
                            {
                                // Map columns from the database to properties of the Actor object
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Credits = Convert.ToInt32(reader["Credits"]),
                                PlaceOfBirth = reader["PlaceOfBirth"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                Gender = reader["Gender"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Biography = reader["Biography"].ToString(),
                                FeaturesIn = reader["FeaturesIn"].ToString(),
                                ProductionCredit = reader["ProductionCredit"].ToString(),
                                CrewCredit = reader["CrewCredit"].ToString(),
                                WritingCredit = reader["WritingCredit"].ToString()
                            };

                            actorsList.Add(actor);
                        }
                    }
                }
            }

            return actorsList;
        }
    }
}
