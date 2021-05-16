using System.Collections.Generic;

namespace DKRDataManager.Models
{
    public class ApplicationUserModel
    {
        public string Email { get; set; }
        public string Id { get; set; }
        public Dictionary<string, string> Roles { get; set; }
    }
}