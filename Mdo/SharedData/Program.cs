using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using PersistenceMocks;

namespace SharedData
{
    class Program
    {
        static void Main(string[] args)
        {
            SoapServerFormatterSinkProvider HserverProv = new SoapServerFormatterSinkProvider();
            HserverProv.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            SoapClientFormatterSinkProvider HclientProv = new SoapClientFormatterSinkProvider();
            IDictionary Hprops = new Hashtable();
            Hprops["port"] = "5000";
            HttpChannel Hchnl = new HttpChannel(Hprops, HclientProv, HserverProv);
            ChannelServices.RegisterChannel(Hchnl, false);

//            TcpChannel channel = new TcpChannel(5000);
//            ChannelServices.RegisterChannel(channel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(UserWarehouse), "UserWarehouse", WellKnownObjectMode.Singleton);
            Console.WriteLine("Remoting server started, press anything to stop...");
            Console.ReadLine();
        }
    }
}
