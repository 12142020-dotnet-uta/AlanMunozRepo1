using System;
// using HelloWorldDemo.Model;

namespace HelloWorldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            Console.WriteLine("This is the official Batch Rock-Paper-Scissors");

            Options UserChoice = new Options();
            Options ComputerChoice = new Options();
            do
            {
                #region GameMessage
                
                Console.WriteLine("Choose your fate:\n1.-Rock\n2.-Paper\n3.-Scissors");
                Console.Write("Choose wisely: ");
                
                string strUserResponse = Console.ReadLine();
                strUserResponse = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strUserResponse.ToLower());
                
                // Console.WriteLine(Enum.TryParse(strUserResponse, out enumUserChoice ).ToString());
                // Console.WriteLine(IsValidGameOption(strUserResponse).ToString());
                #endregion


                if ( !Enum.TryParse(strUserResponse, out UserChoice ) || !IsValidGameOption(strUserResponse) )
                {
                    Console.WriteLine("Invalid Input, try again.");
                    continue;
                }

                Console.WriteLine($"The user response is: {UserChoice}");
                
                
                Random random = new Random(10);
                ComputerChoice = (Options) random.Next(1,4);

                Console.WriteLine($"Choice of the computer: {ComputerChoice}");


                //expropriation time 
                int intOperation = (int) UserChoice - (int) ComputerChoice;
                if ( intOperation == 1 || intOperation == -2 )
                    Console.WriteLine("Congratulations, you won!");
                else if ( intOperation == 0 )
                    Console.WriteLine("It is a tie!");
                else
                    Console.WriteLine("You have lost, better luck next time!");

                // //My previous version validating...
                // if ( (UserChoice == Options.Rock && ComputerChoice == Options.Scissors ) ||
                //      (UserChoice == Options.Paper && ComputerChoice == Options.Rock ) ||
                //      (UserChoice == Options.Scissors && ComputerChoice == Options.Paper ) )
                // {
                //     //User won
                //     Console.WriteLine("Congratulations, you won!");
                // }
                // else if ( UserChoice == ComputerChoice)
                // {
                //     //Tie
                //     Console.WriteLine("It is a tie!");
                // }
                // else
                // {
                //     //Computer Won
                //     Console.WriteLine("You have lost, better luck next time!");
                // }

                Console.WriteLine("Play again??: Y/N");

                string strPlayAgain = Console.ReadLine();
                if ( strPlayAgain.ToLower().Trim() != "y" )
                {
                    break;
                }
            }while(true);

        }

        public static bool IsValidGameOption(string OptionUser)
        {
            int intNumber;
            
            if ( int.TryParse(OptionUser.ToString(),out intNumber) )
            {
                // Console.WriteLine($"It is a number: {intNumber}");

                //Validating if its only a 
                if(Enum.IsDefined(typeof(Options),  intNumber))
                    return true;
                else
                    return false;
            }
            else
            {
                // 
                if(Enum.IsDefined(typeof(Options),  OptionUser))
                    return true;
                else
                    return false;
            }
        }
        
    }

    public enum Options
    {
        Rock=1,
        Paper=2,
        Scissors=3
    }
}
