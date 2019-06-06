using System;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class RowVersionAttribute : Attribute
    {
    }
}