namespace OnlineShopWebApp.Models
{
    public class ChangeRoleViewModel
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public UserViewModel User { get; set; }
        public List<RoleViewModel> Roles { get; set; }

        public ChangeRoleViewModel() { }
        
        public ChangeRoleViewModel(UserViewModel user, List<RoleViewModel> roles)
        {
            User = user;
            Roles = roles;
        }
    }
}
