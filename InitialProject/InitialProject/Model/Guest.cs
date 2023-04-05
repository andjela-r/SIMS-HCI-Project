namespace InitialProject.Model
{
    public class Guest : User
    {
        public bool SuperStatus { get; set; } = false;
        public Guest() { }
        public Guest(string username, string password, Role role, bool superStatus) : base(username, password, role)
        {
            SuperStatus = superStatus;
        }
    }
}
