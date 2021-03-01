using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleManagement
{
    public class Person
    {
        public Person()
        {

        }

        public Person( string name, byte[] surname, int gender, string email, string phoneNumber, DateTime dof)
        {
            //ID = personID;
            Name = name;
            Surname = surname;
            Gender = gender;
            Email = email;
            PhoneNumber = phoneNumber;
            DateOfBirth = dof;
        }
        [Column("PersonId")]
        [Key]
        public long ID { get; set; }
        [Column("Firstname")]
        public string Name { get; set; }
        [Column("Surname")]
        public byte[] Surname { get; set; }
        [Column("Gender")]
        public int Gender { get; set; } // Unknown=0, male=1, female=2 //https://datadictionary.nhs.uk/attributes/person_gender_code.html
        [Column("EmailAddress")]
        public string Email { get; set; }
        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Column("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
    }
}
