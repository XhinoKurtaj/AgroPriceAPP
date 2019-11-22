using System;
using System.Collections.Generic;
using System.Text;

namespace AgroPrice.Core.Services.Settings
{
    public interface ISettingService<out T>where T : ISettings, new()
    {
        /// <summary>
        /// Load settings.
        /// </summary>
        /// <returns></returns>
        T LoadSettings();
    }
}
