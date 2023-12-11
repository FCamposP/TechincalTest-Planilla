using Planilla.DTO.ConfiguracionGlobal;
using Planilla.DTO;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Http.ExceptionHandling;
using Planilla.DTO.Others;

namespace Back.Utilidades
{
    public class SettingsConverter<T> where T : new()
    {
        public T Value { get; set; }
        public static T ToSettings(Dictionary<string, ConfiguracionGlobalDTO> dictionary)
        {
            //ExceptionHandler exceptionHandler = new ExceptionHandler();
            T response = new T();
            Type objType = typeof(T);
            PropertyInfo[] myPropertyInfo;
            myPropertyInfo = objType.GetProperties();
            foreach (PropertyInfo prop in myPropertyInfo)
            {
                try
                {
                    prop.SetValue(response, dictionary[prop.Name].Valor, null);
                }
                catch (Exception ex)
                {
                    var exDTO = (ExceptionDTO)ex;
                    if (prop?.Name != null)
                    {
                        exDTO.Message += string.Concat(" - ", prop.Name);
                    }

                    //exceptionHandler.SaveException(exDTO);
                }
            }

            return response;
        }


        public static explicit operator SettingsConverter<T>(Dictionary<string, ConfiguracionGlobalDTO> dictionary)
        {
            //ExceptionHandler exceptionHandler = new ExceptionHandler();
            SettingsConverter<T> response = new SettingsConverter<T>();
            T value = new T();
            Type objType = typeof(T);
            PropertyInfo[] myPropertyInfo;
            myPropertyInfo = objType.GetProperties();
            foreach (PropertyInfo prop in myPropertyInfo)
            {
                try
                {
                    prop.SetValue(response, dictionary[prop.Name].Valor, null);
                }
                catch (Exception ex)
                {
                    var exDTO = (ExceptionDTO)ex;
                    if (prop?.Name != null)
                    {
                        exDTO.Message += string.Concat(" - ", prop.Name);
                    }

                    //exceptionHandler.SaveException(exDTO);
                }
            }
            response.Value = value;
            return response;
        }
    }
}
