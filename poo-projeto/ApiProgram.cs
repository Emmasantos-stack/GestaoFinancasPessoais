using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SistemaFinanceiro;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(new PersistenciaJson("data"));
builder.Services.AddSingleton<Sistema>(sp => new Sistema(sp.GetRequiredService<PersistenciaJson>()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }

app.UseStaticFiles();

// Endpoints simples
app.MapGet("/api/categorias", (Sistema s) => s.Categorias);
app.MapPost("/api/categorias", (Sistema s, Categoria c) =>
{
    var created = s.CriarCategoria(c.Nome);
    return Results.Created($"/api/categorias/{created.Id}", created);
});

app.MapGet("/api/transacoes", (Sistema s) => s.Transacoes);
app.MapPost("/api/transacoes", (Sistema s, Transacao t) =>
{
    var created = s.CriarTransacao(t.Descricao, t.Valor, t.Data, t.Tipo, t.CategoriaId);
    return Results.Created($"/api/transacoes/{created.Id}", created);
});

app.MapGet("/api/saldo", (Sistema s) => new { saldo = s.ObterSaldoAtual() });

app.MapGet("/api/utilizadores", (Sistema s) => s.Utilizadores);
app.MapPost("/api/utilizadores", (Sistema s, Utilizador u) =>
{
    var created = s.CriarUtilizador(u.Nome, u.Email, u.Password, u.Perfil);
    return Results.Created($"/api/utilizadores/{created.Id}", created);
});

app.Run();
