using P1_ModelLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P1_RepositoryLayer;

namespace P1_BuisnessLogicLayer
{
    public class BuisnessLogicClass
    {

        private readonly Repository _repository;


        public BuisnessLogicClass()
        {

        }
        public BuisnessLogicClass(Repository repository)
        {
            _repository = repository;

        }
        public Location GetDefaultLocationForRegisterCustomer()
        {
            Location myLocation = _repository.GetDefaultLocationForRegisterCustomer();

            return myLocation;
        }

    }


}
