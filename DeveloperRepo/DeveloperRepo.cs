using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperRepository
{ 
    public class DeveloperRepo
    {
        private List<Developer> _listOfDevelopers = new List<Developer>();
        private int _countForId = 0; 

        //Create
        public void AddDeveloperToList(Developer developer)
        {
            developer.DevIdNumber = ++_countForId;
            _listOfDevelopers.Add(developer);
        }

        //Read
        public List<Developer> GetDeveloperList()
        {
            return _listOfDevelopers;
        }

        //Update
        public bool UpdateExistingDeveloper(int existingDeveloperIdNumber, Developer newDeveloper)
        {
            //Find the Developer
            Developer existingDeveloper = GetDeveloperByIdNumber(existingDeveloperIdNumber);

            //Update the Content
            if (existingDeveloper != null)
            {
                existingDeveloper.LastName = newDeveloper.LastName;
                existingDeveloper.FirstName = newDeveloper.FirstName;
                
                existingDeveloper.HasPluralsightAccess = newDeveloper.HasPluralsightAccess;
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete
        public bool RemoveDeveloperFromList(int devIdNumber)
        {
            Developer developer = GetDeveloperByIdNumber(devIdNumber);
            if(developer == null)
            {
                return false;
            }
            int initialCount = _listOfDevelopers.Count;
            _listOfDevelopers.Remove(developer);

            if (initialCount > _listOfDevelopers.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Helper Method for Updating
        public Developer GetDeveloperByIdNumber(int devIdNumber)
        {
            foreach(Developer developer in _listOfDevelopers)
            {
                if (developer.DevIdNumber == devIdNumber)
                {
                    return developer;
                }
            }

            return null;
        }
    }
}
