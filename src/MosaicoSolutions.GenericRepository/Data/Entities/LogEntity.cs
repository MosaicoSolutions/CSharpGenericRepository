using System;
using System.ComponentModel.DataAnnotations.Schema;
using MosaicoSolutions.GenericRepository.Annotations;

namespace MosaicoSolutions.GenericRepository.Data.Entities
{
    public class LogEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LogEntityId { get; set; }
        public string EntityName { get; set; }
        public LogActionType LogActionType { get; set; }
        public string OriginalValues { get; set; }
        public string ChangedValues { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TransactionId { get; set; }
    }
}