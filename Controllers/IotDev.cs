using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace IoTProject301.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IotDev: ControllerBase
    {
        static Device device;

        
        static string constring= "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
        static RegistryManager registryManager;
        
        [Route("AddDevice")]

        [HttpPost]
        public async Task<Device> AddDeviceAsync(string Id){
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            return await registryManager.AddDeviceAsync(new Device(Id));
        }
        [Route("RemoveDevice")]

        [HttpDelete]
        public async Task RemoveDeviceAsync(string Id){
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            await registryManager.RemoveDeviceAsync(Id);
        }
        [Route("GetDevice")]

        [HttpGet]
        public async Task<Device> GetDeviceAsync(string Id){
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            device= await registryManager.GetDeviceAsync(Id);
            return device;
        }
        [Route("UpdateDevice")]

        [HttpPut]
        public async Task UpdateDeviceAsync(string Id){
            Device anotherDev;
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            anotherDev= await registryManager.GetDeviceAsync(Id);
            anotherDev.Status=Microsoft.Azure.Devices.DeviceStatus.Enabled;
            await registryManager.UpdateDeviceAsync(anotherDev);
            
        }

     /*    [Route("Addtags")]

        [HttpPut]
        public async Task AddTagsAndQuery()
{
     //Console.WriteLine("Device Id is : 000");
    registryManager = RegistryManager.CreateFromConnectionString(constring);
    var twin = await registryManager.GetTwinAsync("AarthiDevice"); 
    Console.WriteLine("Device Id is : "+ twin.DeviceId);
    var patch =
        @"{
            tags: {
                location: {
                    region: 'US',
                    plant: 'Redmond43'
                }
            }
        }";
    await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);

        } */

    }
}