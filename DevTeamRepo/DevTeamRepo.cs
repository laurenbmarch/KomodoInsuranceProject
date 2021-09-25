using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamRepository
{ 
    public class DevTeamRepo
    {
        private List<DevTeam> _listOfTeams = new List<DevTeam>();
        private int _countForTeamId = 0;

        //Create
        public void AddTeamToList(DevTeam devTeam)
        {
            devTeam.TeamId = ++_countForTeamId;
            _listOfTeams.Add(devTeam);
        }

        //Read
        public List<DevTeam> GetListofDevTeams()
        {
            return _listOfTeams;
        }

        //Update
        public bool UpdateExistingTeams(int existingTeamId, DevTeam newDevTeam)
        {
            //Find the Team
            DevTeam existingTeam = GetTeamByTeamIdNumber(existingTeamId);

            //Update the Team
            if (existingTeam != null)
            {
                existingTeam.ListOfDevelopersOnTeam = newDevTeam.ListOfDevelopersOnTeam; //Question -- is this right way to do this?
                existingTeam.TeamName = newDevTeam.TeamName;
                existingTeam.TeamId = newDevTeam.TeamId;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        public bool RemoveTeamFromList(int teamId)
        {
            DevTeam devTeam = GetTeamByTeamIdNumber(teamId);
            if(devTeam == null)
            {
                return false;
            }
            int initialCount = _listOfTeams.Count;
            _listOfTeams.Remove(devTeam);

            if(initialCount > _listOfTeams.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Helper Method for Updating and Deleting a Team
        public DevTeam GetTeamByTeamIdNumber(int teamIdNumber)
        {
            foreach (DevTeam devTeam in _listOfTeams)
            {
                if (devTeam.TeamId == teamIdNumber)
                {
                    return devTeam;
                }
            }
            return null;
        }
    }
}
