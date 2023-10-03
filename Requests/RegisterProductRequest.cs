using Contabilizacao.Entities;

namespace Contabilizacao.Requests;

public class RegisterProductRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public float Measurement { get; set; }
    public float Price { get; set; }
    public string Brand { get; set; }
    public Unit Unit { get; set; }
    public string AuthorIdn { get; set; }
}