// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Server.Services
// {
//     public class RemoteDataService
//     {
//         public string SendRegRequestPackage(byte[] package) => this.SendRegRequestPackage(Convert.ToBase64String(package));

//         public string SendRegRequestPackage(string package)
//         {
//             using (RegService regService = new RegService())
//                 return regService.SendPacket(package);
//         }
//     }
// }