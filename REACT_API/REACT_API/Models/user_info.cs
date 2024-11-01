using System.ComponentModel.DataAnnotations;

namespace REACT_API.Models
{
    public record user_info
    {
        public int SEQ_NUM { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLogin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserIp { get; set; }
        public string Rights { get; set; }
    }
}
