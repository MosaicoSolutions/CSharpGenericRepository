using System;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class RowVersionAttribute : Attribute
    {
    }
}