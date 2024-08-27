using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.PermissionManagement;

namespace quanLyDuAnNoiBo.Roles
{
    public class PermissionsManagementAppService : quanLyDuAnNoiBoAppService
    {
        private readonly IPermissionManager _permissionManager;
        private readonly PermissionAppService _permissionAppService;
        public PermissionsManagementAppService(
            IPermissionManager permissionManager,
            PermissionAppService permissionAppService
            )
        {
            _permissionManager = permissionManager;
            _permissionAppService = permissionAppService;
        }

        public async Task<List<PermissionWithGrantedProviders>> GetAllPermissionsByProviderKey(string providerKey)
        {
            var permissions = await _permissionManager.GetAllAsync("R", providerKey);
            var result = permissions.Where(p => !p.Name.StartsWith("Abp")
                         && !p.Name.StartsWith("Setting")
                         && !p.Name.StartsWith("Feature")).ToList();
            return result;
        }

        public async Task<List<PermissionWithGrantedProviders>> GetAllPermissions()
        {
            var permissions = await _permissionManager.GetAllAsync("R", "");
            var result = permissions.Where(p => !p.Name.StartsWith("Abp")
                         && !p.Name.StartsWith("Setting")
                         && !p.Name.StartsWith("Feature")).ToList();
            return result;
        }

        public async Task<bool> CreatePermissionGrant(string roleName, UpdatePermissionsDto permissions)
        {
            if (roleName != string.Empty)
            {
                foreach (var permission in permissions.Permissions)
                {
                    await _permissionManager.SetForRoleAsync(roleName, permission.Name, true);
                }
                return true;
            }

            return false;
        }

        public async Task<bool> UpdatePermissionGrant(string roleName, UpdatePermissionsDto permissions)
        {
            if(roleName != string.Empty)
            {
                await _permissionAppService.UpdateAsync("R", roleName, permissions);
                return true;
            }
            
            return false;
        }

        public async Task<bool> DeleteAsync(string roleName)
        {
            if (roleName != string.Empty)
            {
                await _permissionManager.DeleteAsync("R", roleName);
                return true;
            }

            return false;
        }
    }
}
