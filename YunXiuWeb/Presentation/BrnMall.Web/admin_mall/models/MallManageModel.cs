using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrnMall.Web.MallAdmin.models
{
    public class MallManageModel
    {

    }

    public class EditPermissionModel
    {
        public YunXiu.Model.Permission Permission { get; set; }
        public List<YunXiu.Model.PermissionType> TypeList { get; set; }
    }
}
