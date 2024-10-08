using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Model
{
    public class LawEnforcementAgency
    {
        public int AgencyID { get; set; }

        public string AgencyName { get; set; }

        public string Jurisdiction { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public List<Officer> Officers { get; set; }

        // Parameterless constructor
        public LawEnforcementAgency()
        {
            Officers = new List<Officer>();
        }

        // Parameterized constructor
        public LawEnforcementAgency(int agencyID, string agencyName, string jurisdiction,
                                     string address, string phoneNumber, List<Officer> officers)
        {
            AgencyID = agencyID;
            AgencyName = agencyName;
            Jurisdiction = jurisdiction;
            Address = address;
            PhoneNumber = phoneNumber;
            Officers = officers ?? new List<Officer>();
        }

        // Override ToString method
        public override string ToString()
        {
            return $"Agency ID: {AgencyID}, Name: {AgencyName}, " +
                   $"Jurisdiction: {Jurisdiction}, Address: {Address}, " +
                   $"Phone: {PhoneNumber}, Officers Count: {Officers.Count}";
        }
    }

}
