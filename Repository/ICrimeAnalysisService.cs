using CaseStudy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CaseStudy.Repository.CrimeAnalysisRepository;

namespace CaseStudy.Repository
{
        public interface ICrimeAnalysisService
        {
         
            bool CreateIncident(Incident incident,string name1,string name2);

           
            bool UpdateIncidentStatus(string status, int incidentId);

         
            List<Incident> GetIncidentsInDateRange(DateTime startDate, DateTime endDate);

           
            List<Incident> SearchIncidents(Incident incidentType);

           (Report report, Officer officerDetails, string incidentType, string location) GenerateIncidentReport(int id);

          
            Case CreateCase(int caseid,string caseDescription, int incidents);

        (Case caseDetails, List<Incident> incidents, List<Victim> victims )GetCaseDetails(int caseId);

            bool UpdateCaseDetails(Case caseObj);
            List<Case> GetAllCases();

        public Incident GetIncidentById(int incidentId);
        }
    }


