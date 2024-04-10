using System.Transactions;
using static System.Console;

namespace DatabasesHW
{
    internal class Program
    {
        public static void PrintWelcome()
        {
            WriteLine("************************************************");
            WriteLine("                KEYBOARD DATABASE               ");
            WriteLine("************************************************");
            WriteLine();
        }
        public static void PrintHeading()
        {
            WriteLine("1. Add a keyboard");
            WriteLine("2. List keyboards");
            WriteLine("3. Update a keyboard");
            WriteLine("4. Remove a keyboard");
            WriteLine("5. Remove all keyboards");
            WriteLine("6. Exit");
        }
        static void Main(string[] args)
        {
            int optionChoice;
            string size;
            string swType;
            int swAmt;
            string lube;
            int idNum;
            PrintWelcome();

            do // Loop to keep asking which option to select
            {
                WriteLine("What would you like to do?");
                PrintHeading();
                WriteLine("Enter the number of your choice: ");
                optionChoice = int.Parse(Console.ReadLine());

                switch (optionChoice)
                {
                    case 1: // User adds a keyboard to the database
                        Write("Enter the size of the keyboard: ");
                        size = Console.ReadLine();
                        Write("Enter the switch type: ");
                        swType = Console.ReadLine();
                        Write("Enter the switch amount: ");
                        swAmt = int.Parse(Console.ReadLine());
                        Write("Enter the lube type: ");
                        lube = Console.ReadLine();

                        using (KeyboardDbContext context = new KeyboardDbContext())
                        {
                            Keyboard newKeyboard = new Keyboard(size, swType, swAmt, lube);
                            context.Keyboards.Add(newKeyboard);
                            context.SaveChanges();
                        }
                        break;

                    case 2: // User lists the keyboards in the database
                        using (var context = new KeyboardDbContext())
                        {
                            List<Keyboard> keyboards = context.Keyboards.ToList();
                            foreach (var keyboard in keyboards) // Iterate through each keyboard
                            {
                                WriteLine(keyboard); // List each keyboard
                            }
                        }
                        break;

                    case 3: // User updates a specific keyboard
                        Write("Enter the id of the keyboard you want to update: ");
                        idNum = int.Parse(Console.ReadLine());

                        using (var context = new KeyboardDbContext()) // Still need to implement
                        {
                            Keyboard updateKeyboard = context.Keyboards.Find(idNum);

                            if (updateKeyboard != null)
                            {
                                // Ask user to reenter keyboard details
                                Write("Enter the size of the keyboard: ");
                                updateKeyboard.Size = Console.ReadLine();
                                Write("Enter the sqitch type: ");
                                updateKeyboard.SwitchType = Console.ReadLine();
                                Write("enter the switch amount: ");
                                updateKeyboard.SwitchAmount = int.Parse(Console.ReadLine());
                                Write("Enter the lube type: ");
                                updateKeyboard.Lube = Console.ReadLine();

                                context.SaveChanges(); // Save the changes
                            } else
                            {
                                WriteLine("That keyboard does not exist.");
                            }
                        }
                            break;

                    case 4: // User deletes a keyboard in the database
                        Write("Enter the id of the keyboard you want to delete: ");
                        idNum = int.Parse(Console.ReadLine());

                        using (var context = new KeyboardDbContext())
                        {
                            Keyboard deleteKeyboard = context.Keyboards.Find(idNum);

                            if (deleteKeyboard != null)
                            {
                                context.Keyboards.Remove(deleteKeyboard); // Remove the keyboard
                                context.SaveChanges(); // Save the changes
                            } else
                            {
                                WriteLine("That keyboard does not exist.");
                            }
                        }

                        break;
                    case 5: // User deletes all keyboards in the database
                        using (var context = new KeyboardDbContext())
                        {
                            List<Keyboard> allKeyboards = context.Keyboards.ToList();
                            context.Keyboards.RemoveRange(allKeyboards); // Remove all keyboards
                            context.SaveChanges(); // Save the changes
                        }
                        break;

                    case 6: // User quits program
                        break;

                    default:
                        WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (optionChoice != 6); // 6 quits the program
        }
    }
}
