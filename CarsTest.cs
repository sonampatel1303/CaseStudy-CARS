using CaseStudy.Model;
using CaseStudy.Repository;
using NUnit.Framework;

namespace CARS.Tests
{
    public class CarsTest
    {
        [Test]
        //[Ignore("Already tested")]
        public void Test_To_IncidentCreation() { 
        ICrimeAnalysisService analysisService=new CrimeAnalysisRepository();
            string name1 = "Vikram";
            string name2 = "Geeta";
            Incident incident = new Incident()
            {
               
                IncidentType = "Dowry",
                IncidentDate = DateTime.Now,
                Location = "Hyderabad",
                Description = "Happened at victim in law house",
                Status = "Open",
                VictimID1 = 5,
                SuspectID1 = 4

            };
            bool isCreated = analysisService.CreateIncident(incident, name1, name2);
                Assert.That(isCreated, Is.True);
                
        }
        [Test]
        public void Test_To_UpdateStatus()
        {
            ICrimeAnalysisService service=new CrimeAnalysisRepository();
            string newstatus = "Open";
            bool result = service.UpdateIncidentStatus(newstatus,3);
            Assert.That(result, Is.True);

        }

    }
}
