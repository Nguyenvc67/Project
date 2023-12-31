public class Order
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int SellerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; }


    public Customer Customer { get; set; }

    public List<OrderDetail> OrderDetails { get; set; }
}