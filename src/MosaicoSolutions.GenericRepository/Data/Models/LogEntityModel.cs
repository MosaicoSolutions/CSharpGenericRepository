using MosaicoSolutions.GenericRepository.Data.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace MosaicoSolutions.GenericRepository.Data.Models
{
    public class LogEntityModel
    {
        public long LogEntityId { get; }
        public string EntityName { get; }
        public string EntityFullName { get; }
        public string EntityAssembly { get; }
        public LogActionType LogActionType { get; }
        public string OriginalValuesAsString { get; }
        public string ChangedValuesAsString { get; }
        public DateTime CreatedAt { get; }
        public string TransactionId { get; set; }
        public Type EntityType { get; }
        public object OriginalValues { get; set; }
        public ModifiedEntityProperty[] ModifiedEntityProperties { get; }

        public LogEntityModel(LogEntity logEntity)
        {
            LogEntityId = logEntity.LogEntityId;
            EntityName = logEntity.EntityName;
            EntityFullName = logEntity.EntityFullName;
            EntityAssembly = logEntity.EntityAssembly;
            LogActionType = logEntity.LogActionType;
            OriginalValuesAsString = logEntity.OriginalValues;
            ChangedValuesAsString = logEntity.ChangedValues;
            CreatedAt = logEntity.CreatedAt;
            TransactionId = logEntity.TransactionId;
            EntityType = Assembly.Load(logEntity.EntityAssembly).GetType(logEntity.EntityFullName);

            OriginalValues = JsonConvert.DeserializeObject(logEntity.OriginalValues, EntityType);

            ModifiedEntityProperties = string.IsNullOrEmpty(logEntity.ChangedValues)
                                        ? new ModifiedEntityProperty[] { }
                                        : JsonConvert.DeserializeObject(logEntity.ChangedValues, typeof(ModifiedEntityProperty[])) as ModifiedEntityProperty[];
        }
    }
}
