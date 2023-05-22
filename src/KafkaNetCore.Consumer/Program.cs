using Confluent.Kafka;
using KafkaNetCore.Producer.Model;
using System.Text.Json;

var config = new ConsumerConfig
{
  GroupId = "gid-consumers",
  BootstrapServers = "localhost:9092",
  AllowAutoCreateTopics = true,
  AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
{
  consumer.Subscribe("testdata");

  while (true)
  {
    var bookingDetails = consumer.Consume();

    var carDetails = JsonSerializer.Deserialize<CarDetails>(bookingDetails.Message.Value);

    //if (bookingDetails.Offset % 2 == 0)
    //  throw new Exception("Deu ruim.");

    Console.WriteLine($"Message: {carDetails} \n"
                    + $"Partition: {bookingDetails.Partition} \n"
                    + $"Offset: {bookingDetails.Offset} \n"
                    + $"Timestamp: {bookingDetails.Message.Timestamp.UtcDateTime} \n");
  }
}