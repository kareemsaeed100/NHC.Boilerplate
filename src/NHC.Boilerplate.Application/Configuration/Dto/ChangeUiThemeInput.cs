using System.ComponentModel.DataAnnotations;

namespace NHC.Boilerplate.Configuration.Dto;

public class ChangeUiThemeInput
{
    [Required]
    [StringLength(32)]
    public string Theme { get; set; }
}
