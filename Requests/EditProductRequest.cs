using Contabilizacao.Entities;

namespace Contabilizacao.Requests;

public class EditProductRequest
{
    public string ProductCode { get; set; }
    public string Name { get; set; }
    public float Measurement { get; set; }
    public string? Brand { get; set; }
    public Unit Unit { get; set; }
    public string AuthorIdn { get; set; }
}