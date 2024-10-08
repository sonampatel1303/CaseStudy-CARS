using CaseStudy.Utility;

using System;
using System.Collections.Generic;
using CaseStudy.Model;
using CaseStudy.Repository;
using CaseStudy.c.myexceptions;
using static CaseStudy.Repository.CrimeAnalysisRepository;
using Spectre.Console;

namespace CaseStudy.main
{
    public class MainModule
    {
        private CrimeAnalysisRepository crimeAnalysisRepository;

        public MainModule()
        {
            crimeAnalysisRepository = new CrimeAnalysisRepository(); 
        }

       
        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== Crime Analysis System Main Menu =====");
                Console.WriteLine("1. Incidents");
                Console.WriteLine("2. Reports");
                Console.WriteLine("3. Cases");
                Console.WriteLine("4. Exit");
                Console.Write("Select a main option: ");
                string mainChoice = Console.ReadLine();

                switch (mainChoice)
                {
                    case "1":
                        DisplayIncidentMenu();
                        break;
                    case "2":
                        DisplayReportMenu();
                        break;
                    case "3":
                        DisplayCaseMenu();
                        break;
                    case "4":
                        exit = true;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Incident submenu
        private void DisplayIncidentMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n===== Incident Menu =====");
                Console.WriteLine("1. Create Incident");
                Console.WriteLine("2. Update Incident Status");
                Console.WriteLine("3. Search Incidents");
                Console.WriteLine("4. Get Incidents in Date Range");
                Console.WriteLine("5. Get Incident By ID");
                Console.WriteLine("6. Back to Main Menu");
                Console.Write("Select an option: ");
                string incidentChoice = Console.ReadLine();

                switch (incidentChoice)
                {
                    case "1":
                        CreateIncident();
                        break;
                    case "2":
                        UpdateIncidentStatus();
                        break;
                    case "3":
                        SearchIncidents();
                        break;
                    case "4":
                        GetIncidentsInDateRange();
                        break;
                    case "5":
                        GetIncidentById();
                        break;
                    case "6":
                        back = true; // Exit submenu
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Report submenu
        private void DisplayReportMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n===== Report Menu =====");
                Console.WriteLine("1. Generate Incident Report");
                Console.WriteLine("2. File Incident Report");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select an option: ");
                string reportChoice = Console.ReadLine();

                switch (reportChoice)
                {
                    case "1":
                        GenerateIncidentReport();
                        break;
                    case "2":
                        FileIncidentReport();
                        break;
                    case "3":
                        back = true; // Exit submenu
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        // Case submenu
        private void DisplayCaseMenu()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\n===== Case Menu =====");
                Console.WriteLine("1. Create Case");
                Console.WriteLine("2. Update Case Details");
                Console.WriteLine("3. Get Case Details");
                Console.WriteLine("4. Get All Cases");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Select an option: ");
                string caseChoice = Console.ReadLine();

                switch (caseChoice)
                {
                    case "1":
                        CreateCase();
                        break;
                    case "2":
                        UpdateCaseDetails();
                        break;
                    case "3":
                        GetCaseDetails();
                        break;
                    case "4":
                        GetAllCases();
                        break;
                    case "5":
                        back = true; // Exit submenu
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

      

        private void CreateIncident()
        {
            Console.WriteLine("Create Incident");

            // Collecting incident details
            Incident newIncident = new Incident();

            Console.Write("Enter Incident Type: ");
            newIncident.IncidentType = Console.ReadLine();

            Console.Write("Enter Incident Date (yyyy-mm-dd): ");
            newIncident.IncidentDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Location: ");
            newIncident.Location = Console.ReadLine();

            Console.Write("Enter Description: ");
            newIncident.Description = Console.ReadLine();

            // Collecting victim and suspect names
            Console.Write("Enter Victim Name: ");
            string victimName = Console.ReadLine();

            Console.Write("Enter Suspect Name: ");
            string suspectName = Console.ReadLine();

            // Call the CreateIncident method
            bool result = crimeAnalysisRepository.CreateIncident(newIncident, victimName, suspectName);
            int incidentid1 = crimeAnalysisRepository.GetLatestIncidentId();
            if (result)
            {
                Console.WriteLine($"Incident created successfully. with ID :{incidentid1}");
            }
            else
            {
                Console.WriteLine("Failed to create incident.");
            }
        }

       
        private void UpdateIncidentStatus()
        {
            Console.Write("Enter Incident ID: ");
            int incidentId;
            while (!int.TryParse(Console.ReadLine(), out incidentId))
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the Incident ID:");
            }

            string status = null;
            bool validStatus = false;

            while (!validStatus)
            {
                Console.Write("Enter new status (Open, Closed, Under Investigation): ");
                status = Console.ReadLine();

                if (status == "Open" || status == "Closed" || status == "Under Investigation")
                {
                    validStatus = true;
                }
                else
                {
                    Console.WriteLine("Invalid status. Please enter one of the following: Open, Closed, Under Investigation.");
                }
            }

            try
            {
                bool isUpdated = crimeAnalysisRepository.UpdateIncidentStatus(status, incidentId);
                if (isUpdated)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Incident status updated successfully.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to update status.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
            }
        }


        private void GetIncidentsInDateRange()
        {
            Console.Write("Enter start date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid start date format. Please try again.");
                return;
            }

            Console.Write("Enter end date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid end date format. Please try again.");
                return;
            }

            List<Incident> incidents = crimeAnalysisRepository.GetIncidentsInDateRange(startDate, endDate);

            if (incidents.Count > 0)
            {
                // Create a new table with Spectre.Console
                var table = new Spectre.Console.Table();

                // Define table columns
                table.AddColumn("Incident ID");
                table.AddColumn("Incident Type");
                table.AddColumn("Date");
                table.AddColumn("Location");
                table.AddColumn("Status");

                // Add rows with incident details
                foreach (var incident in incidents)
                {
                    table.AddRow(
                        incident.IncidentID.ToString(),
                        incident.IncidentType,
                        incident.IncidentDate.ToString("yyyy-MM-dd"),
                        incident.Location,
                        incident.Status
                    );
                }

                // Render the table
                Spectre.Console.AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("No incidents found in the specified date range.");
            }
        }

      
        private void SearchIncidents()
        {
            Console.WriteLine("Enter Incident Type");
            Incident searchCriteria = new Incident { IncidentType = Console.ReadLine() };

            // Fetch incidents matching the search criteria
            List<Incident> incidents = crimeAnalysisRepository.SearchIncidents(searchCriteria);

            if (incidents.Count > 0)
            {
                // Create a new table using Spectre.Console
                var table = new Spectre.Console.Table();

                // Define table columns
                table.AddColumn("Incident ID");
                table.AddColumn("Incident Type");
                table.AddColumn("Date");
                table.AddColumn("Location");
                table.AddColumn("Status");

                // Add rows with incident details
                foreach (var incident in incidents)
                {
                    table.AddRow(
                        incident.IncidentID.ToString(),
                        incident.IncidentType,
                        incident.IncidentDate.ToString("yyyy-MM-dd"),
                        incident.Location,
                        incident.Status
                    );
                }

                // Render the table
                Spectre.Console.AnsiConsole.Write(table);
            }
            else
            {
                Console.WriteLine("No incidents found for the specified incident type.");
            }
        }


        private void GenerateIncidentReport()
        {
            Console.Write("Enter Incident ID: ");

            if (!int.TryParse(Console.ReadLine(), out int incidentId))
            {
                Console.WriteLine("Invalid Incident ID. Please enter a valid number.");
                return;
            }

            // Fetch the incident report, officer details, incident type, and location
            (Report report, Officer officerDetails, string incidentType, string location) = crimeAnalysisRepository.GenerateIncidentReport(incidentId);

            if (report != null && officerDetails != null)
            {
                
                var table = new Spectre.Console.Table();

              
                table.AddColumn("Field");
                table.AddColumn("Details");

                // Add rows for each field and value
                table.AddRow("Incident ID", report.IncidentID.ToString());
                table.AddRow("Incident Type", incidentType);
                table.AddRow("Location", location);
                table.AddRow("Report ID", report.ReportID.ToString());
                table.AddRow("Reporting Officer", officerDetails.FirstName);
                //table.AddRow("Badge Number", officerDetails.BadgeNumber);
                table.AddRow("Report Date", report.ReportDate.ToString("yyyy-MM-dd"));
                table.AddRow("Report Details", report.ReportDetails);
                table.AddRow("Status", report.Status);

              
                Spectre.Console.AnsiConsole.Write(table);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to generate the report or officer details not found.");
                Console.ResetColor();
            }
        }


        private void CreateCase()
        {
            Console.WriteLine("Enter case id");
            int caseid = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter incident id");
            int incidents = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter description");
            string descript = Console.ReadLine();
            Case newCase = crimeAnalysisRepository.CreateCase(caseid, descript, incidents);
            Console.WriteLine(newCase != null ? $"Case Created: {newCase.Description}" : "Failed to create case.");
        }

        private void GetCaseDetails()
        {
            Console.Write("Enter Case ID: ");

            if (!int.TryParse(Console.ReadLine(), out int caseId))
            {
                Console.WriteLine("Invalid Case ID. Please enter a valid number.");
                return;
            }

            // Fetch the case, incident, and victim details
            (Case caseDetails, List<Incident> incidents, List<Victim> victims) = crimeAnalysisRepository.GetCaseDetails(caseId);

          
            if (caseDetails == null)
            {
                Console.WriteLine("Case not found.");
                return;
            }

            // Create a table to display all details
            var detailsTable = new Table();
            detailsTable.AddColumn("Case ID");
            detailsTable.AddColumn("Case Description");
            detailsTable.AddColumn("Date Created");
            detailsTable.AddColumn("Incident ID");
            detailsTable.AddColumn("Incident Date");
            detailsTable.AddColumn("Incident Description");
           // detailsTable.AddColumn("Victim Name");

            // Add a row for each incident related to the case
            foreach (var incident in incidents)
            {
              
               
                detailsTable.AddRow(
                    caseDetails.CaseID.ToString(),
                    caseDetails.Description,
                    caseDetails.DateCreated.ToString("yyyy-MM-dd"),
                    incident.IncidentID.ToString(),
                    incident.IncidentDate.ToString("yyyy-MM-dd"),
                    incident.Description
                    //victim.FirstName
                );
            }

           
            AnsiConsole.Write(detailsTable);
        }


        private void UpdateCaseDetails()
        {
            Console.WriteLine("Enter caseID and description");
            Case caseObj = new Case
            {
                CaseID = int.Parse(Console.ReadLine()),
                Description = Console.ReadLine()
            };

            bool isUpdated = crimeAnalysisRepository.UpdateCaseDetails(caseObj);
            if (isUpdated) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Case Details Updated");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to update case");
                Console.ResetColor();

            }
           // Console.WriteLine(isUpdated ? "Case details updated." : "Failed to update case.");
        }

