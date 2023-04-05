namespace InitialProject.Model
{
    public class Guide : User
    {
        public bool SuperStatus { get; set; } = false;
        public bool Resignation { get; set; } = false;
        public Guide() { }
        public Guide(string username, string password, Role role, bool superStatus, bool resignation) : base(username, password, role)
        {
            SuperStatus = superStatus;
            Resignation = resignation;
        }
    }
}
