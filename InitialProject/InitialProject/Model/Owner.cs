namespace InitialProject.Model
{
    public class Owner : User
    {
        public bool SuperStatus { get; set; } = false;
        public Owner() { }
        public Owner(string username, string password, Role role, bool superStatus) : base(username, password, role) 
        {
            SuperStatus = superStatus;
        }
    }
}
