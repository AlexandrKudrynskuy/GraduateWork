using Domain.Models;

namespace TradeAssistant.Models
{
    public class UserRoleManagerViewModel
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IList<string> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public string[] SelectedRoles { get; set; }
    }
}
