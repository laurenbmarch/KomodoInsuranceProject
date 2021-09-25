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
                    "\t6. View Developers Who Need Pluralsight Access\n\n" +
                    
                    "Development Teams:\n" +
                    "\t7. Create New Development Team\n" +
                    "\t8. View All Development Teams\n" +
                    "\t9. Find Development Team By Team ID Number\n" +
                    "\t10. Update Existing Development Team\n"+   
                    "\t11. Delete Existing Development Team\n\n" +
                    
                    "12. Exit");

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
                        //Display list of those who need Pluralsight
                        DisplayDevelopersWhoNeedPluralsight();
                        break;
                    case "7":
                        //Create New Development Team
                        AddTeamToList();
                        break;
                    case "8":
                        //View All Deveopment Teams by Team ID Number
                        DisplayListofDevTeams();
                        break;
                    case "9":
                        //Find Development Team By ID Number
                        DisplayTeamByTeamIdNumber();
                        break;
                    
                    case "10":
                        //Update Existing Development Team
                        UpdateExistingTeams();
                            break;
                    
                    case "11":
                        //Delete Existing Development Team
                        RemoveTeamFromList();
                        break;
                    
                    case "12":
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

        //Show List of All Developers
        private void DisplayDeveloperList()
        {
            Console.Clear();
            List<Developer> listOfDevelopers = _developerRepo.GetDeveloperList();

            foreach (Developer developer in listOfDevelopers)
            {
                Console.WriteLine($"Name: {developer.FirstName}{" "}{developer.LastName}\n" +
                    $"ID Number: {developer.DevIdNumber}\n" +
                    $"Access to Pluralsight: {developer.HasPluralsightAccess}\n");
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
                    $"Developers on the Team: {developmentTeam.ListOfDevelopersOnTeam}");  //Question: How do I access this? This is not right.
            }
        }

        //Show One Developer
        private void DisplayDeveloperByIdNumber()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID Number of the Developer you would like to view:");

            int idNumber = int.Parse(Console.ReadLine()); //Question: I need an option for if the user does not enter an integer

            Developer developer = _developerRepo.GetDeveloperByIdNumber(idNumber);

            if(developer != null)
            {
                Console.WriteLine($"Name: {developer.FirstName}{" "}{developer.LastName}\n" +
                    $"ID Number: {developer.DevIdNumber}\n" +
                    $"Pluralsight Access: {developer.HasPluralsightAccess}");
            }
            else
            {
                Console.WriteLine("There is no Developer in the database with that ID Number.");
            }
        }

        //Show One Dev Team
        private void DisplayTeamByTeamIdNumber()
        {
            Console.Clear();
            Console.WriteLine("Enter the ID Number of the Development Team you would like to access:");

            int teamIdNumber = int.Parse(Console.ReadLine()); //Question: I need to have an option if user does not enter an integer
            DevTeam devTeam = _devTeamRepo.GetTeamByTeamIdNumber(teamIdNumber);

            if (devTeam != null)
            {
                Console.WriteLine($"Team Name: {devTeam.TeamName}\n" +
                    $"Team ID Number: {devTeam.TeamId}\n" +
                    $"List of Developers on the team: {devTeam.ListOfDevelopersOnTeam}"); 
            }
            else
            {
                Console.WriteLine("There is no Development Team with that ID Number.");
            }
        }

        //Update Individual Developer
        private void UpdateExistingDeveloper()
        {
            DisplayDeveloperList();
            Console.WriteLine("Enter the ID Number of the Developer you would like to update:");

            int oldDevIdNumber = int.Parse(Console.ReadLine());

            Developer newDeveloper = new Developer();

            Console.WriteLine("Enter the First Name of the Developer:");
            newDeveloper.FirstName = Console.ReadLine();

            Console.WriteLine("Enter the Last Name of the Developer:");
            newDeveloper.LastName = Console.ReadLine();

            //Console.WriteLine("Enter the ID Number of the Developer:");
            //newDeveloper.DevIdNumber = int.Parse(Console.ReadLine());

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
       
        //Update Dev Team
        private void UpdateExistingTeams()
        {
            DisplayListofDevTeams();
            Console.WriteLine("Enter the ID Number of the Team you would like to update:");

            int oldDevTeamIdNumber = int.Parse(Console.ReadLine());

            DevTeam newDevTeam = new DevTeam();

            Console.WriteLine("Enter the Team Name:");
            newDevTeam.TeamName = Console.ReadLine();

            Console.WriteLine("Enter the Team ID Number:");
            newDevTeam.TeamId = int.Parse(Console.ReadLine());

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

        //Delete One Developer
        private void RemoveDeveloperFromList()
        {
            DisplayDeveloperList();
            Console.WriteLine("\n Enter the ID Number of the Developer you would like to remove:");
            int devIdNumber = int.Parse(Console.ReadLine()); //Question: I need an option for if the user does not enter an integer

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

        //Delete a Whole Dev Team
        private void RemoveTeamFromList()
        {
            DisplayListofDevTeams();
            Console.WriteLine("\n Enter the Team ID Number that you would like to remove:");
            int devTeamIdNumber = int.Parse(Console.ReadLine()); //Question: I need an option for if the user does not enter an integer

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

        //Seed Method for Individual 
        private void SeedDeveloperList()
        {
            Developer exampleDeveloperOne = new Developer("Smith", "James ", true);
            Developer exampleDeveloperTwo = new Developer("Jones", "Alice ", false);
            Developer exampleDeveloperThree = new Developer("Gonzales", "Roberto ", true);
            Developer exampleDeveloperFour = new Developer("Graham", "William ", false);

            _developerRepo.AddDeveloperToList(exampleDeveloperOne);
            _developerRepo.AddDeveloperToList(exampleDeveloperTwo);
            _developerRepo.AddDeveloperToList(exampleDeveloperThree);
            _developerRepo.AddDeveloperToList(exampleDeveloperFour);

            /*DevTeam exampleDevTeamOne = new DevTeam(exampleDeveloperOne,"Software Engineering Team A");*/ // Question: I don't know how to do this
        }

    }
}
