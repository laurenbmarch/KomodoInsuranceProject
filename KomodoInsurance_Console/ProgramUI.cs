using DeveloperRepository;
using DevTeamRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoInsurance_Console
{
    public class ProgramUI
    {
        private DeveloperRepo _developerRepo = new DeveloperRepo(); 
        private DevTeamRepo _devTeamRepo = new DevTeamRepo(); 

        //Run the UI
        public void Run()
        {
            SeedDeveloperList();
            Menu();
        }

        //Menu
        private void Menu()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.WriteLine("Select a menu option:\n\n"+
                    "Developers:\n"+
                    "\t1. Create New Developer\n" +
                    "\t2. View All Developers\n" +
                    "\t3. Find Developer By ID Number\n" +
                    "\t4. Update Existing Developer\n" +
                    "\t5. Delete Existing Developer\n" +
                    "\t6. Add a Developer to Team\n" +
                    "\t7. Remove a Developer from a Team\n" +
                    "\t8. View Developers Who Need Pluralsight Access\n\n" +
                    
                    "Development Teams:\n" +
                    "\t9. Create New Development Team\n" +
                    "\t10. View All Development Teams\n" +
                    "\t11. Find Development Team By Team ID Number\n" +
                    "\t12. Update Existing Development Team\n"+   
                    "\t13. Delete Existing Development Team\n\n" +
                    
                    "14. Exit");

                string userInput = Console.ReadLine();

                switch(userInput)
                {
                    case "1":
                        //Create New Developer
                        AddDeveloperToList();  
                        break;
                    case "2":
                        //View All Developers By ID Number
                        DisplayDeveloperList();
                        break;
                    case "3":
                        //Find Individual Developer By ID Number
                        DisplayDeveloperByIdNumber();
                        break;
                    case "4":
                        //Update Existing Developer
                        UpdateExistingDeveloper();
                        break;
                    case "5":
                        //Delete Existing Developer
                        RemoveDeveloperFromList();
                        break;
                    case "6":
                        //Add a developer to a team
                        AddADeveloperToADevTeam();
                        break;
                    case "7":
                        //Remove a developer from a team
                        RemoveADeveloperFromDevTeam();
                        break;
                    case "8":
                        //Display list of those who need Pluralsight
                        DisplayDevelopersWhoNeedPluralsight();
                        break;
                    case "9":
                        //Create New Development Team
                        AddTeamToList();
                        break;
                    case "10":
                        //View All Deveopment Teams by Team ID Number
                        DisplayListofDevTeams();
                        break;
                    case "11":
                        //Find Development Team By ID Number
                        DisplayTeamByTeamIdNumber();
                        break;
                    
                    case "12":
                        //Update Existing Development Team
                        UpdateExistingTeams();
                            break;
                    
                    case "13":
                        //Delete Existing Development Team
                        RemoveTeamFromList();
                        break;
                    
                    case "14":
                        Console.WriteLine("Exiting Now");
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
                Console.WriteLine("Please press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        //Create New Developer
        private void AddDeveloperToList()
        {
            Console.Clear();
            Developer newDeveloper = new Developer();

            //Last Name
            Console.WriteLine("Enter the last name of the new developer:");
            newDeveloper.LastName = Console.ReadLine();

            //First Name
            Console.WriteLine("Enter the first name of the new developer:");
            newDeveloper.FirstName = Console.ReadLine();

            //Access
            Console.WriteLine("Does the developer have access to Pluralsight? (yes/no)");
            string accessToPluralsightAsString = Console.ReadLine().ToLower();

            if (accessToPluralsightAsString == "yes")
            {
                newDeveloper.HasPluralsightAccess = true;
            }
            else
            {
                newDeveloper.HasPluralsightAccess = false;
            }

            _developerRepo.AddDeveloperToList(newDeveloper);
        }

        //Create New Dev Team
        private void AddTeamToList()
        {
            Console.Clear();
            DevTeam newDevTeam = new DevTeam();

            Console.WriteLine("Enter the name of the Development Team:");
            newDevTeam.TeamName = Console.ReadLine();
            newDevTeam.ListOfDevelopersOnTeam = AddDevelopersToDevTeam();

            _devTeamRepo.AddTeamToList(newDevTeam);
        }

        //Add Existing Developers to a Team
        private List<Developer> AddDevelopersToDevTeam()
        {
            List<Developer> listOfDevelopersToBeAdded = new List<Developer>();
            bool userIsCompletedAdding = true;
            while(userIsCompletedAdding)
            {
                Console.Clear();
                DisplayDeveloperList();
                Console.WriteLine("Enter the developer's ID Number that you want to add to the team:");
                int developerId = int.Parse(Console.ReadLine());
                Developer developerToBeAdded = _developerRepo.GetDeveloperByIdNumber(developerId);
                listOfDevelopersToBeAdded.Add(developerToBeAdded);
                Console.WriteLine("Would you like to add another developer?(yes/no)");
                string wantsToAddAnotherDeveloper = Console.ReadLine();
                if (wantsToAddAnotherDeveloper == "no")
                {
                    userIsCompletedAdding = false;
                }
            }
            return listOfDevelopersToBeAdded;
        }

        //Add New Developers to a Team
        public List<Developer> AddADeveloperToADevTeam()
        {
            Console.Clear();
            List<Developer> listOfDevelopersToBeAdded = AddDevelopersToDevTeam();
            DisplayListofDevTeams();
            Console.WriteLine("Enter the Team ID for the developer team that you would like to add:");
            int teamId = int.Parse(Console.ReadLine());
            DevTeam devTeam = _devTeamRepo.GetTeamByTeamIdNumber(teamId);
            foreach (var developer in listOfDevelopersToBeAdded)
            {
                devTeam.AddDeveloperToTeam(developer, devTeam);
            }
            return devTeam.GetListofDevelopersOnATeam();
        }

        //Remove a devloper from a Team
        public void RemoveADeveloperFromDevTeam()
        {
            DisplayListofDevTeams();
            Console.WriteLine("Enter the ID number of the developer that you would like to remove:");
            int developerId = int.Parse(Console.ReadLine());
            Developer developer = _developerRepo.GetDeveloperByIdNumber(developerId);
            Console.WriteLine("Enter the Team ID number for the developer you would like to remove:");
            int teamId = int.Parse(Console.ReadLine());
            DevTeam devTeam = _devTeamRepo.GetTeamByTeamIdNumber(teamId);
            devTeam.RemoveDeveloperFromTeam(developer, devTeam);
  
        }

        //Show List of All Developers
        private void DisplayDeveloperList()
        {
            Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetDeveloperList();

            foreach (Developer developer in listOfDevelopers)
            {
                Console.WriteLine($"Name: {developer.FirstName}{" "}{developer.LastName}\n" +
                    $"ID Number: {developer.DevIdNumber}\n" +
                    $"Access to Pluralsight: {ReturnYesOrNo(developer.HasPluralsightAccess)}\n");
            }

        }

        //Show List of All Dev Teams
        private void DisplayListofDevTeams()
        {
            Console.Clear();
            List<DevTeam> listOfDevTeams = _devTeamRepo.GetListofDevTeams();

            foreach (DevTeam developmentTeam in listOfDevTeams)
            {
                Console.WriteLine($"Team Name: {developmentTeam.TeamName}\n" +
                    $"Team ID Number: {developmentTeam.TeamId}\n" +
                    $"Developers on the Team: ");
                foreach (Developer developer in developmentTeam.ListOfDevelopersOnTeam)
                {
                    Console.WriteLine($"\tName: {developer.FirstName}{" "}{developer.LastName}\n" +
                        $"\tID Number: {developer.DevIdNumber}\n" +
                        $"\tPluralsight Access: {ReturnYesOrNo(developer.HasPluralsightAccess)}\n\n");
                }
            }
        }

        //Show One Developer
        private void DisplayDeveloperByIdNumber()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID Number of the Developer you would like to view:");

            int idNumber;
            try
            {
                idNumber = int.Parse(Console.ReadLine()); //Question: I need an option for if the user does not enter an integer
                Developer developer = _developerRepo.GetDeveloperByIdNumber(idNumber);

                if (developer != null)
                {
                    Console.WriteLine($"Name: {developer.FirstName}{" "}{developer.LastName}\n" +
                        $"ID Number: {developer.DevIdNumber}\n" +
                        $"Pluralsight Access: {ReturnYesOrNo(developer.HasPluralsightAccess)}");
                }
                else
                {
                    Console.WriteLine("There is no Developer in the database with that ID Number.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection.");                
            }

        }

        //Show One Dev Team
        private void DisplayTeamByTeamIdNumber()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID Number of the Development Team you would like to access:");

            int teamIdNumber;
            try
            {
                teamIdNumber = int.Parse(Console.ReadLine()); 

                DevTeam devTeam = _devTeamRepo.GetTeamByTeamIdNumber(teamIdNumber);

                if (devTeam != null)
                {
                    Console.WriteLine($"Team Name: {devTeam.TeamName}\n" +
                        $"Team ID Number: {devTeam.TeamId}\n" +
                        $"\tList of Developers on the team:");
                    foreach (Developer developer in devTeam.ListOfDevelopersOnTeam)
                    {
                        Console.WriteLine($"\tName: {developer.FirstName}{" "}{developer.LastName}\n" +
                            $"\tID Number: {developer.DevIdNumber}\n" +
                            $"\tPluralsight Access: {developer.HasPluralsightAccess}\n\n");
                    }
                }
                else
                {
                    Console.WriteLine("There is no Development Team with that ID Number.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
            }
                   
        }

        //Update Individual Developer
        private void UpdateExistingDeveloper()
        {
            DisplayDeveloperList();
            Console.WriteLine("Enter the ID Number of the Developer you would like to update:");

            int oldDevIdNumber;
            try
            {
                oldDevIdNumber = int.Parse(Console.ReadLine());
                Developer newDeveloper = new Developer();

                Console.WriteLine("Enter the First Name of the Developer:");
                newDeveloper.FirstName = Console.ReadLine();

                Console.WriteLine("Enter the Last Name of the Developer:");
                newDeveloper.LastName = Console.ReadLine();

                Console.WriteLine("Does this Developer have access to Pluralsight? (yes/no)");
                string accessAsString = Console.ReadLine().ToLower();

                if (accessAsString == "yes")
                {
                    newDeveloper.HasPluralsightAccess = true;
                }
                else
                {
                    newDeveloper.HasPluralsightAccess = false;
                }

                bool wasUpdated = _developerRepo.UpdateExistingDeveloper(oldDevIdNumber, newDeveloper);
                if (wasUpdated)
                {
                    Console.WriteLine("Developer successfully updated.");
                }
                else
                {
                    Console.WriteLine("Could not update developer.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
            }   
        }
       
        //Update Dev Team
        private void UpdateExistingTeams()
        {
            DisplayListofDevTeams();
            Console.WriteLine("Enter the ID Number of the Team you would like to update:");

            int oldDevTeamIdNumber;
            try
            {
                oldDevTeamIdNumber = int.Parse(Console.ReadLine());

                DevTeam newDevTeam = new DevTeam();

                Console.WriteLine("Enter the Team Name:");
                newDevTeam.TeamName = Console.ReadLine();

                newDevTeam.ListOfDevelopersOnTeam = AddDevelopersToDevTeam();

                bool wasUpdated = _devTeamRepo.UpdateExistingTeams(oldDevTeamIdNumber, newDevTeam);
                if (wasUpdated)
                {
                    Console.WriteLine("The Dev Team was successfully updated.");
                }
                else
                {
                    Console.WriteLine("Could not update the Dev Team.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
            }
            
        }

        //Delete One Developer
        private void RemoveDeveloperFromList()
        {
            DisplayDeveloperList();
            Console.WriteLine("\n Enter the ID Number of the Developer you would like to remove:");
            int devIdNumber;
            try
            {
                devIdNumber = int.Parse(Console.ReadLine());

                bool wasDeleted = _developerRepo.RemoveDeveloperFromList(devIdNumber);

                if (wasDeleted)
                {
                    Console.WriteLine("The Developer was successfully removed from the system.");
                }
                else
                {
                    Console.WriteLine("The Developer could not be removed from the system.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
            }
           
        }

        //Delete a Whole Dev Team
        private void RemoveTeamFromList()
        {
            DisplayListofDevTeams();
            Console.WriteLine("\nEnter the Team ID Number that you would like to remove:");
            int devTeamIdNumber;

            try
            {
                devTeamIdNumber = int.Parse(Console.ReadLine());
                bool wasDeleted = _devTeamRepo.RemoveTeamFromList(devTeamIdNumber);

                if (wasDeleted)
                {
                    Console.WriteLine("The Development Team was successfully removed from the system.");
                }
                else
                {
                    Console.WriteLine("The Development Team could not be deleted from the system.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid selection.");
            }
            
        }

        //See List of Employees Who Need a Pluralsight License
        private void DisplayDevelopersWhoNeedPluralsight()
        {
            Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetDeveloperList();
            Console.WriteLine("The following Developers do NOT have access to Pluralsight:\n ");
            foreach (Developer developer in listOfDevelopers)
            {
                if (developer.HasPluralsightAccess == false)
                {
                    
                    Console.WriteLine(($"Name: {developer.FirstName}{" "}{developer.LastName}\n" +
                    $"ID Number:{developer.DevIdNumber}\n"));
                }
               
            }
            

        }

        // Method for displaying yes/no instead of true/false
        public string ReturnYesOrNo(bool value)
        {
            if (value == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        //Seed Method for Individual 
        private void SeedDeveloperList()
        {
            Developer exampleDeveloperOne = new Developer("Smith", "James ", true);
            Developer exampleDeveloperTwo = new Developer("Jones", "Alice ", false);
            Developer exampleDeveloperThree = new Developer("Gonzales", "Roberto ", true);
            Developer exampleDeveloperFour = new Developer("Graham", "William ", false);
            Developer exampleDeveloperFive = new Developer("Martinez", "Jose", true);

            _developerRepo.AddDeveloperToList(exampleDeveloperOne);
            _developerRepo.AddDeveloperToList(exampleDeveloperTwo);
            _developerRepo.AddDeveloperToList(exampleDeveloperThree);
            _developerRepo.AddDeveloperToList(exampleDeveloperFour);
            _developerRepo.AddDeveloperToList(exampleDeveloperFive);

            DevTeam exampleDevTeamOne = new DevTeam(new List<Developer>(),"Software Team A");
            DevTeam exampleDevTeamTwo = new DevTeam(new List<Developer>(),"Software Team B");

            exampleDevTeamOne.AddDeveloperToTeam(exampleDeveloperOne, exampleDevTeamOne);
            exampleDevTeamOne.AddDeveloperToTeam(exampleDeveloperTwo, exampleDevTeamOne);
            exampleDevTeamTwo.AddDeveloperToTeam(exampleDeveloperThree, exampleDevTeamTwo);
            exampleDevTeamTwo.AddDeveloperToTeam(exampleDeveloperFour, exampleDevTeamTwo);
            
            
        }

    }
}
