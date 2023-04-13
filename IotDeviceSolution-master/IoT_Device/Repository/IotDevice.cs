﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;


namespace IotHubDevice.Repository
{
    public class IotDevice
    {
        public static RegistryManager registryManager;

        private static string iothubConnection = "HostName=COURSEIOTHUBDEMO.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=z6j5aAhwoUFnme7OVB4XGYTCtMKkYpW4zHMdY2wdoz4=\n";

        public  static async Task AddIotDevice(string deviceName)
        {
            Device device;
            if(string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("Kindly Add Device Name:");
            }
            registryManager= RegistryManager.CreateFromConnectionString(iothubConnection);
            device = await registryManager.AddDeviceAsync(new Device(deviceName));
            
        }

        public static async Task<Device> GetIotDevice(string deviceId)
        {
            Device device;
            registryManager=RegistryManager.CreateFromConnectionString(iothubConnection);
            device= await registryManager.GetDeviceAsync(deviceId);
            return device;
        }

        public  static async Task<Device> UpdateIotDevice(string deviceId)
        {
            if(string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("Kindly Enter Device Id:");
            }
            Device device =  new Device(deviceId);
            registryManager = RegistryManager.CreateFromConnectionString(iothubConnection);
            device = await registryManager.GetDeviceAsync(deviceId);
            device.StatusReason = "Device Updated";
            device = await registryManager.UpdateDeviceAsync(device);
            return device;

        }

        public static async Task DeleteDevice(string deviceId)
        {
            registryManager = RegistryManager.CreateFromConnectionString(iothubConnection);
            await registryManager.RemoveDeviceAsync(deviceId);
        }
    }
}
