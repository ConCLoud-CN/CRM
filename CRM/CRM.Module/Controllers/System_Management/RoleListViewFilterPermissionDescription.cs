using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Module.Controllers
{
    public class RoleListViewFilterPermissionDescription
    {
        public RoleListViewFilterPermissionDescription() { }

        public RoleListViewFilterPermissionDescription(string listViewIds, Type targetType, string criteria)
        {
            ListViewIds = listViewIds;
            TargetType = targetType;
            Criteria = criteria;
        }

        public string ListViewIds { get; set; }
        public Type TargetType { get; set; }
        public string Criteria { get; set; }
    }

    public class ListViewFilterPermissionLite
    {
        public Guid RoleId { get; set; }
        public string ListViewId { get; set; }
        public Type TargetType { get; set; }
        public string Criteria { get; set; }

        public ListViewFilterPermissionLite(Guid roleId, string listViewId, Type targetType, string criteria)
        {
            RoleId = roleId;
            ListViewId = listViewId;
            TargetType = targetType;
            Criteria = criteria;
        }
    }
}
