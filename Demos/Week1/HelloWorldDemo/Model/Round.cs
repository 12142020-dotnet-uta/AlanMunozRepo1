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

    
        
    }

}