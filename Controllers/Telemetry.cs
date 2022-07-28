using System;  
using Microsoft.Azure.Devices.Client;  
using System.Text;  
using Newtonsoft.Json;  
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;

namespace IoTProject301.Controllers
{
     [ApiController]
    [Route("controller")]
    public class Telemetry: ControllerBase
    {
    public static DeviceClient s_deviceClient;  
    
         static RegistryManager registryManager;
        static string constring = "HostName=NxTIoTTraining.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=RlHaCMxWGXDfNA+0BPe7b8WPjOnS+7B7EUAfBQSMOzg=";
         static string s_connectionString01 = "HostName=NxTIoTTraining.azure-devices.net;DeviceId=AarthiDevice;SharedAccessKey=SZrmyrL8vFwD38YWqiP+INNCLJEXATdTquz7qNCNWAc=";  
         
  
        

         [Route("SendDevice")]

        [HttpPost]
        public async void SendDeviceToCloudMessagesAsync(string DeviceName)  
        {  
            registryManager = RegistryManager.CreateFromConnectionString(constring);
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString01, Microsoft.Azure.Devices.Client.TransportType.Mqtt);  

            try  
            {  
                var device= registryManager.GetDeviceAsync(DeviceName);
                double minTemperature = 20;  
                double minHumidity = 60;  
                Random rand = new Random();  
  
                while (true)  
                {  
                    double currentTemperature = minTemperature + rand.NextDouble() * 15;  
                    double currentHumidity = minHumidity + rand.NextDouble() * 20;  
  
                   // Create JSON message  
  
                    var telemetryDataPoint = new  
                    {  
                         
                        temperature = currentTemperature,  
                        humidity = currentHumidity  
                    };  
  
                    string messageString = "";  
  
  
  
                    messageString = JsonConvert.SerializeObject(telemetryDataPoint);  
  
                    var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));  
  
                    // Add a custom application property to the message.  
                    // An IoT hub can filter on these properties without access to the message body.  
                    //message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");  
  
                    // Send the telemetry message  
                    await s_deviceClient.SendEventAsync(message);  
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);  
                    await Task.Delay(1000 * 10);  
                  
                }  
            }  
            catch (Exception ex)  
            {  
  
                throw ex;  
            }  
        }  
    }  
}  