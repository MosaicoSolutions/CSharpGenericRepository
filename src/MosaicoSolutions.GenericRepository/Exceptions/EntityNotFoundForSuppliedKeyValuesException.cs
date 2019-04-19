using System;
using System.Runtime.Serialization;

namespace MosaicoSolutions.GenericRepository.Exceptions
{
    public class EntityNotFoundForSuppliedKeyValuesException : Exception
    {
        public object[] KeyValues { get; } = new object[] {};
        public override string Message => "Entity not found for supplied key values!" + KeyValues is null ? string.Empty : $" Key Values [{string.Join(",", KeyValues)}].";

        public EntityNotFoundForSuppliedKeyValuesException(object[] ids) => KeyValues = ids;
    }
}