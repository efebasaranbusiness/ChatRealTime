using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Sunucu IP Adresi: ");
        string serverIp = Console.ReadLine();

        Console.Write("Sunucu Port Numarası: ");
        string serverPort = Console.ReadLine();

        var hubConnection = new HubConnectionBuilder()
            .WithUrl($"http://{serverIp}:{serverPort}/chathub")  // Sunucu IP ve portu ile bağlantı
            .Build();

        hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            Console.WriteLine($"{user}: {message}");
        });

        try
        {
            await hubConnection.StartAsync();
            Console.WriteLine("Bağlantı başarılı.");

            Console.Write("Kullanıcı adı: ");
            string user = Console.ReadLine();

            while (true)
            {

                Console.Write("Mesaj: ");
                string message = Console.ReadLine();

                await hubConnection.InvokeAsync("SendMessage", user, message);

              
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Bağlantı hatası: {ex.Message}");
        }
    }
}
