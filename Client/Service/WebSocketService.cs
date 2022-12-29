using System.Net.WebSockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Entities.DTOs;
using Entities.Models;
using Client.HttpRepository;

namespace Client.Service
{
    public class WebSocketService : IWebSocketService
    {
        private readonly IRegRequestHttpRepository _regRequestRepo; 
        public MessageResponse messageResponse = new MessageResponse();
        public WebSocketService(IRegRequestHttpRepository regRequestRepo)
        {
            _regRequestRepo = regRequestRepo;
        }
        public async Task<MessageResponse> GenerateRequest(RequestAbonent requestAbonent, CertRequestDataDto? certRequestData)
        {
            var opt = new JsonSerializerOptions {

                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var certRequestDataString = JsonSerializer.Serialize(certRequestData, opt);
            Console.WriteLine(certRequestDataString);
            Message message = new Message {
                Code = 1,
                Data = certRequestDataString
            };
            var stringMessage = JsonSerializer.Serialize(message, opt);
            var request = await SendMessage(stringMessage);
            return request;
        }

        public async Task<MessageResponse> InstallCertificate(CertificateDataDto certificateDataDto)
        {
            var opt = new JsonSerializerOptions {

                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var certDataString = JsonSerializer.Serialize(certificateDataDto, opt);
            Console.WriteLine(certDataString);
            Message message = new Message {
                Code = 2,
                Data = certDataString
            };
            var stringMessage = JsonSerializer.Serialize(message, opt);
            var request = await SendMessage(stringMessage);
            return request;
        }

        public async Task<MessageResponse> SendMessage(string message)
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
                messageResponse = JsonSerializer.Deserialize<MessageResponse>(receivedAsText);
                Console.WriteLine(receivedAsText);
                var stringReq = messageResponse.Data.value;
                Console.WriteLine(stringReq);

                return messageResponse;
                
            }
            Console.WriteLine(Encoding.UTF8.GetString(buffer));
            disposalTokenSource.Cancel();
            _ = webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", CancellationToken.None);
            
            return messageResponse;
        }

        public async Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent)
        {
            return await _regRequestRepo.GetCertRequestData(clientAbonent);
        }
        
    }
}