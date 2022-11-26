// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;

// namespace Server.Services
// {
//     public class RegService : SoapHttpClientProtocol
//     {
//         [SoapDocumentMethod("http://regservice.keydisk.ru/SendPacket", ParameterStyle = SoapParameterStyle.Wrapped, RequestNamespace = "http://regservice.keydisk.ru/", ResponseNamespace = "http://regservice.keydisk.ru/", Use = SoapBindingUse.Literal)]
//         public string SendPacket(string packet) => (string) this.Invoke(nameof (SendPacket), new object[1]
//         {
//         (object) packet
//         })[0];
//     }
// }