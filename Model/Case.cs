using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Model
{
    public class Case
    {
        
        public int CaseID { get; set; }           
        public string Description { get; set; }    
        public DateTime DateCreated { get; set; }  
        public int Incidents { get; set; }  

       
        public Case()
        {
            
        }

        // Parameterized constructor
        public Case(int caseID, string description, DateTime dateCreated, int incidents)
        {
            CaseID = caseID;
            Description = description;
            DateCreated = dateCreated;
            Incidents = incidents;
        }

        // Override ToString method
        public override string ToString()
        {
            return $"Case ID: {CaseID}, Description: {Description}, " +
                   $"Date Created: {DateCreated.ToShortDateString()}, " 
                   
                  ;
        }
    }

}
