namespace Shared.dtos.users
{
    public class UserSimpleDTO
    {
        public UserSimpleDTO() { }

        public UserSimpleDTO(int id, string login)
        {
            Id = id;
            Login = login;
        }

        public int Id { get; set; }

        public string Login { get; set; }
    }
}
