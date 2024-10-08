using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Model
{
   public class Suspect
    {
        public int SuspectID { get; set; }

        public string FirstName { get; set; }

       public string LastName { get; set; }

       public DateTime DateOfBirth { get; set; }

       public string Gender { get; set; }

        public string Address { get; set; }
       public string PhoneNumber { get; set; }

        // Parameterless constructor
      public Suspect()
        {
        }

        // Parameterized constructor
       public Suspect(int suspectID, string firstName, string lastName,
                       DateTime dateOfBirth, string gender, string address,
                       string phoneNumber)
        {
            SuspectID = suspectID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        // Override ToString method
        public override string ToString()
        {
            return $"Suspect ID: {SuspectID}, Name: {FirstName} {LastName}, " +
                   $"DOB: {DateOfBirth.ToShortDateString()}, Gender: {Gender}, " +
                   $"Address: {Address}, Phone: {PhoneNumber}";
        }
    }

}
