using System.ComponentModel.DataAnnotations;

namespace ABM.Users.Dto;

public class ChangeUserLanguageDto
{
    [Required]
    public string LanguageName { get; set; }
}