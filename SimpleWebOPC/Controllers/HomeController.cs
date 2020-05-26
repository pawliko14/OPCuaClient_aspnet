using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Opc.Ua;
using Opc.Ua.Client;
using Siemens.UAClientHelper;

namespace SimpleWebOPC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
           
            return View();
        }

        public ActionResult Contact()
        {
            string IB0 = null;
            IB0 = Read_IB3();
            ViewBag.Message = "wartosc ->  IB0: " + IB0;
            
            return View();

        }


        [HttpPost]
        public int Add()
        {
            string IB0 = null;

            int resolution = 120 / 23;

            IB0 = Read_IB3();
            ViewBag.Message = "wartosc ->  IB0: " + IB0;

            int result = int.Parse(IB0);

            int inv = 0;


            // Taking xor until n becomes zero 
            for (; result != 0; result = result >> 1)
                inv ^= result;

            return inv * resolution;

        }

        [HttpPost]
        public string DB11_DBB6_0()
        {
            string IB0 = null;
            IB0 = Read_DB11_DBB6_0();
            return IB0;
        }




        private string Read_IB3()
        {
            

            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/IB3", 2).ToString());

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);

            return results[0]; 
        }

        //auto
        private string Read_DB11_DBB6_0()
        {  
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB11.DBX6.0", 2).ToString());
  

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);

            return results[0];
        }

        //MDA
        private string Read_DB11_DBB6_1()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB11.DBX6.1", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //JOG
        private string Read_DB11_DBB6_2()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB11.DBX6.2", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //REF Point
        private string Read_DB11_DBB7_2()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB11.DBX7.2", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }


        //KEY POS 3
        private string Read_DB10_DBX56_7()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB10.DBX56.7", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //KEY POS 2
        private string Read_DB10_DBX56_6()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB10.DBX56.6", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //KEY POS 1
        private string Read_DB10_DBX56_5()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB10.DBX56.5", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //PROG RUNNING
        private string Read_DB20_DBX35_0()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB20.DBX35.0", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //program running
        private string Read_Q204_0()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/Q204.0", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //program in idle
        private string Read_Q204_1()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/Q204.1", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //program - alarm active
        private string Read_Q204_2()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/Q204.2", 2).ToString());


            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);
            return results[0];
        }

        //Feed start 
        private string Feed_start()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

             nodesToRead.Add(new NodeId("/Plc/Q1.7", 2).ToString());

             results = OpcUastartup.get_m_server().ReadValues(nodesToRead);


            return results[0]; 
        }

        //Feed stop 
        private string Feed_stop()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/Q1.6", 2).ToString());

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);


            return results[0];
        }

        //Spindle start 
        private string Spindle_start()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/Q2.1", 2).ToString());

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);


            return results[0];
        }

        //Spindle stop 
        private string Spindle_stop()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/Q2.0", 2).ToString());

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);


            return results[0];
        }
    }
}