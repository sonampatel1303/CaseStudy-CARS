using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Model
{
    public class Evidence
    {
        private int EvidenceID { get; set; }

        private string Description { get; set; }

        private string LocationFound { get; set; }

       private Incident IncidentID1 { get; set; }

        public Evidence()
        {
        }

        // Parameterized constructor
        public Evidence(int evidenceID, string description, string locationFound, Incident incidentID)
        {
            EvidenceID = evidenceID;
            Description = description;
            LocationFound = locationFound;
            IncidentID1 = incidentID;
        }

        // Override ToString method
        public override string ToString()
        {
            return $"Evidence ID: {EvidenceID}, Description: {Description}, " +
                   $"Location Found: {LocationFound}, Incident ID: {IncidentID1.IncidentID}";
        }
    }

}
