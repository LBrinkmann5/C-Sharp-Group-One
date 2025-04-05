namespace Southwest_Airlines.Models
{
    public class UserList
    {
        public List<Registration> Users { get; set; } = new List<Registration>();

        public void AddUser(Registration user)
        {
            Users.Add(user);
        }
    }
}