        private void GetAllCases()
        {
            List<Case> cases = crimeAnalysisRepository.GetAllCases();
            cases.ForEach(caseObj => Console.WriteLine($"Case ID: {caseObj.CaseID}, Description is :{caseObj.Description}"));
        }
        private void GetIncidentById()
        {
            Console.Write("Enter Incident ID: ");
            int incidentId;

          
            if (int.TryParse(Console.ReadLine(), out incidentId))
            {
                try
                {
                   
                    Incident incident = crimeAnalysisRepository.GetIncidentById(incidentId);

                   
                    Console.WriteLine($"Incident ID: {incident.IncidentID}");
                    Console.WriteLine($"Incident Type: {incident.IncidentType}");
                    Console.WriteLine($"Incident Date: {incident.IncidentDate}");
                    Console.WriteLine($"Location: {incident.Location}");
                   
                    Console.WriteLine($"Description: {incident.Description}");
                    Console.WriteLine($"Status: {incident.Status}");
                    Console.WriteLine($"Victim ID: {incident.VictimID1}");
                    Console.WriteLine($"Suspect ID: {incident.SuspectID1}");
                }
                catch (IncidentNumberNotFoundException ex)
                {
                    Console.WriteLine(ex.Message); // Custom error message if the incident is not found
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}"); // General error handling
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid Incident ID.");
            }
        }
        private void FileIncidentReport()
        {
            Console.Write("Enter Incident ID: ");
            if (!int.TryParse(Console.ReadLine(), out int incidentId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Incident ID. Please enter a valid number.");
                Console.ResetColor();
                return;
            }

            Console.Write("Enter Officer Name: ");
            string officerName = Console.ReadLine();

            Console.Write("Enter Report Details: ");
            string reportDetails = Console.ReadLine();

            Console.Write("Enter Report Date (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime reportDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
                Console.ResetColor();
                return;
            }

            // File the report
            bool isReportFiled = crimeAnalysisRepository.FileIncidentReport(incidentId, officerName, reportDetails, reportDate);
            if (isReportFiled)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Incident report filed successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Failed to file incident report.");
            }
            Console.ResetColor();
        }


    }
}
