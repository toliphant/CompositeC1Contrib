﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Security;

using Composite.Data;
using Composite.Data.Types;

using CompositeC1Contrib.Security.Data.Types;
using CompositeC1Contrib.Security.Web;

namespace CompositeC1Contrib.Security.Security
{
    public static class PermissionsFacade
    {
        public static bool HasAccess(SiteMapNode node)
        {
            var page = PageManager.GetPageById(new Guid(node.Key));

            return HasAccess(page);
        }

        public static bool HasAccess(IPage page)
        {
            if (page.Id.ToString() == SiteMap.RootNode.Key)
            {
                return true;
            }

            using (var data = new DataConnection())
            {
                var permissions = data.Get<IPagePermissions>().SingleOrDefault(p => p.PageId == page.Id);

                return HasAccess(permissions);
            }
        }

        public static bool HasAccess(IMediaFile media)
        {
            using (var data = new DataConnection())
            {
                var folder = data.Get<IMediaFileFolder>().Single(f => f.StoreId == media.StoreId && f.Path == media.FolderPath);
                var folderPermission = data.Get<IMediaFolderPermissions>().SingleOrDefault(f => f.KeyPath == folder.KeyPath);
                if (folderPermission != null)
                {
                    return HasAccess(folderPermission);
                }

                var mediaPermissions = data.Get<IMediaFilePermissions>().SingleOrDefault(m => m.KeyPath == media.KeyPath);

                return HasAccess(mediaPermissions);
            }
        }

        public static bool HasAccess(IDataPermissions permissions)
        {
            if (permissions == null)
            {
                return true;
            }

            var allowedRoles = permissions.AllowedRoles == null ? new string[0] : permissions.AllowedRoles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var deniedRoles = permissions.DeniedRoles == null ? new string[0] : permissions.DeniedRoles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (deniedRoles.Length <= 0 && allowedRoles.Length <= 0)
            {
                return true;
            }

            var principal = Thread.CurrentPrincipal;
            var userRoles = new List<string>();

            if (principal.Identity.IsAuthenticated)
            {
                userRoles.AddRange(Roles.GetRolesForUser());

                userRoles.Add(CompositeC1RoleProvider.AuthenticatedRole);
            }
            else
            {
                userRoles.Add(CompositeC1RoleProvider.AnonymousdRole);
            }

            if (deniedRoles.Intersect(userRoles).Any())
            {
                return false;
            }

            if (!allowedRoles.Intersect(userRoles).Any())
            {
                return false;
            }

            return true;
        }
    }
}