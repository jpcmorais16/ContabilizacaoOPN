namespace Contabilizacao.Entities;

public class Event
{
    public int Id { get; set; }
    public string UserIdn { get; set; }
    public string ProductCode { get; set; }
    public int? ShiftId { get; set; }
    public int? SupermarketId { get; set; }
    public EventType Type { get; set; }
    public int? Amount { get; set; }
    public DateTime Time { get; set; }
}

public enum EventType
{
    Addition = 0,
    Registration = 1,
    Edition = 2
}