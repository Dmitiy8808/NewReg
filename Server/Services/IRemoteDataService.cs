using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface IRemoteDataService
    {
        string SendRegRequestPackage(string package);

        string SendRegRequestPackage(byte[] package);
    }
}