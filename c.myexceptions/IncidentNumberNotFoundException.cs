using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.c.myexceptions
{
        public class IncidentNumberNotFoundException : Exception
        {
            // Default constructor that uses a standard error message
            public IncidentNumberNotFoundException() : base("Incident number not found in the database.")
            {
            }

            // Constructor that allows you to pass a custom message
            public IncidentNumberNotFoundException(string message) : base(message)
            {
            }

            // Constructor that allows for chaining exceptions
            public IncidentNumberNotFoundException(string message, Exception innerException)
                : base(message, innerException)
            {
            }
        }
    }


