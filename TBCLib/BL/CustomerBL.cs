using Model;
using DAL;

namespace BL
{
    public class CustomerBL
    {
        private CustomerDAL cdal = new CustomerDAL();
        public Customer? GetById(int customerId)
        {
            Customer? cus = new Customer();
            cus = cdal.GetById(customerId);
            if (cus != null)
                return cus;
            else return null;
        }

        public int AddCustomer(Customer customer)
        {
            return cdal.AddCustomer(customer);
        }
    }
}