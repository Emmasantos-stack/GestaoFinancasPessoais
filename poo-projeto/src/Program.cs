using SistemaFinanceiro.Services;
using SistemaFinanceiro.Models;
using SistemaFinanceiro;

// =============================
// BUILDER
// =============================
var builder = WebApplication.CreateBuilder(args);

// =============================
// CORS
// =============================
builder.Services.AddCors(options =>
{
    options.AddPolicy("frontend", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// =============================
// INJEÇÃO DE DEPENDÊNCIA
// =============================
builder.Services.AddSingleton<Sistema>();

builder.Services.AddScoped<GerirCategoria>();
builder.Services.AddScoped<GerirUtilizador>();
builder.Services.AddScoped<GerirTransacao>();   // ✅ correto
builder.Services.AddScoped<Login>();

var app = builder.Build();

// =============================
// PIPELINE (ORDEM IMPORTA)
// =============================
app.UseCors("frontend");
app.UseStaticFiles();
app.UseRouting(); // ✅ FALTAVA (crítico para APIs)

// =============================
// API - UTILIZADOR
// =============================
app.MapGet("/api/utilizador", (GerirUtilizador s) =>
    Results.Ok(s.ObterTodos())
);

app.MapPost("/api/utilizador", (UtilizadorDto dto, GerirUtilizador s) =>
{
    try
    {
        return Results.Ok(
            s.Criar(dto.Nome, dto.Email, dto.Password, dto.Perfil ?? "user")
        );
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

// =============================
// API - CATEGORIA
// =============================
app.MapGet("/api/categoria", (GerirCategoria s) =>
    Results.Ok(s.ObterTodas())
);

app.MapPost("/api/categoria", (CategoriaDto dto, GerirCategoria s) =>
{
    try
    {
        return Results.Ok(s.Criar(dto.Nome));
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/api/categoria/{id:int}", (int id, GerirCategoria s) =>
    s.Remover(id) ? Results.Ok() : Results.NotFound()
);

// =============================
// API - TRANSACAO ✅
// =============================
app.MapGet("/api/transacao", (GerirTransacao s, GerirCategoria cat) =>
{
    var transacoes = s.ObterTransacao();
    var categorias = cat.ObterTodas();

    var resultado = transacoes.Select(t => new
    {
        t.Id,
        t.Descricao,
        t.Valor,
        t.Data,
        Tipo = t.Tipo.ToString(),
        CategoriaNome = categorias
            .FirstOrDefault(c => c.Id == t.CategoriaId)?.Nome
    });

    return Results.Ok(resultado);
});

app.MapPost("/api/transacao", (TransacaoDto dto, GerirTransacao s) =>
{
    if (!Enum.TryParse<TipoTransacao>(dto.Tipo, true, out var tipo))
        return Results.BadRequest("Tipo de transação inválido.");

    try
    {
        return Results.Ok(
            s.CriarTransacao(
                dto.Descricao,
                dto.Valor,
                dto.Data,
                tipo,
                dto.CategoriaId
            )
        );
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapDelete("/api/transacao/{id:int}", (int id, GerirTransacao s) =>
    s.RemoverTransacao(id) ? Results.Ok() : Results.NotFound()
);

// =============================
// API - LOGIN
// =============================
app.MapPost("/api/login", (LoginDto dto, Login login) =>
{
    if (string.IsNullOrWhiteSpace(dto.Email) ||
        string.IsNullOrWhiteSpace(dto.Password))
        return Results.BadRequest("Dados inválidos.");

    var user = login.Autenticar(dto.Email, dto.Password);

    if (user == null)
        return Results.Unauthorized();

    return Results.Ok(new
    {
        token = Guid.NewGuid().ToString(),
        user = new
        {
            user.Id,
            user.Nome,
            user.Email,
            user.Perfil
        }
    });
});

app.Run();

// =============================
// DTOs
// =============================
record UtilizadorDto(string Nome, string Email, string Password, string? Perfil);
record CategoriaDto(string Nome);

record TransacaoDto(
    string Descricao,
    double Valor,
    DateTime Data,
    string Tipo,
    int? CategoriaId
);

record LoginDto(string Email, string Password);
