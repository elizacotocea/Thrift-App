using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Server;
using Thrift.Transport;

namespace client
{
    class NotifyServer
    {

        public NotifyServer(int port)
        {
            Task.Run(() => start(port));
        }

        public void start(int port)
        {
            try
            {
                NotifyHandler handler = new NotifyHandler();
                NotifyService.Processor processor = new NotifyService.Processor(handler);
                TServerTransport serverTransport = new TServerSocket(port);
                TServer server = new TSimpleServer(processor, serverTransport);
                server.Serve();
            }
            catch (Exception x)
            {
                Console.WriteLine(x.StackTrace);
            }
        }
    }
}
