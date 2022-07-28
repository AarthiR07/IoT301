using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Client.Transport;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace IoTProject301.Controllers
{
    [ApiController]
    [Route("controller")]
    public class DeviceProperties: ControllerBase
    {
        Device device;
        static RegistryManager registryManager;
        static string constring = "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
         
        [Route("Addtags")]

        [HttpPut]
        public   async Task AddTagsAndQuery()
        {
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            var twin = await registryManager.GetTwinAsync("AarthiDevice");
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

        }

        [Route("UpdateDesiredProperties")]

        [HttpPut]

       public async Task UpdateDesiredProperties(string deviceName)

        {

        using (var registrymanager = RegistryManager.CreateFromConnectionString(constring))
        {
            var twin = await registrymanager.GetTwinAsync("AarthiDevice");
            twin.Properties.Desired["frequency"] = "5 hz";
            await registrymanager.UpdateTwinAsync(twin.DeviceId, twin, twin.ETag);
        }
        }

         [Route("UpdateReportededProperties")]

        [HttpPut]

        public async Task UpdateReportedPropertiesAsync(string deviceId)

        {

            // Set a reported value for a property the device supports, with the corresponding data type

            try

            {

                Console.WriteLine("Sending connectivity data as reported property");



                string DeviceConnectionString = "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=;deviceid=" + deviceId;



                DeviceClient dc = DeviceClient.CreateFromConnectionString(DeviceConnectionString);

                TwinCollection reportedProperties, connectivity, telemetryConfig;

                reportedProperties = new TwinCollection();

                connectivity = new TwinCollection();

                connectivity["type"] = "cellular";

                reportedProperties["connectivity"] = connectivity;

                telemetryConfig = new TwinCollection();

                telemetryConfig["sendFrequency"] = "5m";

                telemetryConfig["status"] = "success";

                reportedProperties["telemetryConfig"] = telemetryConfig;

                await dc.UpdateReportedPropertiesAsync(reportedProperties);

            }

            catch (Exception ex)

            {

                Console.WriteLine();

                Console.WriteLine("Error in sample: {0}", ex.Message);

            }

        }
    }
}