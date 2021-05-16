using System.Collections.Generic;
using System.Linq;

namespace DKRDesktopUI.Library.Models
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public string RoleList => string.Join(", ", Roles.Select(x => x.Value));
        public Dictionary<string, string> Roles { get; set; }
    }
}