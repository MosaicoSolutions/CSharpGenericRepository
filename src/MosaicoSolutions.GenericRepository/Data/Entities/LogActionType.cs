using System;

namespace MosaicoSolutions.GenericRepository.Data.Entities
{
    [Flags]
    public enum LogActionType
    {
        Insert = 1,
        Update = 2,
        Delete = 4
    }
}