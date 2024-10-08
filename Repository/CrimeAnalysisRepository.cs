using CaseStudy.c.myexceptions;
using CaseStudy.Model;
using CaseStudy.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Repository
{
   
        public class CrimeAnalysisRepository : ICrimeAnalysisService
        {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;
             
        public CrimeAnalysisRepository()
        {
            sqlConnection = new SqlConnection(DbConnUtil.GetConnString());
            cmd = new SqlCommand();
        }

        private List<Incident> incidents = new List<Incident>();


            private List<Case> cases = new List<Case>();

        //creating an incident
  
        public bool CreateIncident(Incident incident, string victimName, string suspectName)
        {
            if (incident == null)
            {
                return false; 
            }

            // Fetch existing IDs based on names passed as parameters
            int victimId = GetVictimIdByName(victimName);
            int suspectId = GetSuspectIdByName(suspectName);

            if (victimId == -1)
            {
                Console.WriteLine("Victim not found.");
                return false; 
            }

            if (suspectId == -1)
            {
                Console.WriteLine("Suspect not found.");
                return false; 
            }

          
            string getMaxIdQuery = "SELECT ISNULL(MAX(IncidentID), 0) FROM Incidents";
            cmd.CommandText = getMaxIdQuery;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();
            int maxIncidentId = (int)cmd.ExecuteScalar();
            int newIncidentId = maxIncidentId + 1; // Increment by 1

            // Step 2: Prepare the insert query
            string insertQuery = "INSERT INTO Incidents (IncidentID, IncidentType, IncidentDate, Location, Description, Status, VictimID, SuspectID) " +
                                  "VALUES (@IncidentID, @IncidentType, @IncidentDate, @Location, @Description, @Status, @VictimID, @SuspectID)";

            // Step 3: Set the command and parameters for the insert query
            cmd.CommandText = insertQuery;
            cmd.Parameters.Clear(); // Clear any existing parameters
            cmd.Parameters.AddWithValue("@IncidentID", newIncidentId);
            cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);
            cmd.Parameters.AddWithValue("@IncidentDate", incident.IncidentDate);
            cmd.Parameters.AddWithValue("@Location", incident.Location);
            cmd.Parameters.AddWithValue("@Description", incident.Description);
            cmd.Parameters.AddWithValue("@Status", "Open"); // Default status
            cmd.Parameters.AddWithValue("@VictimID", victimId); // Use fetched VictimID
            cmd.Parameters.AddWithValue("@SuspectID", suspectId); // Use fetched SuspectID

            // Step 4: Execute the insert query
            int rowsAffected = cmd.ExecuteNonQuery(); // Execute the insert
            sqlConnection.Close(); // Close the connection

            return rowsAffected > 0; // Return true if at least one row was affected
        }

        // Method to get Victim ID by name
        private int GetVictimIdByName(string victimName)
        {
            cmd.CommandText = "SELECT VictimID FROM Victims WHERE FirstName = @FirstName";
            cmd.Connection = sqlConnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FirstName", victimName);

            sqlConnection.Open();
            int victimId = (int)(cmd.ExecuteScalar() ?? -1); // Fetch the VictimID or return -1 if not found
            sqlConnection.Close();

            return victimId;
        }

        // Method to get Suspect ID by name
        private int GetSuspectIdByName(string suspectName)
        {
            cmd.CommandText = "SELECT SuspectID FROM Suspects WHERE FirstName = @FirstName";
            cmd.Connection = sqlConnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FirstName", suspectName);

            sqlConnection.Open();
            int suspectId = (int)(cmd.ExecuteScalar() ?? -1); // Fetch the SuspectID or return -1 if not found
            sqlConnection.Close();

            return suspectId;
        }



        public bool UpdateIncidentStatus(string status, int incidentId)
        {
            cmd.CommandText = "UPDATE Incidents SET Status = @Status WHERE IncidentID = @IncidentID";
            cmd.Connection = sqlConnection;

            // Set parameters
            cmd.Parameters.Clear(); // Clear previous parameters
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@IncidentID", incidentId);

            sqlConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0; // Return true if at least one row was affected
        }
        // Method to get a list of incidents within a date range
       

        public List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate)
        {
            List<Incident> incidents = new List<Incident>();
            cmd.CommandText = "SELECT * FROM Incidents WHERE IncidentDate BETWEEN @StartDate AND @EndDate";
            cmd.Connection = sqlConnection;

            // Set parameters
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Incident incident = new Incident
                {
                    IncidentID = (int)reader["IncidentID"],
                    IncidentType = (string)reader["IncidentType"],
                    IncidentDate = (DateTime)reader["IncidentDate"],
                   Location = (string)reader["Location"],
                  
                    Description = (string)reader["Description"],
                    Status = (string)reader["Status"],
                    VictimID1 = (int)reader["VictimID"],
                    SuspectID1 = (int)reader["SuspectID"]
                };
                incidents.Add(incident);
            }
            sqlConnection.Close();
            return incidents;
        }
        // Method to search for incidents based on incident type
      
        public List<Incident> SearchIncidents(Incident incident)
        {
            List<Incident> incidents = new List<Incident>();
            cmd.CommandText = "SELECT * FROM Incidents WHERE IncidentType = @IncidentType";
            cmd.Connection = sqlConnection;

            // Set parameters
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IncidentType", incident.IncidentType);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Incident foundIncident = new Incident
                {
                    IncidentID = (int)reader["IncidentID"],
                    IncidentType = (string)reader["IncidentType"],
                    IncidentDate = (DateTime)reader["IncidentDate"],
                    Location = (string)reader["Location"],
                    Description = (string)reader["Description"],
                    Status = (string)reader["Status"],
                    VictimID1 = (int)reader["VictimID"],
                    SuspectID1 = (int)reader["SuspectID"]
                };
                incidents.Add(foundIncident);
            }
            sqlConnection.Close();
            return incidents;
        }
      
        public (Report report, Officer officerDetails, string incidentType, string location) GenerateIncidentReport(int incidentId)
        {
            Report report = null;
            Officer officerDetails = null;
            string incidentType = null;
            string location = null;

            cmd.CommandText =@"
        SELECT 
            r.ReportID, 
            r.IncidentID, 
            r.ReportingOfficer, 
            r.ReportDate, 
            r.ReportDetails, 
            r.Status,
            o.FirstName, 
            o.BadgeNumber,
            i.IncidentType, 
            i.Location 
        FROM Reports r
        JOIN Officers o ON r.ReportingOfficer = o.OfficerID 
        JOIN Incidents i ON r.IncidentID = i.IncidentID
        WHERE r.IncidentID = @IncidentID";

            cmd.Connection = sqlConnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IncidentID", incidentId);

            sqlConnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                report = new Report
                {
                    ReportID = (int)reader["ReportID"],
                    IncidentID = (int)reader["IncidentID"],
                    ReportingOfficerID = (int)reader["ReportingOfficer"],
                    ReportDate = (DateTime)reader["ReportDate"],
                    ReportDetails = (string)reader["ReportDetails"],
                    Status = (string)reader["Status"],
                };

                // Fetch Officer details separately
                officerDetails = new Officer
                {
                   FirstName = (string)reader["FirstName"],
                    BadgeNumber = (int)reader["BadgeNumber"]
                };
                incidentType = (string)reader["IncidentType"];
                location = (string)reader["Location"];
            }

            reader.Close();
            sqlConnection.Close();

            return (report, officerDetails,incidentType,location); // Return both report and officer details
        }


        // Method to create a new case and associate it with incidents

        public Case CreateCase(int caseID, string caseDescription,int incidentsForCase)
        {
            // Manually setting the CaseID from the user input
            Case newCase = new Case
            {
                CaseID = caseID,  // Manually set CaseID
                Description = caseDescription,
                DateCreated = DateTime.Now,
                Incidents = incidentsForCase
            };

            cmd.CommandText = "INSERT INTO [Case] (CaseID, Description, DateCreate, IncidentID) VALUES (@CaseID, @Description, @DateCreate,@IncidentID)";
            cmd.Connection = sqlConnection;

            // Set parameters
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CaseID", newCase.CaseID);
            cmd.Parameters.AddWithValue("@Description", newCase.Description);
            cmd.Parameters.AddWithValue("@DateCreate", newCase.DateCreated);
            cmd.Parameters.AddWithValue("@IncidentID", newCase.Incidents);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return newCase; // Return the created case
        }

      
        public (Case caseDetails, List<Incident> incidents, List<Victim> victims) GetCaseDetails(int caseId)
        {
            // SQL query to join case, incident, and victim tables to fetch relevant details
            cmd.CommandText = @"
        SELECT 
            c.CaseID, 
            c.Description AS CaseDescription, 
            c.DateCreate, 
            i.IncidentID, 
            i.IncidentDate, 
            i.Description AS IncidentDescription, 
            v.VictimID,
            v.FirstName 
        FROM [Case] c
        JOIN Incidents i ON c.CaseID = i.CaseID
        JOIN Victims v ON i.VictimID = v.VictimID
        WHERE c.CaseID = @CaseID";

            cmd.Connection = sqlConnection;

            // Set parameters
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CaseID", caseId);

            Case caseDetails = null;
            List<Incident> incidents = new List<Incident>();
            List<Victim> victims = new List<Victim>();

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Initialize case details only once
                if (caseDetails == null)
                {
                    caseDetails = new Case
                    {
                        CaseID = (int)reader["CaseID"],
                        Description = (string)reader["CaseDescription"],
                        DateCreated = (DateTime)reader["DateCreate"]
                    };
                }

                // Create an Incident object for each incident
                Incident incident = new Incident
                {
                    IncidentID = (int)reader["IncidentID"],
                    IncidentDate = (DateTime)reader["IncidentDate"],
                    Description = (string)reader["IncidentDescription"]
                };
                incidents.Add(incident);

                // Create a Victim object for each victim
                Victim victim = new Victim
                {
                    VictimID = (int)reader["VictimID"],
                    FirstName = (string)reader["FirstName"]
                };
                victims.Add(victim);
            }

            reader.Close();
            sqlConnection.Close();

            // Return the Case object, the list of incidents, and the list of victims
            return (caseDetails, incidents, victims);
        }




        // Method to update case details

        public bool UpdateCaseDetails(Case caseObj)
        {
            cmd.CommandText = "UPDATE [Case] SET Description = @Description WHERE caseID = @CaseID";
            cmd.Connection = sqlConnection;

            // Set parameters
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Description", caseObj.Description);
            cmd.Parameters.AddWithValue("@CaseID", caseObj.CaseID);

            sqlConnection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rowsAffected > 0; // Return true if at least one row was affected
        }

        // Method to get a list of all cases
        public List<Case> GetAllCases()
            {
            cmd.CommandText = "select * from [Case]";
            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Case case1 = new Case();
                case1.CaseID = (int)reader["caseID"];
                case1.Description = (string)reader["Description"];
                case1.DateCreated = (DateTime)reader["DateCreate"];
             
                cases.Add(case1);
            }
            sqlConnection.Close();
            return cases;
            }


      //Get an incident by id
        public Incident GetIncidentById(int incidentId)
        {
            cmd.CommandText = "SELECT * FROM Incidents WHERE IncidentID = @IncidentID";
            cmd.Connection = sqlConnection;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IncidentID", incidentId);

            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Incident incident = null;
            if (reader.Read())
            {
                incident = new Incident
                {
                    IncidentID = (int)reader["IncidentID"],
                    IncidentType = (string)reader["IncidentType"],
                    IncidentDate = (DateTime)reader["IncidentDate"],
                  Location = (string)reader["Location"],
                  
                    Description = (string)reader["Description"],
                    Status = (string)reader["Status"],
                    VictimID1 = (int)reader["VictimID"],
                    SuspectID1 = (int)reader["SuspectID"]
                };
            }

            sqlConnection.Close();

            // Throw custom exception if incident not found
            if (incident == null)
            {
                throw new IncidentNumberNotFoundException($"No incident found with ID: {incidentId}");
            }

            return incident;
        }

      
        public bool FileIncidentReport(int incidentId, string officerName, string reportDetails, DateTime reportDate)
        {
            try
            {
                // Get Officer ID by Officer Name
                int officerId = (int)GetOfficerIdByName(officerName);

                if (officerId == -1) // Assuming -1 indicates officer not found
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Officer not found.");
                    Console.ResetColor();
                    return false;
                }

                // Generate a new Report ID
                string getMaxIdQuery = "SELECT ISNULL(MAX(ReportID), 0) FROM Reports";
                cmd.CommandText = getMaxIdQuery;
                cmd.Connection = sqlConnection;

                sqlConnection.Open();
                int maxReportId = (int)cmd.ExecuteScalar();
                int newReportId = maxReportId + 1;

                // Prepare the insert query for the report
                string insertQuery = "INSERT INTO Reports (ReportID, IncidentID, ReportingOfficer, ReportDate, ReportDetails, Status) " +
                                     "VALUES (@ReportID, @IncidentID, @ReportingOfficer, @ReportDate, @ReportDetails, @Status)";
                cmd.CommandText = insertQuery;

                // Set parameters
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ReportID", newReportId);
                cmd.Parameters.AddWithValue("@IncidentID", incidentId);
                cmd.Parameters.AddWithValue("@ReportingOfficer", officerId);
                cmd.Parameters.AddWithValue("@ReportDate", reportDate);
                cmd.Parameters.AddWithValue("@ReportDetails", reportDetails);
                cmd.Parameters.AddWithValue("@Status", "Open");

                // Execute the insert query
                int rowsAffected = cmd.ExecuteNonQuery();
                sqlConnection.Close();

                if (rowsAffected > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    //Console.WriteLine("Report filed successfully.");
                    Console.ResetColor();
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to file the report.");
                    Console.ResetColor();
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error filing report: {ex.Message}");
                Console.ResetColor();
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

      
        public int? GetOfficerIdByName(string officerName)
        {
            try
            {
                cmd.CommandText = "SELECT OfficerID FROM Officers WHERE FirstName = @OfficerName";
                cmd.Connection = sqlConnection;
                cmd.CommandTimeout = 10; // 10-second timeout for the command

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@OfficerName", officerName);

                sqlConnection.Open();
                object result = cmd.ExecuteScalar();
                sqlConnection.Close();

                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error retrieving officer ID: {ex.Message}");
                Console.ResetColor();
                sqlConnection.Close();
                return null;
            }
        }
        public int GetLatestIncidentId()
        {
            string getMaxIdQuery = "SELECT ISNULL(MAX(IncidentID), 0) FROM Incidents";
            cmd.CommandText = getMaxIdQuery;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();
            int maxIncidentId = (int)cmd.ExecuteScalar();
            sqlConnection.Close();

            return maxIncidentId; // Return the latest Incident ID
        }


    }
}


