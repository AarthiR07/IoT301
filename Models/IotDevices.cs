using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
namespace IoTProject301.Models
{
    public class IotDevices
    {
        static Device device;
        static string constring= "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static RegistryManager registryManager;
        public static async Task<Device> AddDeviceAsync(string Id){
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            return await registryManager.AddDeviceAsync(device);
        }
        public static async Task RemoveDeviceAsync(string Id){
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            await registryManager.RemoveDeviceAsync(device);
        }
        public static async Task<Device> GetDeviceAsync(string Id){
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            device= await registryManager.GetDeviceAsync(Id);
            return device;
        }
        public static async Task UpdateDeviceAsync(string Id){
            Device anotherDev;
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            anotherDev= await registryManager.GetDeviceAsync(Id);
            anotherDev.Status=Microsoft.Azure.Devices.DeviceStatus.Enabled;
            await registryManager.UpdateDeviceAsync(anotherDev);
            
        }




        }
    }
