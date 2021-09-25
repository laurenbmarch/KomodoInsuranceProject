using DeveloperRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamRepository
{
    public class DevTeam
    {
        public List<Developer> ListOfDevelopersOnTeam { get; set; } = new List<Developer>(); 
        public string TeamName { get; set; }
        public int TeamId { get; set; }

        

        public DevTeam() { }
        public DevTeam(List<Developer> listOfTeamMembers, string teamName) 
        {
            ListOfDevelopersOnTeam = listOfTeamMembers;
            TeamName = teamName;
            
        }
    }
}
