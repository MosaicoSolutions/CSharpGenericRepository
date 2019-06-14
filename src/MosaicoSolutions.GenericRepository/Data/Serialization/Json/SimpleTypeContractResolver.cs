using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MosaicoSolutions.GenericRepository.Data.Serialization.Json
{
    public class SimpleTypeContractResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type objectType)
        {
            var serializableMenbers = base.GetSerializableMembers(objectType);
            var serializableMenbersFiltered = serializableMenbers.OfType<PropertyInfo>()
                                                                 .Cast<PropertyInfo>()
                                                                 .Where(p => IsSimpleType(p.PropertyType))
                                                                 .Cast<MemberInfo>()
                                                                 .ToList();

            return serializableMenbersFiltered;
        }

        private bool IsSimpleType(Type type)
            => type.IsPrimitive ||
                new[]
                {
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                type.IsEnum ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                (type.IsGenericType &&
                 type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                 IsSimpleType(type.GetGenericArguments().FirstOrDefault()));
    }
}
