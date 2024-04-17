using System.Transactions;
using Microsoft.EntityFrameworkCore;
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
        static async Task Main(string[] args)
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
                Write("Enter the number of your choice: ");
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
                            Keyboard newKeyboard = new Keyboard(size, swType, swAmt, lube); // new keyboard object
                            await context.Keyboards.AddAsync(newKeyboard); // Add the keyboard to database
                            await context.SaveChangesAsync(); // save changes
                        }
                        break;

                    case 2: // User lists the keyboards in the database
                        using (var context = new KeyboardDbContext())
                        {
                            List<Keyboard> keyboards = await context.Keyboards.ToListAsync();
                            foreach (var keyboard in keyboards) // Iterate through each keyboard
                            {
                                WriteLine(keyboard); // List each keyboard
                            }
                        }
                        break;

                    case 3: // User updates a specific keyboard
                        Write("Enter the id of the keyboard you want to update: ");
                        idNum = int.Parse(Console.ReadLine());

                        using (var context = new KeyboardDbContext())
                        {
                            Keyboard updateKeyboard = await context.Keyboards.FindAsync(idNum); // Find the keyboard based on id

                            if (updateKeyboard != null)
                            {
                                // Ask user to reenter keyboard details
                                Write("Enter the size of the keyboard: ");
                                updateKeyboard.Size = Console.ReadLine();
                                Write("Enter the switch type: ");
                                updateKeyboard.SwitchType = Console.ReadLine();
                                Write("enter the switch amount: ");
                                updateKeyboard.SwitchAmount = int.Parse(Console.ReadLine());
                                Write("Enter the lube type: ");
                                updateKeyboard.Lube = Console.ReadLine();

                                await context.SaveChangesAsync(); // Save the changes
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
                            Keyboard deleteKeyboard = await context.Keyboards.FindAsync(idNum);

                            if (deleteKeyboard != null)
                            {
                                context.Keyboards.Remove(deleteKeyboard); // Remove the keyboard
                                await context.SaveChangesAsync(); // Save the changes
                            } else
                            {
                                WriteLine("That keyboard does not exist.");
                            }
                        }

                        break;
                    case 5: // User deletes all keyboards in the database
                        using (var context = new KeyboardDbContext())
                        {
                            List<Keyboard> allKeyboards = await context.Keyboards.ToListAsync();
                            context.Keyboards.RemoveRange(allKeyboards); // Remove all keyboards
                            await context.SaveChangesAsync(); // Save the changes
                        }
                        break;

                    case 6: // User quits program
                        break;

                    default:
                        WriteLine("Invalid option. Please try again.");
                        break;
                }
            } while (optionChoice != 6); // Entering 6 exits the program
        }
    }
}
