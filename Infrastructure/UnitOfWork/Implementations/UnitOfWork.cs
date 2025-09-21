using System.Collections;
using LAB5_RodrigoApaza.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AcademicDbContext _context;
    private Hashtable _repositories;

    public UnitOfWork(AcademicDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type))
            return (IGenericRepository<TEntity>)_repositories[type];

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance == null)
            throw new Exception($"No se pudo crear una instancia del repositorio para el tipo {type}");

        _repositories.Add(type, repositoryInstance);
        return (IGenericRepository<TEntity>)repositoryInstance;
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }
}