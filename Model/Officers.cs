using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Model
{
    public class Officer
    {
        public int OfficerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public int BadgeNumber { get; set; }

        public int Rank { get; set; }

        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public LawEnforcementAgency AgencyID1 { get; set; }

        // Parameterless constructor
        public Officer()
        {
        }

        // Parameterized constructor
        public Officer(int officerID, string firstName, string lastName,
                       int badgeNumber, int rank, string address,
                       string phoneNumber, LawEnforcementAgency agencyID)
        {
            OfficerID = officerID;
            FirstName = firstName;
            LastName = lastName;
            BadgeNumber = badgeNumber;
            Rank = rank;
            Address = address;
            PhoneNumber = phoneNumber;
            AgencyID1 = agencyID;
        }

        // Override ToString method
        public override string ToString()
        {
            return $"Officer ID: {OfficerID}, Name: {FirstName} {LastName}, " +
                   $"Badge Number: {BadgeNumber}, Rank: {Rank}, " +
                   $"Address: {Address}, Phone: {PhoneNumber}, Agency ID: {AgencyID1.AgencyID}";
        }
    }

}
