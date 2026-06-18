using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.users
{
    public class UserRoleUpdateDTO
    {
        public UserRoleUpdateDTO() { }

        public UserRoleUpdateDTO(int roleId)
        {
            RoleId = roleId;
        }

        [Required(ErrorMessage = "RoleId обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "RoleId должен быть > 0")]
        public int RoleId { get; set; }
    }
}
