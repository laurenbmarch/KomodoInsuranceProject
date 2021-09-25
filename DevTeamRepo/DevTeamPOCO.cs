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

        public void AddDeveloperToTeam (Developer developer, DevTeam devTeam)
        {
            devTeam.ListOfDevelopersOnTeam.Add(developer);
        }

        public void RemoveDeveloperFromTeam (Developer developer, DevTeam devTeam)
        {
            devTeam.ListOfDevelopersOnTeam.Remove(developer);
        }

        public List<Developer> GetListofDevelopersOnATeam()
        {
            return this.ListOfDevelopersOnTeam;
        }

    }
}
