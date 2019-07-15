using System;
using MosaicoSolutions.GenericRepository.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Annotations
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class EntityLogAttribute : Attribute
    {
        public bool IgnoreEntity { get; set; }
        public LogActionType LogActionType { get; set; } = LogActionType.Insert | LogActionType.Update | LogActionType.Delete;
    }
}