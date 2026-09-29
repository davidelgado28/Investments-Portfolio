namespace Investments.Api.Endpoints;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Investments.Domain.Entities;
using Investments.Domain.Enums;
using Investments.Application.DTOs;
using Investments.Infrastructure.Context;

public static class TransacaoEndpoints
{
    public static void MapTransacaoEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapPost("/api/transacoes", async (
            [FromBody] CriarTransacaoDto dto,
            [FromServices] AppDbContext context) =>
        {

            var transacao = new Transacao(
                dto.Ativo,
                dto.Quantidade,
                dto.PrecoCompra,
                dto.DataTransacao,
                dto.Tipo,
                dto.UsuarioId
            );

            context.Transacoes.Add(transacao);
            await context.SaveChangesAsync();

            var transacoesAtivo = await context.Transacoes
                .Where(t => t.UsuarioId == dto.UsuarioId && t.Ativo == dto.Ativo.ToUpperInvariant())
                .OrderBy(t => t.DataTransacao)
                .ToListAsync();

            decimal quantidadeTotal = 0;
            decimal custoTotal = 0;

            foreach (var t in transacoesAtivo)
            {
                if (t.Tipo == TipoTransacao.Compra)
                {
                    custoTotal += t.Quantidade * t.PrecoCompra;
                    quantidadeTotal += t.Quantidade;
                }
                else if (t.Tipo == TipoTransacao.Venda)
                {
                    if (quantidadeTotal > 0)
                    {
                        decimal precoMedioAtual = custoTotal / quantidadeTotal;
                        quantidadeTotal -= t.Quantidade;
                        custoTotal = quantidadeTotal * precoMedioAtual; 
                    }
                }
            }
            decimal precoMedioFinal = quantidadeTotal > 0 ? custoTotal / quantidadeTotal : 0;

            return Results.Created($"/api/transacoes/{transacao.Id}", new
            {
                Mensagem = "Transação registrada com sucesso!",
                TransacaoId = transacao.Id,
                Ativo = transacao.Ativo,
                PosicaoAtual = new
                {
                    QuantidadeConsolidada = quantidadeTotal,
                    PrecoMedioPonderado = Math.Round(precoMedioFinal, 4)
                }
            });
        })
        .WithName("CriarTransacao")
        .WithOpenApi();
    }
}
