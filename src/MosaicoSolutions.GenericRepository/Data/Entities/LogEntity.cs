using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MosaicoSolutions.GenericRepository.Data.Entities
{
    public class LogEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LogEntityId { get; set; }
        public string EntityName { get; set; }
        public string EntityFullName { get; set; }
        public string EntityAssembly { get; set; }
        public LogActionType LogActionType { get; set; }
        public string OriginalValues { get; set; }
        public string ChangedValues { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TransactionId { get; set; }
    }
}