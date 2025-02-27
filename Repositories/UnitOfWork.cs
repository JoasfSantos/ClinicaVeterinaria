﻿using ClinicaVet.Data;
using ClinicaVet.Model;

namespace ClinicaVet.Repositories; //Padrão de projeto Unit Of Work.
public interface IUnitOfWork : IDisposable
{
    UsuarioRepository UsuarioRepository { get; }
    AgendamentoRepository AgendamentoRepository { get; }

    Task<int> CommitAsync();
}

public class UnitOfWork : IUnitOfWork
{
    private readonly MyDbContext _context;

    public UsuarioRepository UsuarioRepository { get; private set; }
    public AgendamentoRepository AgendamentoRepository { get; private set; }

    public UnitOfWork()
    {
        _context = new MyDbContext();

        UsuarioRepository = new UsuarioRepository(_context);
        AgendamentoRepository = new AgendamentoRepository(_context);

        _ = UsuarioRepository.Add(new Usuario("Jozz", "admin", "admin", true));
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose() => _context.Dispose();
}