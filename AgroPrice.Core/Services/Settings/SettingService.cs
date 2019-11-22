using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Core.Services.Settings
{
    public class SettingService<T> : ISettingService<T> 
        where T : ISettings, new()
    {
        private readonly T _settings;

        public SettingService(IOptionsMonitor<T> settings)
        {
            _settings = settings.CurrentValue;
        }

        /// <summary>
        /// Load settings.
        /// </summary>
        /// <returns></returns>
        public virtual T LoadSettings()
        {
            return _settings;
        }

    }
}
