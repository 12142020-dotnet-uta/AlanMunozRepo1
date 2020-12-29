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

    }
}