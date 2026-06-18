using System.Data;

namespace Shared.dtos.users
{
    public class UserDTO
    {
        public UserDTO() { }

        public UserDTO(int id, string login, int roleId, string fullName) 
        {
            Id = id;
            Login = login;
            RoleId = roleId;
            FullName = fullName;
        }
        
        public int Id { get; set; }

        public string Login { get; set; }

        public int RoleId { get; set; }

        public string FullName { get; set; }
    }
}
