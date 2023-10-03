using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ch23.Web
{

    public class RequrstReceiverEventArgs : EventArgs 
    {
    
        public HttpListenerContext Context { get; }
        public RequrstReceiverEventArgs(HttpListenerContext context) => Context = context;
    
    }
    public class WebServer
    {
        public event EventHandler<RequrstReceiverEventArgs> RequrstReceiver;

        //private TcpListener _Listener = new TcpListener(new IPEndPoint(IPAddress.Any, 8080));
        private HttpListener _HttpListener;
        private readonly int _Port;
        private bool _Enabled;
        private readonly object _SyncRoot = new object();

        public int Port => _Port;

        public bool Enabled { get => _Enabled; set { if (value) Start(); else Stop(); } }

        public WebServer(int Port) => _Port = Port;

        public void Start()
        {
            if (_Enabled) return;
            lock (_SyncRoot)
            {
                if (_Enabled) return;
                _HttpListener = new HttpListener();
                _HttpListener.Prefixes.Add($"http://*:{_Port}/");
                _HttpListener.Prefixes.Add($"http://+:{_Port}/");
                _Enabled = true;
            }
            ListenAsync();
            
        }

        public void Stop() 
        {
            if (!_Enabled) return;
            lock (_SyncRoot)
            {
                if (!_Enabled) return;
                _HttpListener = null;
                _Enabled = false;
            }
        }

        private async void ListenAsync() 
        {
            var listener = _HttpListener;
            listener.Start();

            while(_Enabled)
            {
                var rc = await listener.GetContextAsync().ConfigureAwait(false);
                ProcessRequest(rc);
            }

            listener.Stop();
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            RequrstReceiver?.Invoke(this, new RequrstReceiverEventArgs(context));    
        }

    }
}
