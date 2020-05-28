using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using Opc.Ua;
using Opc.Ua.Client;
using Siemens.UAClientHelper;

namespace SimpleWebOPC
{
    public  static class OpcUastartup
    {
        private static UAClientHelperAPI m_Server = null;
        private static Subscription m_Subscription;
        private static Subscription m_SubscriptionBlock;
        private static UInt16 m_NameSpaceIndex = 0;


        public static UAClientHelperAPI get_m_server()
        {
            return m_Server;
        }

        public static void OPC()
        {
            m_Server = new UAClientHelperAPI();
            m_Server.CertificateValidationNotification += new CertificateValidationEventHandler(m_Server_CertificateEvent);

            try
            {
                m_Server.Connect("opc.tcp://192.168.90.39:4840", "none", MessageSecurityMode.SignAndEncrypt, true, "OpcUaClient", "12345678");
            }
            catch
            {
                Console.WriteLine("Wrong connections data, check opc address and credentials");
                return;
            }



            List<string> nodesToRead = new List<string>();

            nodesToRead.Add("ns=0;i=" + Variables.Server_NamespaceArray.ToString());
        }

        private static  void m_Server_CertificateEvent(CertificateValidator validator, CertificateValidationEventArgs e)
        {
            // Accept all certificate -> better ask user
            e.Accept = true;
        }
        
    }


}