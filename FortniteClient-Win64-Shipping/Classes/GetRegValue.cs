using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FortniteClient_Win64_Shipping.Classes;

namespace FortniteClient_Win64_Shipping.Classes 
{
    class GetRegValue
    {
        public static string FetchAnyKey(string keyPath)
        {
            try
            {
                string directory = @"FortniteReplica";

                using (RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(directory))
                {
                    if (key != null)
                    {
                        object value = key.GetValue(keyPath);
                        if (value != null)
                        {
                            return value.ToString();
                        }

                        return null;
                    }

                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void SaveAnyKey(string value, string selectedString)
        {
            try
            {
                string registryKey = @"FortniteReplica";

                using (var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(registryKey))
                {
                    if (key != null)
                    {
                        key.SetValue(value, selectedString);
                    }
                    else
                    {
                        Logger.Log("Failed to create registry key.");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log($"Failed to save key. Exception: {ex}");
            }
        }
    }
}
