using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsService.Models;

public class Module
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ModuleId { get; set; }
    public string ModuleCode { get; set; }
    public string Name { get; set; }
    public int Credits { get; set; }
}