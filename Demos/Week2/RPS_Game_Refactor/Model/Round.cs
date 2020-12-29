using System;
namespace HelloWorldDemo.Model
{
    public class Round
    {
        private Guid roundID = Guid.NewGuid();
        public Guid RoundID { get { return roundID; }}

        public GameOptions PlayerChoice {get; set;}  //User
        public GameOptions Player2Choice {get; set;}  //PC

        public Player WinningPlayer{get;set;} = null;


        public void AssignWinningPlayer (Player player = null)
        {
            this.WinningPlayer = player;
        }

        public string GetWinningPlayer(Match ActualMatch)
        {
            int intOperation = (int) this.PlayerChoice - (int) this.Player2Choice;
            if ( intOperation == 1 || intOperation == -2 )
            {
                this.AssignWinningPlayer(ActualMatch.Player1);
                return ("\t\t\tCongratulations, you won this round!");
            }
            else if ( intOperation == 0 )
            {
                return ("\t\t\tIt is a tie!");

            }
            else
            {
                this.AssignWinningPlayer(ActualMatch.Player2);
                return ("\t\t\tYou have lost, better luck next time!");
            }
        }

        public string WinnerRoundMessage()
        {
            if (this.WinningPlayer != null)
                return $"The winner is {this.WinningPlayer.ToString()} for this round";
            else
                return "It is a Tie.";
        }


        public static bool IsValidGameOption(string OptionUser)
        {
            int intNumber;
            
            if ( int.TryParse(OptionUser.ToString(),out intNumber) )
            {
                // Console.WriteLine($"It is a number: {intNumber}");

                //Validating if its only a 
                if(GameOptions.IsDefined(typeof(GameOptions),  intNumber))
                    return true;
                else
                    return false;
            }
            else
            {
                // 
                if(GameOptions.IsDefined(typeof(GameOptions),  OptionUser))
                    return true;
                else
                    return false;
            }
        }


    }

}