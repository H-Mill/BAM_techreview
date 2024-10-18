using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StargateAPI.Business.Commands;

namespace StargateAPI.Business.Data
{
    [Table("ExceptionLogs")]
    public class ExceptionLogs
    {
        public ExceptionLogs() { }
        public ExceptionLogs(CreateExceptionLog create)
        {
            Timestamp = DateTime.UtcNow;
            Message = create.exception.Message;
            StackTrace = create.exception.StackTrace;
            Source = create.exception.Source;
            InnerException = create.exception.InnerException?.ToString();
            UserId = "This could be something useful";
            AdditionalData = "This could be something useful";
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string Message { get; set; }

        public string? StackTrace { get; set; }

        public string? Source { get; set; }

        public string? InnerException { get; set; }

        public string? UserId { get; set; }

        public string? AdditionalData { get; set; }
    }

    public class ExceptionLogsConfiguration : IEntityTypeConfiguration<ExceptionLogs>
    {
        public void Configure(EntityTypeBuilder<ExceptionLogs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
