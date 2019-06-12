using System;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(System.AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class EntityLogAttribute : Attribute
    {
        public bool IgnoreEntity { get; set; }

        public EntityLogAttribute(bool ignoreEntity = false) => IgnoreEntity = ignoreEntity;
    }
}