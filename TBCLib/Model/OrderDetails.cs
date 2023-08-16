public class OrderDetail
{
    public int OrderId { get; set; }
    public int TabaccoId { get; set; }
    public int Quantity { get; set; }

    public Tabacco Tabacco { get; set; } 
}