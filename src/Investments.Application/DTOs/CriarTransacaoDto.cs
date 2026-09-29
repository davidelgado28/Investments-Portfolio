namespace Investments.Application.DTOs;

using Investments.Domain.Enums;

public record CriarTransacaoDto(
    string Ativo,
    decimal Quantidade,
    decimal PrecoCompra,
    DateTime DataTransacao,
    TipoTransacao Tipo,
    Guid UsuarioId
);
