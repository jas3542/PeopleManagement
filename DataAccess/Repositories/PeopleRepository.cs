using System;
using System.Threading.Tasks;

namespace PeopleManagement.Repositories
{
    public sealed class PeopleRepository : IPeopleRepository
    {
        private DBContext _dbContext;

        public PeopleRepository(DBContext context)
        {
            _dbContext = context;
        }

        public async Task<PageList<Person>> GetPeople(PageParameter pageParameter)
        {
            PageList<Person> people = new PageList<Person>(pageParameter);
            await people.GetData(_dbContext.Person);
            
            return people;
        }

        public Task<Person> GetPersonById(long personId)
        {
            return _dbContext.Person.FindAsync(personId).AsTask();
        }

        public async Task<int> AddPerson(Person person)
        {
            _dbContext.Add(person);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdatePerson(long personId, Person person)
        {
            _dbContext.Update(person);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeletePerson(Person person)
        {
            _dbContext.Remove(person);

            return await _dbContext.SaveChangesAsync();
        }

        public Task<bool> checkDBConnection()
        {
            return _dbContext.Database.CanConnectAsync();
        }

    }
}
