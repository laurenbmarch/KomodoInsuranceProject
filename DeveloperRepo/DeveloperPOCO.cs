using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperRepository
{
    public class Developer
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int DevIdNumber { get; set; }
        public bool HasPluralsightAccess { get; set;}

        public Developer () { }
        
        public Developer (string lastName, string firstName, bool hasPluralsightAccess)
        {
            LastName = lastName;
            FirstName = firstName;
            HasPluralsightAccess = hasPluralsightAccess;
        }
    }
}
