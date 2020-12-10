using System;
using System.Globalization;
using BirthdayManager.Domain.Entities;
using BirthdayManager.Domain.Services;
using BirthdayManager.Infrastructure.Repositories;

namespace BirthdayManager.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            int operation, option, personOption;
            bool flag = true;
            string query;

            //Classes of service representing CRUD           
            var serviceCreate = new PersonServiceCreateOnFile(new PersonRepositoryFile());
            var serviceRead   = new PersonServiceReadOnFile(new PersonRepositoryFile());
            var serviceUpdate = new PersonServiceUpdateOnFile(new PersonRepositoryFile());
            var serviceDelete = new PersonServiceDeleteOnFile(new PersonRepositoryFile());

            //Other application services
            var servicePrintData  = new PersonServicePrintData();
            var serviceDob        = new PersonServiceDobInfo();
            var serviceSearch     = new PersonServiceSearch();

            //Brazil date pattern instance
            CultureInfo culture   = new CultureInfo("pt-BR");

            //Start application
            while (flag)
            {
                InitialOptions();

                //variable receives input value to iterate between switch
                operation = int.Parse(Console.ReadLine());

                //Treat the program according to the value of the operation variable
                switch (operation)
                {
                    case 1:                        
                        DisplayMessage("The SEARCH FOR PEOPPLE option was selected");
                        Console.WriteLine("Choose one of the options below");
                        Console.WriteLine("1 - Search by Name");
                        Console.WriteLine("2 - Search by Date of Birth");

                        //Variable option stores user input and converts it to int type
                        option = int.Parse(Console.ReadLine());

                        //If the option is equal to 1, the person object is inserted in the database repository and file text
                        if (option == 1)
                        {
                            DisplayMessage("The Search by Name was selected");                            
                            Console.WriteLine("Enter the name or part of the name you want to find:");
                            query = Console.ReadLine().ToUpper();

                            //Variable that stores the search term
                            var personsByName     = serviceRead.ReadOnFile();

                            //Event that performs the name search according to the informed term
                            var queryResultByName = serviceSearch.SearchByName(personsByName, query);

                            //Checks whether the term entered is different or empty
                            //If it is different from 0 it executes the instructions inside the if block
                            if (queryResultByName.Count != 0)
                            {
                                //Variable index scrolls through the list of people and points to the index in personsByName.IndexOf (index)                                                
                                foreach (var i in queryResultByName)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"{queryResultByName.IndexOf(i)} - {servicePrintData.PrintFullName(i)}");
                                }
                                Console.WriteLine();

                                //If the value of the personOption matches the position of the options of the listed person, print their detailed data
                                //Load contact data according to the selected option
                                Console.WriteLine("Select one of the options below to view the data of the people found:");
                                Console.WriteLine();
                                personOption = int.Parse(Console.ReadLine());

                                //Variable index scrolls through the list of people and points to the index in personsByName.IndexOf (index)
                                foreach (var i in queryResultByName)
                                {   
                                    //If the value of the option corresponds to the person's index, he prints the details according
                                    if (personOption == queryResultByName.IndexOf(i))
                                    {
                                        if (i.Dob.Day == DateTime.Today.Day && i.Dob.Month == DateTime.Today.Month)
                                        {
                                            BirthdayMessage();
                                            Console.WriteLine("Person Data:");
                                            Console.WriteLine(servicePrintData.PrintFullData(i));
                                            ClearScreen();
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Person Data:");
                                            Console.WriteLine(servicePrintData.PrintFullData(i));
                                            Console.WriteLine($"{serviceDob.DaysForNextBirthday(i.Dob.Month, i.Dob.Day)} days until this anniversary.");
                                            ClearScreen();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("########### Search has no results ##########");
                                ClearScreen();
                            }
                        }
                        else if (option == 2)
                        {
                            Console.Clear();
                            Console.WriteLine();
                            Console.WriteLine("The Search by Date of Birth was selected");
                            Console.WriteLine();
                            Console.WriteLine("Enter exact date of birth dd/mm/yyyy you want to find:");
                            query = DateTime.Parse(Console.ReadLine()).ToString();

                            //Variable that stores the search term
                            var personsByDob     = serviceRead.ReadOnFile();

                            //Event that performs the dob search according to the informed dob
                            var queryResultByDob = serviceSearch.SearchByDob(personsByDob, DateTime.Parse(query));

                            //Checks whether the term entered is different or empty
                            //If it is different from 0 it executes the instructions inside the if block
                            if (queryResultByDob.Count != 0)
                            {
                                //Variable index scrolls through the list of people and points to the index in personsByName.IndexOf (index)                                                
                                foreach (var i in queryResultByDob)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine($"{queryResultByDob.IndexOf(i)} - {servicePrintData.PrintFullName(i)}");
                                }
                                Console.WriteLine();

                                //If the value of the personOption matches the position of the options of the listed person, print their detailed data
                                //Load contact data according to the selected option
                                Console.WriteLine("Select one of the options below to view the data of the people found:");
                                Console.WriteLine();
                                personOption = int.Parse(Console.ReadLine());

                                //Variable index scrolls through the list of people and points to the index in personsByName.IndexOf (index)
                                foreach (var i in queryResultByDob)
                                {   //If the value of the option corresponds to the person's index, he prints the details according
                                    if (personOption == queryResultByDob.IndexOf(i))
                                    {
                                        if (i.Dob.Day == DateTime.Today.Day && i.Dob.Month == DateTime.Today.Month)
                                        {
                                            BirthdayMessage();
                                            Console.WriteLine("Person Data:");
                                            Console.WriteLine(servicePrintData.PrintFullData(i));                                
                                        }
                                        else
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Person Data:");
                                            Console.WriteLine(servicePrintData.PrintFullData(i));
                                            Console.WriteLine($"{serviceDob.DaysForNextBirthday(i.Dob.Month, i.Dob.Day)} days until this anniversary.");
                                            ClearScreen();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("########### Search has no results ##########");
                                ClearScreen();
                            }
                        }
                        break;

                    //Add person in to the list
                    case 2:
                        DisplayMessage("The ADD PERSON option was selected");
                        
                        //Instantiate the person object
                        Person person = new Person();

                        //Captures the values ​​entered and assigns the object variable
                        person.Id = Guid.NewGuid();
                        Console.WriteLine("Please enter name");
                        person.Name = Console.ReadLine().ToUpper();
                        Console.WriteLine();

                        Console.WriteLine("Please enter last name");
                        person.LastName = Console.ReadLine().ToUpper();
                        Console.WriteLine();

                        Console.WriteLine("Please enter the date of birth in the following format dd/MM/yyyy");
                        person.Dob = DateTime.Parse(Console.ReadLine().ToString(culture));
                        Console.WriteLine();

                        Console.WriteLine("Please enter a e-mail");
                        person.Email = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Please enter a phone number");
                        person.PhoneNumber = Console.ReadLine();
                        Console.WriteLine();

                        //Confirms the data to be entered
                        Console.WriteLine("Is the data below correct?");
                        Console.WriteLine();
                        Console.WriteLine(servicePrintData.PrintFullData(person));
                        Console.WriteLine();

                        Console.WriteLine("1 - Yes");
                        Console.WriteLine("2 - No");
                        Console.WriteLine();

                        //Variable option stores user input and converts it to int type
                        option = int.Parse(Console.ReadLine());

                        //If the option is equal to 1, the person object is inserted in the database repository and file text
                        if (option == 1)
                        {
                            serviceCreate.Register(person);                            
                            Console.WriteLine("Data saved successfully");
                            ClearScreen();
                        }
                        else
                        {
                            Console.WriteLine("Data not saved");
                            ClearScreen();
                        }
                        break;

                    //Edit person in to the list
                    case 3:
                        DisplayMessage("The Edit PERSON option was selected");

                        //Variable that stores the list of names saved in the text file
                        var personsUpdate = serviceRead.ReadOnFile();

                        //Checks whether the list is empty and prints that there is no data
                        if (personsUpdate.Count == 0)
                        {
                            Console.WriteLine("########### There is no recorded data. ##########");
                            ClearScreen();
                        }
                        else
                        {
                            //Variable index scrolls through the list of people and points to the index in personsUpdate
                            foreach (var i in personsUpdate)
                            {
                                Console.WriteLine($"{personsUpdate.IndexOf(i)} - {servicePrintData.PrintFullName(i)}");
                            }
                            Console.WriteLine();

                            //Load contact data according to the selected option
                            Console.WriteLine("Select one of the options below to view the data of the people found:");
                            Console.WriteLine();
                            personOption = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            //Variable index scrolls through the list of people and points to the index in personsUpdate
                            foreach (var i in personsUpdate)
                            {   //If the option value matches the person's index it brings the selected person to confirm the deletion action
                                if (personOption == personsUpdate.IndexOf(i))
                                {
                                    //Confirms the data to be entered
                                    Console.WriteLine("You really want to edit the person?");
                                    Console.WriteLine();
                                    Console.WriteLine(servicePrintData.PrintFullName(i));
                                    Console.WriteLine();

                                    Console.WriteLine("1 - Yes");
                                    Console.WriteLine("2 - No");
                                    Console.WriteLine();

                                    //Variable option stores user input and converts it to int type
                                    option = int.Parse(Console.ReadLine());
                                    Console.WriteLine();

                                    //If the option is equal to 1, the person object is updated in the database repository and file text
                                    if (option == 1)
                                    {
                                        //Instantiate the person object
                                        person = new Person();

                                        //Captures the values ​​entered and assigns the object variable                                        
                                        person.Id = i.Id;
                                        Console.WriteLine();

                                        Console.WriteLine("Please enter name");
                                        person.Name = Console.ReadLine().ToUpper();
                                        Console.WriteLine();

                                        Console.WriteLine("Please enter last name");
                                        person.LastName = Console.ReadLine().ToUpper();
                                        Console.WriteLine();

                                        Console.WriteLine("Please enter the date of birth in the following format dd/MM/yyyy");
                                        person.Dob = DateTime.Parse(Console.ReadLine().ToString(culture));
                                        Console.WriteLine();

                                        Console.WriteLine("Please enter a e-mail");
                                        person.Email = Console.ReadLine();
                                        Console.WriteLine();

                                        Console.WriteLine("Please enter a phone number");
                                        person.PhoneNumber = Console.ReadLine();
                                        Console.WriteLine();

                                        //Confirms the data to be entered
                                        Console.WriteLine("Is the data below correct?");
                                        Console.WriteLine();
                                        Console.WriteLine(servicePrintData.PrintFullData(person));
                                        Console.WriteLine();

                                        Console.WriteLine("1 - Yes");
                                        Console.WriteLine("2 - No");

                                        Console.WriteLine();

                                        //Variable option stores user input and converts it to int type
                                        option = int.Parse(Console.ReadLine());

                                        //If the option is equal to 1, the person object is updated in the database repository and file text
                                        if (option == 1)
                                        {
                                            serviceUpdate.UpdateOnFile(person);
                                            Console.WriteLine("Data updated successfully");
                                            ClearScreen();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data was not updated");
                                            ClearScreen();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Action Canceled");
                                        ClearScreen();
                                    }
                                }
                            }
                        }
                        break;                        

                    //List all persons in to the list
                    case 4:
                        DisplayMessage("The LIST ALL PERSONS option was selected");

                        //Variable that stores the list of names saved in the text file
                        var personsList = serviceRead.ReadOnFile();

                        //Validates if the list is empty
                        if (personsList.Count == 0 )
                        {
                            Console.WriteLine("########### There is no recorded data. ##########");
                            ClearScreen();
                        }
                        else
                        {
                            foreach (var p in personsList)
                            {
                                Console.WriteLine(servicePrintData.PrintFullData(p));
                                Console.WriteLine();
                            }
                            ClearScreen();
                        }
                        break;

                    //Delete persons in to the list
                    case 5:
                        DisplayMessage("The DELETE PERSON option was selected");

                        //Variable that stores the list of names saved in the text file
                        var personsDelete = serviceRead.ReadOnFile();

                        //Checks whether the list is empty and prints that there is no data
                        if (personsDelete.Count == 0)
                        {
                            Console.WriteLine("########### There is no recorded data. ##########");
                            ClearScreen();
                        }
                        else
                        {
                            //Variable index scrolls through the list of people and points to the index in personsDelete
                            foreach (var i in personsDelete)
                            {
                                Console.WriteLine($"{personsDelete.IndexOf(i)} - {servicePrintData.PrintFullName(i)}");
                            }
                            Console.WriteLine();

                            //Load contact data according to the selected option
                            Console.WriteLine("Select one of the options below to view the data of the people found:");
                            Console.WriteLine();
                            personOption = int.Parse(Console.ReadLine());
                            Console.WriteLine();

                            //Variable index scrolls through the list of people and points to the index in personsDelete
                            foreach (var i in personsDelete)
                            {   //If the option value matches the person's index it brings the selected person to confirm the deletion action
                                if (personOption == personsDelete.IndexOf(i))
                                {
                                    //Confirms the data to be entered
                                    Console.WriteLine("You really want to delete the person?");
                                    Console.WriteLine();
                                    Console.WriteLine(servicePrintData.PrintFullName(i));
                                    Console.WriteLine();

                                    Console.WriteLine("1 - Yes");
                                    Console.WriteLine("2 - No");
                                    Console.WriteLine();

                                    //Variable option stores user input and converts it to int type
                                    option = int.Parse(Console.ReadLine());
                                    Console.WriteLine();

                                    //If the option is equal to 1, the person object is deleted in the database repository and file text
                                    if (option == 1)
                                    {
                                        serviceDelete.DeleteOnFile(i.Id);
                                        Console.WriteLine("Person deleted successfully");
                                        ClearScreen();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Action Canceled");
                                        ClearScreen();
                                    }
                                }
                            }
                        }
                        break;

                    //List Birthday people of the day
                    case 6:
                        DisplayMessage("The list birthdays of the day option was selected");

                        //Variable that stores the list of names saved in the text file
                        var personsBirthdayList = serviceRead.ReadOnFile();

                        //If there is a birthday boy on the day, return a list with these people
                        if (personsBirthdayList.Count == 0)
                        {
                            Console.WriteLine("There are no birthdays today");
                        }
                        else
                        {
                            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                            Console.WriteLine("########### These are the birthdays of the day ##########");
                            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                            Console.WriteLine();
                        }

                        foreach (var p in personsBirthdayList)
                        {
                            if (personsBirthdayList.Count > 0)
                                serviceDob.DisplayBirthdaysOfTheDay(p);
                        }
                        ClearScreen();
                        break;

                    //Terminates te program if option 7 is checked
                    case 7:
                        DisplayMessage("The EXIT option was selected");
                        Console.WriteLine("Bye!!!");
                        flag = false;
                        break;

                    //Restart the program
                    default:
                        Console.WriteLine("Option does not exist, repeat the operation");
                        break;
                }
            }
        }

        //Private methods accessible only to the program class of the Console App

        /// <summary>
        /// Method to clear the console app screen
        /// </summary>
        private static void ClearScreen()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// shows the chosen option
        /// </summary>
        /// <param name="message"></param>
        private static void DisplayMessage(string message)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
        }

        /// <summary>
        /// Brings the birthday message of the day
        /// </summary>
        private static void BirthdayMessage()
        {
            Console.WriteLine();
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("########### This is a birthday of the day ##########");
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine();
        }

        /// <summary>
        /// Load the initial options for iteration with the application
        /// </summary>
        private static void InitialOptions()
        {
            Console.WriteLine();
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("########### Birthday Manager ##########");
            Console.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine();
            Console.WriteLine("Please select one of the options below:");
            Console.WriteLine();
            Console.WriteLine("1 - Search person");
            Console.WriteLine("2 - Add person");
            Console.WriteLine("3 - Edit person");
            Console.WriteLine("4 - List all persons");
            Console.WriteLine("5 - Delete persons");
            Console.WriteLine("6 - List Birthday person of the day");
            Console.WriteLine("7 - Exit");
            Console.WriteLine();
        }
    }
}