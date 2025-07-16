using System.ComponentModel.DataAnnotations;

namespace NHC.Boilerplate.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}