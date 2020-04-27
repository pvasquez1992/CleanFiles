using FormDB.DataAccess;
using FormDB.Dto;

namespace FormDB.BussinesLogic
{
    public class CustomerQfBol
    {
        private readonly CustomerQFDao customerQfDao;

        public CustomerQfBol()
        {
            customerQfDao = new CustomerQFDao();
        }
        public CustomerDto GetCustomerById(int id)
        {

            return customerQfDao.GetCustomer(id);

        }



    }
}
