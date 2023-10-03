namespace Contabilizacao.Entities;

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public float Measurement { get; set; }
    public string? Brand { get; set; }
    public int Amount { get; set; }
    public Unit Unit { get; set;}
}

public enum Unit
{
    Kg = 0,
    G = 1,
    L = 2,
    Ml = 3,
    Un = 4
}