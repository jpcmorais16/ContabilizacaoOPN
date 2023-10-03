namespace Contabilizacao.Entities;

public class ProductToSupermarketToShift
{
    public string ProductCode { get; set; }
    public int SupermarketId { get; set; }
    public int ShiftId { get; set; }
    public int Amount { get; set; }
    public Product? Product { get; set; }
    
    public void Add(int amount)
    {
        Amount += amount;

        Product!.Amount += amount;
    }
}