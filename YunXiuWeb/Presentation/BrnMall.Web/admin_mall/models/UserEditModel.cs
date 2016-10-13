using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrnMall.Web.MallAdmin.models
{
    public class UserEditModel
    {
        public YunXiu.Model.User User { get; set; }

        public List<YunXiu.Model.Permission> Permissions { get; set; }

        public List<YunXiu.Model.Role> Roles { get; set; }
    }
}
