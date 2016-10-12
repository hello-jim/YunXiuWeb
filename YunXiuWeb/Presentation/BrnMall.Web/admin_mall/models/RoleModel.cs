using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunXiu.Model;

namespace BrnMall.Web.MallAdmin.models
{
    public class RoleModel
    {
        public Role Role { get; set; }

        public List<Permission> Permissions { get; set; }

        public List<Permission> OwnPermissions { get; set; }
    }
}
