using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace DownloadManager.Services
{
    class SocketDataService : ISocketDataService
    {
        public event SocketDataEvent OnReceived;

        static StreamSocketListener listener = new StreamSocketListener();
        int port = 25346;

        public SocketDataService()
        {
            listener.Control.KeepAlive = true;
            listener.ConnectionReceived += Listener_ConnectionReceived;
        }

        public async Task Begin()
        {
            await listener.BindServiceNameAsync(port.ToString());
        }

        private async void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
        {
            Debug.WriteLine("Got connection");

            uint bufferSize = 1024 * 8;
            byte[] buffer = new byte[bufferSize];
            uint realCount = 0;
            using (MemoryStream ms = new MemoryStream())
            using (var dr = new DataReader(args.Socket.InputStream))
            {
                dr.InputStreamOptions = InputStreamOptions.Partial;

                while ((realCount = await dr.LoadAsync(bufferSize)) > 0)
                {
                    dr.ReadBytes(buffer);
                    await ms.WriteAsync(buffer, 0, (int)realCount);
                }
                var str = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                Debug.WriteLine($"data:{str}");
                OnReceived.Invoke(System.Text.Encoding.UTF8.GetString(ms.ToArray()));
            }

            using (var dw = new DataWriter(args.Socket.OutputStream))
            {
                dw.WriteString(@"HTTP/1.1 200 OK
Cache-Control: private
Connection: Keep-Alive
Content-Length: 0
Content-Type: text/html; charset=UTF-8
Server: kumblr

");
                await dw.StoreAsync();
                dw.DetachStream();
            }
        }
    }
}
