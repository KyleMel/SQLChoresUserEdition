using System;

namespace SQLChores
{
    class User
    {
        private string userChoreName;
        private string userAssignName;
        private int userID;
        private int inputString;
        private static string login;

        public User(int input)
        {
            inputString = input;
        }
        public static void Welcome()
        {
            login = "Data Source=localhost;Initial Catalog=master;User ID=Kyle;Password=Temp123!";
            var calls = new DBcalls(login);
            calls.CreateDB();
            calls.CreateTable();
            Console.WriteLine("Please Select What You Would Like To Do:");
            while (true)
            {
                Console.Write(" 1. Add Chore\n 2. Delete Chore\n 3. Update Chore\n 4. List All\n 5. Exit\nEnter: ");
                try
                {
                    int userInput = Convert.ToInt32(Console.ReadLine());
                    var user = new User(userInput);
                    user.UserCases();
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.ToString());
                }
                Console.Write("Back To Main Menu: Y/N\nEnter: ");
                var response = Console.ReadLine();
                Console.Clear();
                if (response == "n") break;
            }
        }
        public void UserCases()
        {
            var calls = new DBcalls(login);
            try
            {
                switch (inputString)
                {
                    case 1:
                        UserAddChore();
                        calls.AddChore(userChoreName, userAssignName);
                        break;
                    case 2:
                        calls.GetChore();
                        UserDeleteChore();
                        calls.DeleteChore(userID);
                        break;
                    case 3:
                        calls.GetChore();
                        UserUpdateChore();
                        calls.UpdateChore(userAssignName, userChoreName);
                        break;
                    case 4:
                        calls.GetChore();
                        break;
                    case 5:
                        break;
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        public void UserAddChore()
        {
            Console.Write("Enter a Chore: ");
            userChoreName = Console.ReadLine();
            Console.Write("Enter who to assign it to: ");
            userAssignName = Console.ReadLine();
        }
        public void UserUpdateChore()
        {
            Console.Write("Select Person to Reassign: ");
            userAssignName = Console.ReadLine();
            Console.Write("Enter New Task: ");
            userChoreName = Console.ReadLine();
        }
        public void UserDeleteChore()
        {
            Console.Write("Select Person ID to Delete: ");
            userID = Convert.ToInt32(Console.ReadLine());
        }
    }
}
