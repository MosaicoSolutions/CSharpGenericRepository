using System;


namespace MosaicoSolutions.GenericRepository.Data.Serialization
{
    public class JsonLogSerializeProperties : ILogSerializeProperties
    {
        public Type EntityType => throw new NotImplementedException();

        public object Deserialize(string entity)
        {
            throw new NotImplementedException();
        }

        public string Serialize(object entity)
        {
            throw new NotImplementedException();
        }
    }
}
