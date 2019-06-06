using System;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class CreatedAtAttribute : Attribute
    {
        public bool UseUtc { get; set; }
    }
}