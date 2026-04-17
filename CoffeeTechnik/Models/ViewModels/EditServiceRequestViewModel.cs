namespace CoffeeTechnik.ViewModels
{
    public class EditServiceRequestViewModel
    {
        public int Id
        { get; set; }

        public string Description
        { get; set; } = string.Empty;

        public string RequestType
        { get; set; } = string.Empty;

        public string Requester
        { get; set; } = string.Empty;
    }
}