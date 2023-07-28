
namespace Model
{
  public static class TabaccoStatus
  {
    public const int NOT_ACTIVE = 0;
    public const int ACTIVE = 1;
  }

  public class Tabacco
  {
    public int? TabaccoId { set; get; }
    public string? TabaccoName { set; get; }
    public decimal TabaccoPrice { set; get; }
    public decimal? Amount { set; get; }
    public int? Pack { set; get; }
    public DateTime TabaccoDate { set;get;}
  }
}
