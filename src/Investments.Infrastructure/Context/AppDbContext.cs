namespace Investments.Infrastructure.Context;

using Microsoft.EntityFrameworkCore;
using Investments.Domain.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(t => t.Id);
            
            entity.Property(t => t.Ativo)
                  .IsRequired()
                  .HasMaxLength(10);
            
            entity.Property(t => t.Quantidade)
                  .HasPrecision(18, 8);

            entity.Property(t => t.PrecoCompra)
                  .HasPrecision(18, 4);

            entity.HasIndex(t => new { t.UsuarioId, t.Ativo });
        });
    }
}
