using System;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class EntityLogAttribute : Attribute
    {
        public bool IgnoreEntity { get; set; }

        public EntityLogAttribute(bool ignoreEntity = false) => IgnoreEntity = ignoreEntity;
    }
}