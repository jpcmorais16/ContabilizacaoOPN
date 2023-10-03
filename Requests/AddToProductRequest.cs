namespace Contabilizacao.Requests;

public class AddToProductRequest
{
    public string ProductCode { get; set; }
    public int SupermarketId { get; set; }
    public int ShiftId { get; set; }
    public int Amount { get; set; }
    public string AuthorIdn { get; set; }
}