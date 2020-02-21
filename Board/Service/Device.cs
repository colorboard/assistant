using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Board.Service
{
    public class ColorDevice
    {
        public WebClient client = new WebClient();
        public string Address;

        public ColorDevice(string Address)
        {
            this.Address = Address;
        }

        public async Task<List<Package>> GetInstalled()
        {
            List<Package> Result = JsonConvert.DeserializeObject<List<Package>>(await client.DownloadStringTaskAsync(this.Address + "/packages/installed"));
            return Result;
        }

        public async Task<List<Package>> GetRepository()
        {
            List<Package> Result = JsonConvert.DeserializeObject<List<Package>>(await client.DownloadStringTaskAsync(this.Address + "/packages/repository"));
            return Result;
        }

        public async Task<Result> DeletePackage(Package package)
        {
            Result result = JsonConvert.DeserializeObject<Result>(await client.DownloadStringTaskAsync(this.Address + "/packages/delete/" + package.Identifier));
            return result;
        }

        public async Task<Result> InstallPackage(Package package)
        {
            Result result = JsonConvert.DeserializeObject<Result>(await client.DownloadStringTaskAsync(this.Address + "/packages/install/" + package.Identifier));
            return result;
        }

        public async Task<Result> OpenPackage(Package package)
        {
            Result result = JsonConvert.DeserializeObject<Result>(await client.DownloadStringTaskAsync(this.Address + "/packages/open/" + package.Identifier));
            return result;
        }

        public async Task<string> RunningPackage()
        {
            return await client.DownloadStringTaskAsync(this.Address + "/packages/running");
        }
    }
}
