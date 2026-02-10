using System.ComponentModel.DataAnnotations;

namespace CoffeeTechnik.Models
{
    public class MontageViewModel
    {
        [Required(ErrorMessage = "Моля, въведете име на обекта")]
        public string ObjectName
        { get; set; }


        [Required(ErrorMessage = "Моля, въведете булстат")]
        public string BULSTAT 
        { get; set; }


        [Required(ErrorMessage = "Моля, въведете град")]
        public string City 
        { get; set; }


        [Required(ErrorMessage = "Моля, въведете адрес")]
        public string Address
        { get; set; }


        [Required(ErrorMessage = "Моля, въведете човек за контакт")]
        public string ContactPerson
        { get; set; }


        [Required(ErrorMessage = "Моля, въведете телефон")]
        [Phone(ErrorMessage = "Невалиден телефонен номер")]
        public string Phone 
        { get; set; }


        [Required(ErrorMessage = "Моля, въведете модел на машината")]
        public string MachineModel 
        { get; set; }

        [Required(ErrorMessage = "Моля, свържете машината")]
        public string MachineConnection 
        { get; set; }

        [Required(ErrorMessage = "Моля, въведете локация")]
        public string Location 
        { get; set; }
    }
}
