using System;
using HelloWorldDemo.Model;

namespace HelloWorldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            Console.WriteLine("This is the official Batch Rock-Paper-Scissors");

            Options enumUserChoice = new Options();
            Options enumComputerChoice = new Options();
            do
            {
                #region GameMessage
                
                Console.WriteLine("Choose your fate:\n1.-Rock\n2.-Paper\n3.-Scissors");
                Console.Write("Choose wisely: ");
                
                string strUserResponse = Console.ReadLine();
                strUserResponse = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strUserResponse.ToLower());

                #endregion

                // Console.WriteLine(Enum.TryParse(strUserResponse, out enumUserChoice ).ToString());
                // Console.WriteLine(IsValidGameOption(strUserResponse).ToString());

                if ( !Enum.TryParse(strUserResponse, out enumUserChoice ) || !IsValidGameOption(strUserResponse) )
                {
                    Console.WriteLine("Invalid Input, try again.");
                    continue;
                }

                Console.WriteLine($"The user response is: {enumUserChoice}");
                
                
                Random random = new Random(10);
                enumComputerChoice = (Options) random.Next(1,4);

                Console.WriteLine($"Choice of the computer: {enumComputerChoice}");

                //Enum Values(More legible)
                if ( (enumUserChoice == Options.Rock && enumComputerChoice == Options.Scissors ) ||
                     (enumUserChoice == Options.Paper && enumComputerChoice == Options.Rock ) ||
                     (enumUserChoice == Options.Scissors && enumComputerChoice == Options.Paper ) )
                {
                    //User won
                    Console.WriteLine("Congratulations, you won!");
                }
                else if ( enumUserChoice == enumComputerChoice)
                {
                    //Tie
                    Console.WriteLine("It is a tie!");
                }
                else
                {
                    //Computer Won
                    Console.WriteLine("You have lost, better luck next time!");
                }
                break;



            }while(true);
        }

        public static bool IsValidGameOption(string OptionUser)
        {

            int intNumber;
            
            if ( int.TryParse(OptionUser.ToString(),out intNumber) )
            {
                Console.WriteLine($"It is a number: {intNumber}");
                if(Enum.IsDefined(typeof(Options),  intNumber))
                    return true;
                else
                    return false;
            }
            else
            {
                if(Enum.IsDefined(typeof(Options),  OptionUser))
                    return true;
                else
                    return false;
            }
        }
        
    }
}
