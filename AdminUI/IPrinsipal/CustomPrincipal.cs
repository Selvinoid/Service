using System.Security.Principal;

namespace AdminUI.IPrinsipal
{
    public class CustomPrincipal: GenericPrincipal
    {
        public CustomPrincipal(IIdentity identity, string[] roles)
          : base(identity, roles)
        {
        }

        public string[] Roles { get; set; }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int TeamId { get; set; }

        public string Image { get; set; }
    }
}