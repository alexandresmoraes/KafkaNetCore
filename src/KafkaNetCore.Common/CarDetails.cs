namespace KafkaNetCore.Producer.Model
{
  public class CarDetails
  {
    public int CarId { get; set; }
    public string? CarName { get; set; }
    public string? BookingStatus { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public override string? ToString()
    {
      return $"CarId: {CarId} \n" +
          $"CarName: {CarName} \n" +
          $"BookingStatus: {BookingStatus} \n" +
          $"CreatedAt: {CreatedAt}";
    }
  }
}