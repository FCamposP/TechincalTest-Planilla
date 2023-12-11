using Planilla.DTO.ConfiguracionGlobal;
using System.Collections.Generic;

namespace Back.Utilidades
{
    public static class DictionaryExtension
    {
        public static T ToUserDefinedSettings<T>(this Dictionary<string, ConfiguracionGlobalDTO> dictionary) where T : new()
        {
            return SettingsConverter<T>.ToSettings(dictionary);
        }
    }
}
