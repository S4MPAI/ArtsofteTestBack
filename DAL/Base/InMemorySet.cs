using System.Collections;

namespace DAL.Base;

public class InMemorySet<TEntity> : IEnumerable<TEntity> where TEntity : BaseEntity
{
    protected readonly List<TEntity> entities;
    protected int autoIncrement = 1;

    public InMemorySet(List<TEntity> entities)
    {
        this.entities = entities;
    }

    public void Append(TEntity entity)
    {
        entity.Id = autoIncrement++;
        entities.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        var entityInCollection = entities.FirstOrDefault(x => x.Id == entity.Id);

        if (entityInCollection != null)
        {
            entities.Remove(entityInCollection);
        }
    }


    public void Update(TEntity entity)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            if (entities[i].Id == entity.Id)
            {
                entities[i] = entity;
                break;
            }
        }
    }

    public IEnumerator<TEntity> GetEnumerator()
    {
        return entities.GetEnumerator();
    }


    IEnumerator IEnumerable.GetEnumerator()
    {
        return entities.GetEnumerator();
    }
}
