using Model;
using DAL;
namespace BL
{
    public class OrderBL
    {
        private OrderDAL odl = new OrderDAL();
        public bool CreateOrder(Order order)
        {
            bool result = odl.CreateOrder(order);
            return result;
        }
    }
}