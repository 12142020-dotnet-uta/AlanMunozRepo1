using System;
using System.Collections.Generic;

namespace HelloWorldDemo.Model
{
    public class RGB_Game
    {
        public List<Match> lstMatchesHistory = new List<Match>();
        

        public List<Match> AllMatchesFromPlayer(Player player)
        {           
            if ( lstMatchesHistory.Count > 0 )//(lstMatchesHistory.Exists(lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName))
            {
                List<Match> lstPlayerHistory = this.lstMatchesHistory.FindAll( lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName ) ;

                return lstPlayerHistory;
            }
            else
            {
                return lstMatchesHistory;
            }
        }

        public int NumberOfWinsFromPlayer( Player player )
        {
            if ( lstMatchesHistory.Count > 0 )//(lstMatchesHistory.Exists(lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName))
            {
                // List of all the player games
                List<Match> lstPlayerHistory = this.lstMatchesHistory.FindAll( lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName ) ;

                int NumberOfWins = 0;

                foreach (Match myMatches in lstPlayerHistory)
                {   
                    foreach (Round myRounds in myMatches.lstRounds)
                    {
                        if (myRounds.WinningPlayer != null)
                        {
                            if ( myRounds.WinningPlayer.ToString() == player.ToString()  )
                               NumberOfWins++; 
                        }
                        // if( myRounds.WinningPlayer.ToString() == player.ToString() )
                        // {
                        //     NumberOfWins++;
                        // }
                    }
                }

                return NumberOfWins;
            }
            else
            {
                // return lstMatchesHistory;
                return 0;
            }
        }

        public int NumberOfLossesFromPlayer( Player player )
        {
            if ( lstMatchesHistory.Count > 0 )//(lstMatchesHistory.Exists(lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName))
            {
                // List of all the player games
                List<Match> lstPlayerHistory = this.lstMatchesHistory.FindAll( lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName ) ;

                int NumberOfLosses = 0;

                foreach (Match myMatches in lstPlayerHistory)
                {   
                    foreach (Round myRounds in myMatches.lstRounds)
                    {
                        if (myRounds.WinningPlayer != null)
                        {
                            if ( myRounds.WinningPlayer.ToString() != player.ToString()  )
                               NumberOfLosses++; 
                        }
                    }
                }
                return NumberOfLosses;
            }
            else
            {
                return 0;
            }

        }

        public int NumberOfTiesFromPlayer( Player player )
        {
            if ( lstMatchesHistory.Count > 0 )//(lstMatchesHistory.Exists(lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName))
            {
                // List of all the player games
                List<Match> lstPlayerHistory = this.lstMatchesHistory.FindAll( lexpr => lexpr.Player1.FirstName == player.FirstName && lexpr.Player1.LastName == player.LastName ) ;

                int NumberOfTies = 0;

                foreach (Match myMatches in lstPlayerHistory)
                {   
                    foreach (Round myRounds in myMatches.lstRounds)
                    {
                        if (myRounds.WinningPlayer == null)
                        {
                            NumberOfTies++; 
                        }
                    }
                }
                return NumberOfTies;
            }
            else
            {
                return 0;
            }

        }
            

        
    }

    
}
