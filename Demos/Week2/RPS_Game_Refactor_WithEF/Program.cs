using System;
using System.Collections.Generic;
using HelloWorldDemo.Model;

namespace HelloWorldDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is the official Batch Rock-Paper-Scissors");
            
            
            RGB_GameRepositoryLayer GameContext = new RGB_GameRepositoryLayer();
            // List<Player> Players = new List<Player>();

            //CreatePlayer()
            Player ComputerPlayer = GameContext.CreatePlayer("Max","Headroom");

            // Player ComputerPlayer = new Player()
            // {
            //     FirstName = "Max",
            //     LastName = "Headroom"
            // };
            bool ExitMainMenu = false;

            // GameContext.Players.Add(ComputerPlayer);

            //Log in
            do
            {
                #region UserLogIn

                Console.Write("Log in  (Press x to quit game)\nFirst Name: ");            
                string strUserFirstName = Console.ReadLine().Trim().Split(' ')[0];// Trim any white spaces that the user input in the beggining and split to get only the first...
                if (strUserFirstName.ToLower() == "x")
                {
                    Console.WriteLine("Exiting the console...\n");
                    ExitMainMenu = true;
                    break;
                }
                // UserPlayer.FirstName = strUserFirstName;

                // Console.WriteLine(strPlayerName);
                Console.Write("Last Name: ");
                string strUserLastName = Console.ReadLine().Trim().Split(' ')[0];// Trim any white spaces that the user input in the beggining and split to get only the first...
                // UserPlayer.LastName = strUserLastName;

                // //CreatePlayer()
                Player UserPlayer = GameContext.CreatePlayer(strUserFirstName, strUserLastName);

                // if ( !GameSession.Players.Exists( lexpr => lexpr.FirstName == UserPlayer.FirstName && lexpr.LastName == UserPlayer.LastName ) )
                // {
                //     GameSession.Players.Add(UserPlayer);
                // }
                // else is a existing user
                #endregion

                #region PrincipalInterface

                do 
                {
                    Console.WriteLine($"Welcome {UserPlayer.ToString()}"); //, your actual matches played are: {GameSession.AllMatchesFromPlayer(UserPlayer).Count}
                    // InterfaceOptions myInterfaceOptions = new InterfaceOptions();
                    Console.WriteLine("\nSelect one of the following options:\n1.-Play a game (3 rounds)\n2.-Statistics\n3.-LogOut\n4.-Exit\n");

                    string strUserInterfaceResponse = Console.ReadLine();
                    // strUserInterfaceResponse = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strUserInterfaceResponse.ToLower());

                    // // Console.WriteLine($"{Enum.TryParse(strUserInterfaceResponse, out myInterfaceOptions ) }, {IsValidInterfaceOption(strUserInterfaceResponse)}");
                    // if ( !Enum.TryParse(strUserInterfaceResponse, out GameContext.myInterfaceOptions) /*|| !IsValidInterfaceOption(strUserInterfaceResponse)*/ )
                    // {
                    //     Console.WriteLine("Invalid Input, try again.");
                    //     continue;
                    // }

                    GameContext.UserSelectionInInterfaceOptions(strUserInterfaceResponse);


                    if (GameContext.myInterfaceOptions == InterfaceOptions.Play3Rounds)
                    {

                        #region GameInterface
    
                        Match match = new Match();
                        // match.Player1 = UserPlayer;
                        // match.Player2 = ComputerPlayer;
                        match.AddPlayers(UserPlayer, ComputerPlayer);

                        do
                        {
                            
                            #region GameMessage
                            
                            Console.WriteLine($"You are in round #{match.Rounds.Count + 1}, choose your fate:\n\t1.-Rock\n\t2.-Paper\n\t3.-Scissors");
                            Console.Write("Choose wisely: ");
                            
                            string strUserResponse = Console.ReadLine();
                            // strUserResponse = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(strUserResponse.ToLower());
                            
                            // Console.WriteLine(Enum.TryParse(strUserResponse, out enumUserChoice ).ToString());
                            // Console.WriteLine(IsValidGameOption(strUs).ToString());
                            #endregion


                            // if ( !Enum.TryParse(strUserResponse, out UserChoice ) || !round.IsValidGameOption(strUserResponse) )
                            // {   
                            //     Console.WriteLine("Invalid Input, try again.");
                            //     continue;
                            // }
                            // Console.WriteLine($"The user response is: {UserChoice}");
                            // round.PlayerChoice = UserChoice;


                            
                            Round round = match.BeginRoundGame(UserPlayer,strUserResponse,ComputerPlayer);
                            // round.Player2Choice = ComputerChoice;

                            Console.WriteLine($"The user response is: {round.PlayerChoice}, choice of the computer: {round.Player2Choice}");

                            //
                            // int intOperation = (int) round.PlayerChoice - (int) round.Player2Choice;
                            // if ( intOperation == 1 || intOperation == -2 )
                            // {
                            //     Console.WriteLine("\t\t\tCongratulations, you won this round!");
                            //     round.AssignWinningPlayer(match.Player1);
                            // }
                            // else if ( intOperation == 0 )
                            // {
                            //     Console.WriteLine("\t\t\tIt is a tie!");

                            // }
                            // else
                            // {
                            //     Console.WriteLine("\t\t\tYou have lost, better luck next time!");
                            //     round.AssignWinningPlayer(match.Player2);
                            // }
                            Console.WriteLine(round.GetWinningPlayer(match));

                            // match.Player1 = UserPlayer;
                            // match.Player2 = ComputerPlayer;


                            // string strWinnerRound = "";

                            // if (round.WinningPlayer != null)
                            //     strWinnerRound = $"The winner is {round.WinningPlayer.ToString()} for this round";
                            // else
                            //     strWinnerRound = "It is a Tie.";


                            Console.WriteLine($"The result of round #{match.Rounds.Count}:\t{round.WinnerRoundMessage()}");//with ID {round.RoundID}


                            if ( match.WinnerMatch() != null )
                            {
                                Console.WriteLine($"The player {match.WinnerMatch().ToString()} is a winner" );

                                round.AssignWinningPlayer(match.WinnerMatch());
                                
                                GameContext.lstMatchesHistory.Add(match);
                                using(RpsDBContext Db = new RpsDBContext())
                                {
                                    Db.matches.Add(match);
                                    Db.players.Update(match.Player1);
                                    Db.players.Update(match.Player2);
                                    Db.SaveChanges();
                                }


                                Console.WriteLine("\nPlay again??: Y/N");

                                string strPlayAgain = Console.ReadLine();
                                if ( !match.PlayAgain(strPlayAgain) )
                                {
                                    //User input N/n or else.
                                    break;
                                }
                                else
                                {
                                    //Restart a match... same players.
                                    match.AsignNewValues();
                                }
                            }

                            

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

                        }while(true);

                        #endregion
                    }
                    else if (GameContext.myInterfaceOptions == InterfaceOptions.LogOut)
                    {
                        Console.WriteLine("Login Out...\n");
                        break;
                    }
                    else if (GameContext.myInterfaceOptions == InterfaceOptions.Exit)
                    {
                        Console.WriteLine("Exiting the console...\n");
                        ExitMainMenu = true;
                        break;
                    }
                    else if (GameContext.myInterfaceOptions == InterfaceOptions.Statistics)
                    {
                        // ...
                        Console.WriteLine($"\t\tPlayer Name: {UserPlayer.ToString()}\n\t\t# of matches played: {GameContext.AllMatchesFromPlayer(UserPlayer).Count}\n\t\t# of rounds wins: {GameContext.NumberOfWinsFromPlayer(UserPlayer)}\n\t\t# of rounds losses: {GameContext.NumberOfLossesFromPlayer(UserPlayer)}\n\t\t# of ties: {GameContext.NumberOfTiesFromPlayer(UserPlayer)}");

                    }
                    else if (GameContext.myInterfaceOptions == InterfaceOptions.NotValid)
                    {
                        Console.WriteLine("\t\tInvalid input.");
                        continue;
                    }

                }
                while(!ExitMainMenu);


                #endregion


            }while(!ExitMainMenu);

        }
        
    }

}
