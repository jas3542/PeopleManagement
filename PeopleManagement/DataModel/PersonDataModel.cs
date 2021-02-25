using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleManagement.DataModel
{
    public class PersonDataModel
    {
        public PersonDataModel() { }

        public PersonDataModel(long personID, string name, string surname, int gender, string email, string phoneNumber, DateTime dof)
        {
            ID = personID;
            Name = name;
            Surname = surname;
            SurnameEncoded = null;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dof;
        }

        public PersonDataModel(long personID, string name,byte[] surnameEncoded, int gender, string email, string phoneNumber, DateTime dof)
        {
            ID = personID;
            Name = name;
            Surname = null;
            SurnameEncoded = surnameEncoded;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dof;
        }

        public long ID { get; set; } // we can use Long or int64
        public string Name { get; set; }
        public string Surname {get; set;}
        public byte[] SurnameEncoded { get; set; }
        public int Gender { get; set; } // Unknown=0, male=1, female=2 //https://datadictionary.nhs.uk/attributes/person_gender_code.html
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool verifyData()
        {
            // this.Gender != null 
            if (this.Name != null && (this.Surname != null || this.SurnameEncoded != null) && this.Email != null && this.DateOfBirth != null) { 
                return true;
            }
            return false;
        }
    }
}
