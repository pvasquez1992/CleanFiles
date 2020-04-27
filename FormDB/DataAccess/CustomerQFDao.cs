using FormDB.Dto;
using FormDB.Repository;
using System.Linq;

namespace FormDB.DataAccess
{
    public class CustomerQFDao
    {
        private readonly SqlRepository<CustomerDto> repository;

        public CustomerQFDao()
        {
            repository = new SqlRepository<CustomerDto>();
        }

        public CustomerDto GetCustomer(int id)
        {

            var result = repository.Get(x => x.CustomerId.Equals(id)).FirstOrDefault();

            return result;
        }

    }
}
