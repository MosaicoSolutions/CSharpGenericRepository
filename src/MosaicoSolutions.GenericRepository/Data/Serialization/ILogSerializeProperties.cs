using System;

namespace MosaicoSolutions.GenericRepository.Data.Serialization
{
    public interface ILogSerializeProperties
    {
        Type EntityType { get;  }
        string Serialize(object entity);
        object Deserialize(string entity);
    }
}
