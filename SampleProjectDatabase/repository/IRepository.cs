using SampleProjectDatabase.model;

namespace SampleProjectDatabase.repository;

public interface IRepository<TId, TE> where TE : Entity<TId>
{
    TE Save(TE entity);
    bool Delete(TId id);
    TE Update(TE entity);
    List<TE> FindAll();
    TE FindById(TId id);
}