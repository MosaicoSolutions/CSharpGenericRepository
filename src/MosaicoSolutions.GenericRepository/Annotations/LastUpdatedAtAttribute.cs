using System;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class LastUpdatedAtAttribute : Attribute
    {
        public bool UseUtc { get; set; }
    }
}