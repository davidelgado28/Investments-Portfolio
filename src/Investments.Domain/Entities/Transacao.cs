namespace Investments.Domain.Entities;

using Investments.Domain.Enums;

public class Transacao
{
    public Guid Id { get.private set; }
    public string Ativo { get.private set; } = string.Empty;
    public decimal Quantidade { get.private set; }
    public decimal PrecoCompra { get.private set; }
    public DateTime DataTransacao { get.private set; }
    public TipoTransacao Tipo { get.private set; }
    public Guid UsuarioId { get.private set; }

    protected Transacao() { }

    public Transacao(string ativo, decimal quantidade, decimal precoCompra, DateTime dataTransacao, TipoTransacao tipo, Guid usuarioId)
    {
        Id = Guid.NewGuid();
        Ativo = ativo.ToUpperInvariant();
        Quantidade = quantidade;
        PrecoCompra = precoCompra;
        DataTransacao = dataTransacao;
        Tipo = tipo;
        UsuarioId = usuarioId;
    }
}
