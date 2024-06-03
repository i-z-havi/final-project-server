namespace final_project_server.Models.Users
{
    public class UserPicture
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public byte[] ProfilePicture { get; set; }
    }
}
