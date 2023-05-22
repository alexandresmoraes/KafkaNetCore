using Confluent.Kafka;
using KafkaNetCore.Producer.Model;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var producerConfiguration = new ProducerConfig();
builder.Configuration.Bind("producerconfiguration", producerConfiguration);

builder.Services.AddSingleton(producerConfiguration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/cars/sendBookingDetails", async (CarDetails employee, IConfiguration config, ProducerConfig producerConfig) =>
{
  string serializedData = JsonSerializer.Serialize(employee);

  var topic = config.GetSection("TopicName").Value;

  using (var producer = new ProducerBuilder<Null, string>(producerConfig).Build())
  {
    await producer.ProduceAsync(topic, new Message<Null, string> { Value = serializedData });
    producer.Flush(TimeSpan.FromSeconds(10));
    return Results.Ok(true);
  }
})
.WithName("GetBookingDetails");

app.Run();