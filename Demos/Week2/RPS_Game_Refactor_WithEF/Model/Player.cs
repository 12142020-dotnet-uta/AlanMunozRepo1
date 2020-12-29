using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HelloWorldDemo.Model
{
    public class Player
    {   
        public Guid playerID = Guid.NewGuid();
        [Key]
        public Guid PlayerID { get{ return playerID; } set {playerID = value;} }
        private string strFirstName;
        public string FirstName
        {
            get { return strFirstName; }
            set 
            { 
                if ( value is string && !string.IsNullOrEmpty(value) && value.Length < 25 )
                    strFirstName = value; 
                else    
                {
                    throw new Exception($"The last name you sent is invalid: {value}");
                }

            }
        }
        
        private string strLastName;
        public string LastName
        {
            get { return strLastName; }
            set 
            { 
                if ( value is string && !string.IsNullOrEmpty(value) && value.Length < 25 )
                    strLastName = value; 
                else
                {
                    throw new Exception($"The last name you sent is invalid: {value}");
                }

            }
        }
            //
        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }


        // private int intPlayerID;
        // public int PlayerID

        // {
        //     get { return intPlayerID; }
        //     set { intPlayerID = value; }
        // }




        // private int intNumberWins = 0;
        // public string NumberWins
        // {
        //     get { return $"# Wins: {intNumberWins}"; }
        //     // set { intNumberWins = value; }
        // }

        // private int intNumLosses = 0;
        // public string NumLosses
        // {
        //     get { return $"# Losses: {intNumLosses}"; }
        //     // set { intNumLosses = value; }
        // }




        

        // private int intPlayerID;
        // public int PlayerID

        // {
        //     get { return intPlayerID; }
        //     set { intPlayerID = value; }
        // }
        



        // private int intNumberWins = 0;
        // public string NumberWins
        // {
        //     get { return $"# Wins: {intNumberWins}"; }
        //     // set { intNumberWins = value; }
        // }

        // private int intNumLosses = 0;
        // public string NumLosses
        // {
        //     get { return $"# Losses: {intNumLosses}"; }
        //     // set { intNumLosses = value; }
        // }

        
        
        
        // public void AddWins()
        // {
        //     intNumberWins++;
        // }
        // public void AddLoss()
        // {
        //     intNumLosses++;
        // }

    }
}