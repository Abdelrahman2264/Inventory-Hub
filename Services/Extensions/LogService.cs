using InventorySystem.Repository.IRepository;
using InventorySystem.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace InventorySystem.Services.Extensions
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILogger<LogService> _logger;

        public LogService(ILogRepository logRepository, IAppUserRepository appUserRepository, AuthenticationStateProvider authenticationStateProvider, ILogger<LogService> logger)
        {
            _logRepository = logRepository;
            _appUserRepository = appUserRepository;
            _authenticationStateProvider = authenticationStateProvider;
            _logger = logger;
        }

        public async Task CreateLogAsync(int assetId, string changeDescription, string LogType, string DeviceType, int userid = 0)
        {
            try
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                var user = authState.User;
                var appUser = await _appUserRepository.GetAppUserAsync(Convert.ToInt32(user.Identity.Name));

                var log = new LogsHistory
                {
                    AssetId = assetId,
                    ChangeDescription = changeDescription,
                    ChangeDate = DateTime.Now,
                    DeviceType = DeviceType,
                    LogType = LogType,
                    AppUserId = appUser.AppUserId,

                };

                await _logRepository.CreateLog(log);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Create Log Async Service : {ex.Message}", ex);
            }
        }
        public async Task<int> ReturnCurrentUserID()
        {
            try
            {
                var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
                return Convert.ToInt32(authState.User.Identity.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Return Current User ID Service : {ex.Message}", ex);
                return 0;
            }
        }
    }


}
