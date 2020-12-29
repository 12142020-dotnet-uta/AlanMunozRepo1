using System;
using System.Collections.Generic;

namespace HelloWorldDemo.Model
{
    public class Match
    {
        private Guid matchID = Guid.NewGuid();
        public Guid MatchID { get{ return matchID; } }
        public Player Player1 { get; set; } //User
        public Player Player2 { get; set; } //PC
        public List<Round> lstRounds = new List<Round>(); 
        

        public Player WinnerMatch()
        {
            int intCountP1Wins = countOfWins( Player1 );
            int intCountP2Wins = countOfWins( Player2 );
            

            if (intCountP1Wins == 2)
            {
                return Player1;
            }
            else if (intCountP2Wins == 2)
            {
                return Player2;
            }
            else
            {
                return null;
            }

        }

        public string PartialWinner()
        {
            int intCountP1Wins = countOfWins( Player1 );
            int intCountP2Wins = countOfWins( Player2 );

            if ( intCountP1Wins == intCountP2Wins )
            {
                return "";
            }
            else if (intCountP1Wins > intCountP2Wins)
            {
                return "Player is winning.";
            }
            else
            {
                return "Computer is winning.";
            }

        }

        internal int countOfWins(Player player)
        {
            int intCountP1Wins = lstRounds.FindAll( expr => expr.WinningPlayer == player ).Count;
            return intCountP1Wins;
        }

        public void AsignNewValues()
        {
            matchID = Guid.NewGuid();
            lstRounds.Clear();
        }

        public void AddPlayers( Player player1, Player player2 )
        {
            this.Player1 = player1;
            this.Player2 = player2;
        }

        public GameOptions AsignGameOptionsToPlayer(Player player, string strGameOptions = "")
        {
            strGameOptions = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strGameOptions.ToLower());

            GameOptions playerGameOption = new GameOptions();

            if( this.Player1.ToString() == player.ToString() )
            {
                // User input
                if ( !GameOptions.TryParse(strGameOptions, out playerGameOption ) || !Round.IsValidGameOption(strGameOptions) )
                {   
                    return GameOptions.NotValid;
                }

                return playerGameOption;

            }
            else
            {   
                
                Random random = new Random(50);
                playerGameOption = (GameOptions) random.Next(1,4);
                return playerGameOption;
            }

        }

        public Round BeginRoundGame(Player UserPlayer, string strUserResponse, Player ComputerPlayer )
        {
            Round round = new Round();

            round.PlayerChoice = this.AsignGameOptionsToPlayer(UserPlayer, strUserResponse);
            round.Player2Choice = this.AsignGameOptionsToPlayer(ComputerPlayer);

            this.lstRounds.Add(round);

            return round;
        }

        public bool PlayAgain(string strUserInput)
        {
            if ( strUserInput.ToLower().Trim() != "y" )
            {
                //User input N/n or else.
                return false;
            }
            else
            {
                //Restart a match... same players.
                this.AsignNewValues();
                return true;
            }
        }


    }

}