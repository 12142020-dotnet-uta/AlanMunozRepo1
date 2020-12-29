using System;
using System.Collections.Generic;

namespace HelloWorldDemo.Model
{
    public class RGB_GameRepositoryLayer
    {
        public List<Match> lstMatchesHistory = new List<Match>();
        public List<Player> Players = new List<Player>();

        public InterfaceOptions myInterfaceOptions = new InterfaceOptions();

/// <summary>
/// It will verify if the player exists in the List of Players and will add it if do not exists.
/// </summary>
/// <param name="player">The object Player that we want to verify if it ex</param>
/// <returns></returns>
        public Player CreatePlayer(string firstName = "null", string lastName = "null")
        {

            if ( !this.Players.Exists( lexpr => lexpr.FirstName == firstName && lexpr.LastName == lastName ) )
            {
                Player player = new Player()
                {
                    FirstName = firstName,
                    LastName = lastName
                };
                this.Players.Add(player);
                return player;
            }

            return null;
        }

/// <summary>
/// This method get all the List of Matches that the player have played throught the game so we have the match information for the player
/// </summary>
/// <param name="player">The object player that we want to get all the matches in the History</param>
/// <returns>Returns the list of Matches the player have been part of</returns>
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

/// <summary>
/// This method get the count of total wins that the player have played throught the history list.
/// </summary>
/// <param name="player">The object player that we want to get all the win matches</param>
/// <returns>Returns the count of victories the player have achieved.</returns>
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

        public void UserSelectionInInterfaceOptions(string strUserInterfaceResponse)
        {
            strUserInterfaceResponse = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strUserInterfaceResponse.ToLower());

            if ( !InterfaceOptions.TryParse(strUserInterfaceResponse, out this.myInterfaceOptions) /*|| !IsValidInterfaceOption(strUserInterfaceResponse)*/ )
            {
                this.myInterfaceOptions = InterfaceOptions.NotValid;
            }


        }


        

        // public bool IsValidInterfaceOption(string IOUser)
        // {
        //     int intNumber;
            
        //     if ( int.TryParse(IOUser.ToString(),out intNumber) )
        //     {
        //         // Console.WriteLine($"It is a number: {intNumber}");

        //         //Validating if its only a 
        //         if(Enum.IsDefined(typeof(GameOptions),  intNumber))
        //             return true;
        //         else
        //             return false;
        //     }
        //     else
        //     {
        //         // 
        //         if(Enum.IsDefined(typeof(GameOptions),  IOUser))
        //             return true;
        //         else
        //             return false;
        //     }
        // }
            

        
    }

    
}
