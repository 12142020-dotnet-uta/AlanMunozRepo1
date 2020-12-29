namespace P0_CLibrary.Models
{
    public class Customer
    {
        private int intCustomerID;
        public int CustomerID
        {
            get { return intCustomerID; }
            set { intCustomerID = value; }
        }
        private string strFirstName;
        public string FirstName
        {
            get { return strFirstName; }
            set { strFirstName = value; }
        }
        private string strLastName;
        public string LastName
        {
            get { return strLastName; }
            set { strLastName = value; }
        }
        private Location objLocation;
        public Location Location
        {
            get { return objLocation; }
            set { objLocation = value; }
        }

        public override string ToString()
        {
            return $"{strFirstName} {this.strLastName}";
        }

        
    }
}