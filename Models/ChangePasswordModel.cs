namespace oop_CA.Models
{
    public class ChangePasswordModel
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmedNewPassword { get; set; }
    }
}
