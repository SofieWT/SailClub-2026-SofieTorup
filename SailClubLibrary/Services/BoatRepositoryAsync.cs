using Microsoft.Data.SqlClient;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    public class BoatRepositoryAsync : Connection, IBoatRepoAsync
    {

        #region Query Strings
        private string _addBoat = "INSERT INTO Boat VALUES(@TheBoatType, @Model, @SailNumber, @EngineInfo, @Length, @Width, @Draft, @YearOfConstruction, @Id)";
        private string _getAllBoats = "SELECT * FROM Boat";
        private string _removeBoat = "Delete FROM Boat WHERE SailNumber = @SailNumber";
        private string _searchBoat = "SELECT * FROM Boat WHERE SailNumber = @SailNumber";
        private string _updateBoat = "UPDATE Boat SET Id = @Id, Model = @Model, EngineInfo = @EngineInfo, Length = @Length, Width = @Width, Draft = @Draft, YearOfConstruction = @YearOfConstruction, TheBoatType = @TheBoatType WHERE SailNumber = @SailNumber";
        private string _filterBoats = "SELECT * FROM Boat WHERE Model LIKE @filterCriteria";
        public Task<int> Count => throw new NotImplementedException();

        #endregion

        #region Methods
        public async Task AddBoat(Boat boat)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_addBoat, connection);
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@Id", boat.Id);
                    command.Parameters.AddWithValue("@Model", boat.Model);
                    command.Parameters.AddWithValue("@SailNumber", boat.SailNumber);
                    command.Parameters.AddWithValue("@EngineInfo", boat.EngineInfo);
                    command.Parameters.AddWithValue("@Length", boat.Length);
                    command.Parameters.AddWithValue("@Width", boat.Width);
                    command.Parameters.AddWithValue("@Draft", boat.Draft);
                    command.Parameters.AddWithValue("@YearOfConstruction", boat.YearOfConstruction);
                    command.Parameters.AddWithValue("@TheBoatType", boat.TheBoatType.ToString());

                    int noOfRowsEffected = await command.ExecuteNonQueryAsync();

                    await command.Connection.CloseAsync();

                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("SQL fejl: " + sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
        }

        public async Task<List<Boat>> FilterBoats(string filterCriteria)
        {
            List<Boat> boats = new List<Boat>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_filterBoats, connection);
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@filterCriteria", $"%{filterCriteria}%");
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                            int boatId = reader.GetInt32("Id");
                            string boatModel = reader.GetString("Model");
                            string sailNumber = reader.GetString("SailNumber");
                            string engineInfo = reader.GetString("EngineInfo");
                            string yearOfConstruction = reader.GetString("YearOfConstruction");
                            double boatLength = reader.GetInt32("Length");
                            double width = reader.GetInt32("Width");
                            double draft = reader.GetInt32("Draft");
                            BoatType bt = Enum.Parse<BoatType>(reader.GetString("TheBoatType"));
                            Boat boat = new Boat(boatId, bt, boatModel, sailNumber, engineInfo, draft, width, boatLength, yearOfConstruction);
                            boats.Add(boat);
                    }

                    await command.Connection.CloseAsync();

                }
                catch (SqlException sqlex)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return boats;
        }

        public async Task<List<Boat>> GetAllBoats()
        {
            List<Boat> boats = new List<Boat>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_getAllBoats, connection);
                    await command.Connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        int boatId = reader.GetInt32("Id");
                        string boatModel = reader.GetString("Model");
                        string sailNumber = reader.GetString("SailNumber");
                        string engineInfo = reader.GetString("EngineInfo");
                        string yearOfConstruction = reader.GetString("YearOfConstruction");
                        double boatLength = reader.GetInt32("Length");
                        double width = reader.GetInt32("Width");
                        double draft = reader.GetInt32("Draft");
                        BoatType bt = Enum.Parse<BoatType>(reader.GetString("TheBoatType"));
                        Boat boat = new Boat(boatId, bt, boatModel, sailNumber, engineInfo, draft, width, boatLength, yearOfConstruction);
                        boats.Add(boat);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }
            return boats;

        }

        public async Task RemoveBoat(string sailNumber)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_removeBoat, connection);

                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@SailNumber", sailNumber);

                    int noOfRows = await command.ExecuteNonQueryAsync();
                    await command.Connection.CloseAsync();
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("sql fejl: " + sqlex.Message);
                    throw;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }

            }
        }

        public async Task<Boat?> SearchBoat(string sailNumber)
        {
            Boat boat = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_searchBoat, connection);
                    await command.Connection.OpenAsync();
                    command.Parameters.AddWithValue("@SailNumber", sailNumber);

                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (reader.Read()) // - if-sætning = hvis der er noget at læse.
                    {
                        int boatId = reader.GetInt32("Id");
                        string boatModel = reader.GetString("Model");
                        string engineInfo = reader.GetString("EngineInfo");
                        string yearOfConstruction = reader.GetString("YearOfConstruction");
                        double boatLength = reader.GetInt32("Length");
                        double width = reader.GetInt32("Width");
                        double draft = reader.GetInt32("Draft");
                        BoatType bt = Enum.Parse<BoatType>(reader.GetString("TheBoatType"));
                        boat = new Boat(boatId, bt, boatModel, sailNumber, engineInfo, draft, width, boatLength, yearOfConstruction);
                    }
                    reader.Close();
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("sql fejl: " + sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
                return boat;
            }
        }

        public async Task UpdateBoat(Boat boat)
        {
            Boat existingBoat = await SearchBoat(boat.SailNumber);
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(_updateBoat, connection);
                    await command.Connection.OpenAsync();

                    command.Parameters.AddWithValue("@SailNumber", boat.SailNumber);
                    command.Parameters.AddWithValue("@Id", boat.Id);
                    command.Parameters.AddWithValue("@Model", boat.Model);
                    command.Parameters.AddWithValue("@EngineInfo", boat.EngineInfo);
                    command.Parameters.AddWithValue("@Length", boat.Length);
                    command.Parameters.AddWithValue("@Width", boat.Width);
                    command.Parameters.AddWithValue("@Draft", boat.Draft);
                    command.Parameters.AddWithValue("@YearOfConstruction", boat.YearOfConstruction);
                    command.Parameters.AddWithValue("@TheBoatType", boat.TheBoatType.ToString());

                    await command.ExecuteNonQueryAsync();

                    await command.Connection.CloseAsync();
                }
                catch (SqlException sqlex)
                {
                    Console.WriteLine("sql fejl: " + sqlex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                finally
                {

                }
            }

        }

        #endregion

    }
}
