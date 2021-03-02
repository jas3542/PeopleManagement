using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleManagement.Repositories
{
    public interface IPeopleRepository: IRepository
    {
        Task<PageList<Person>> GetPeople(PageParameter pageParameter);
        Task<Person> GetPersonById(long personId);
        Task<int> AddPerson(Person person);
        Task<int> UpdatePerson(long personId, Person person);
        Task<int> DeletePerson(Person person);

    }
}
