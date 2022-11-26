using System.Net.WebSockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Entities.DTOs;
using Entities.Models;
using Reg.Client.HttpRepository;

namespace Client.Service
{
    public class WebSocketService : IWebSocketService
    {
        private readonly IRegRequestHttpRepository _regRequestRepo; 
        public WebSocketService(IRegRequestHttpRepository regRequestRepo)
        {
            _regRequestRepo = regRequestRepo;
        }
        string message1 = """{"code":1,"data":"{\"providerCode\":80,\"providerName\":\"Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider\",\"signTool\":\"СКЗИ \\\"КриптоПро CSP\\\" версия 4.0\",\"certAttributes\":[{\"Oid\":\"2.5.4.3\",\"Value\":\"ООО \\\"НПЦ \\\"1С\\\"\"},{\"Oid\":\"2.5.4.4\",\"Value\":\"Ступин\"},{\"Oid\":\"2.5.4.42\",\"Value\":\"Дмитрий Алексеевич\"},{\"Oid\":\"2.5.4.6\",\"Value\":\"RU\"},{\"Oid\":\"2.5.4.8\",\"Value\":\"77 г. Москва\"},{\"Oid\":\"2.5.4.7\",\"Value\":\"Москва г\"},{\"Oid\":\"2.5.4.9\",\"Value\":\"Мосфильмовская ул 42 1 помещение 1, комната 7\"},{\"Oid\":\"1.2.643.3.131.1.1\",\"Value\":\"575101119088\"},{\"Oid\":\"1.2.643.100.4\",\"Value\":\"7729510210\"},{\"Oid\":\"2.5.4.10\",\"Value\":\"ООО \\\"НПЦ \\\"1С\\\"\"},{\"Oid\":\"2.5.4.12\",\"Value\":\"Руководитель направления\"},{\"Oid\":\"1.2.643.100.1\",\"Value\":\"1047796526546\"},{\"Oid\":\"1.2.643.100.3\",\"Value\":\"14979582340\"},{\"Oid\":\"1.2.840.113549.1.9.1\",\"Value\":\"stud@1c.ru\"}],\"enhKeyUsage\":[\"1.3.6.1.5.5.7.3.2\",\"1.3.6.1.5.5.7.3.4\"],\"certPolicies\":[{\"Oid\":\"1.2.643.100.113.1\",\"Value\":\"\"},{\"Oid\":\"1.2.643.100.113.2\",\"Value\":\"\"}],\"certAltarnativeNames\":[],\"keyUsage\":240,\"notBefore\":\"\",\"notAfter\":\"\",\"containerName\":\"5CC170EC-EE61-4DFB-9B42-07E3990F26B6\",\"requestName\":\"5CC170EC-EE61-4DFB-9B42-07E3990F26B3.p10\",\"identificationKind\":0}"}""";
        
         RequestAbonent clientAbonent = new RequestAbonent {
            Inn = "7729510210",
            Kpp = "772901001",
            Ogrn = "1047796526546",
            ShortName = "ООО \"НПЦ \"1С\"",
            FullName = "ООО \"НПЦ \"1С\"",
            Phone = "+7(495)1234567",
            PostalAddress = new AddressInfo {
                PostalCode = null,
                RegionId = 2,
                City = null,
                Locality = "Москва г",
                Area = "",
                Street = "Мосфильмовская ул",
                Building = "42",
                Bulk = "1",
                Flat = "помещение 1, комната 7"
            },
            LocationAddress = new AddressInfo {
                PostalCode = null,
                RegionId = 2,
                City = null,
                Locality = "Москва г",
                Area = "",
                Street = "Мосфильмовская ул",
                Building = "42",
                Bulk = "1",
                Flat = "помещение 1, комната 7"
            },
            Person = new PersonRequestInfo {
                LastName = "Ступин",
                FirstName = "Дмитрий",
                Patronymic = "Алексеевич",
                Snils = "149-565-823 11",
                BirthDate = "08.07.1977",
                BirthPlace = "г Орел",
                Country = "RUS",
                Gender = 1,
                Post = "Руководитель направления",
                Email = "test@1c.ru",
                OrgUnitName = "",
                PassportType = 1,
                PassportSeries = "2222",
                PassportNumber = "222222",
                PassportDate = "02.04.2011",
                PassportAddon = "ЖД РОВД",
                PassportUnit = "570-002",
                CryptoProviderId = 11,
                CryptoProviderName = "Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider",
                CryptoProviderCode = "80",
                Inn = "572201119011"
            },
            CertRequest = "",
            CertificationCenter = "ЗАО \"Калуга Астрал\"",
            ContainerName = "",
            OrganisationUnit = "",
            IsJuridical = true
        };

        public async Task GenerateRequest()
        {
            var opt = new JsonSerializerOptions {

                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var certRequestData =  await GetCertRequestData(clientAbonent);
            var certRequestDataString = JsonSerializer.Serialize(certRequestData, opt);
            Console.WriteLine(certRequestDataString);
            Message message = new Message {
                Code = 1,
                Data = certRequestDataString
            };
            var stringMessage = JsonSerializer.Serialize(message, opt);
            var request= SendMessage(stringMessage);
        }
        public async Task<string> SendMessage(string message)
        {
            CancellationTokenSource disposalTokenSource = new CancellationTokenSource();
            ClientWebSocket webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri("wss://127.0.0.1:9292/RegistrationOffice"), disposalTokenSource.Token);
            var dataToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            await webSocket.SendAsync(dataToSend, WebSocketMessageType.Text, true, disposalTokenSource.Token);
            var buffer = new ArraySegment<byte>(new byte[2048]);   // Уточнить на что влияет параметр был 1024
             while (!disposalTokenSource.IsCancellationRequested)
            {
                var received = await webSocket.ReceiveAsync(buffer, disposalTokenSource.Token);
                var receivedAsText = Encoding.UTF8.GetString(buffer.Array, 0, received.Count);
                Console.WriteLine(receivedAsText);
                
            }
            Console.WriteLine(Encoding.UTF8.GetString(buffer));
            disposalTokenSource.Cancel();
            _ = webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", CancellationToken.None);
            
            return "Web Socket Closed";
        }

        public async Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent)
        {
            return await _regRequestRepo.GetCertRequestData(clientAbonent);
        }
        
    }
}