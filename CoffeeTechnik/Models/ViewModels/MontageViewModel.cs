using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.ViewModels
{
    public class MontageViewModel
    {
        [Required(ErrorMessage = "Моля, въведете име на обекта")]
        [Display(Name = "Обект")]
        public string ObjectName
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете булстат")]
        [Display(Name = "Булстат")]
        public string BULSTAT 
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете град")]
        [Display(Name = "Град")]
        public string City 
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете адрес")]
        [Display(Name = "Адрес")]
        public string Address
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете човек за контакт")]
        [Display(Name = "Контактно лице")]
        public string ContactPerson 
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете телефон")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        [Display(Name = "Телефон")]
        public string Phone
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете модел на машината")]
        [Display(Name = "Модел на машината")]
        public string MachineModel 
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, свържете машината")]
        [Display(Name = "Свързване")]
        public string MachineConnection
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете локация")]
        [Display(Name = "Локация")]
        public string Location 
        { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля, въведете името на заявителя")]
        [Display(Name = "Заявител")]
        public string Requester 
        { get; set; } = string.Empty;
    }
}