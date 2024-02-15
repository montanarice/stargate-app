using System.ComponentModel.DataAnnotations.Schema;

namespace StargateAPI.Business.Data;

[Table("LogTableEntry")]
public class LogTableEntry
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    
    // TODO: Add Exception property to database
}