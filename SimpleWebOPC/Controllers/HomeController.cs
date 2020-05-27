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
using SimpleWebOPC.ModbusCommunication;



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
        public double ModbusCommunication()
        {
            double var = 0;
            //     Communication modbusCommunication = new Communication();
            //     modbusCommunication.CreateConnection();

            //     var = modbusCommunication.ReadValues();

            return var;
        }

        [HttpPost]
        public int IB3()
        {
            string IB3 = null;

            int resolution = 120 / 23;

            IB3 = Read_IB3();
            ViewBag.Message = "wartosc ->  IB0: " + IB3;

            int result = int.Parse(IB3);
            int inv = 0;


            // Taking xor until n becomes zero 
            for (; result != 0; result = result >> 1)
                inv ^= result;

            return inv * resolution;

        }

        [HttpPost]
        public int IB0()
        {
            int resolution = 120 / 15;


            string val = Read_IB0();

            int value = Int32.Parse(val);

            string Greyed = Convert.ToString(value, 2);
            string binary = Greyed.Substring(0, 4);
            string result = Convert.ToInt32(binary, 2).ToString();
            int result_int = int.Parse(result);
            int inv = 0;

            // Taking xor until n becomes zero 
            for (; result_int != 0; result_int = result_int >> 1)
                inv ^= result_int;


            return inv * resolution;

        }

      

        [HttpPost] // auto
        public string DB11_DBB6_0()
        {
            string IB0 = null;
            IB0 = Read_DB11_DBB6_0();
            return IB0;
        }


        [HttpPost] //mda
        public string DB11_DBB6_1()
        {
            string val = null;
            val = Read_DB11_DBB6_1();
            return val;
        }

        [HttpPost] //JOG
        public string DB11_DBB6_2()
        {
            string val = null;
            val = Read_DB11_DBB6_2();
            return val;
        }

        [HttpPost] //Ref point
        public string DB11_DBB7_2()
        {
            string val = null;
            val = Read_DB11_DBB7_2();
            return val;
        }

        [HttpPost] // calculate work mode
        public string Work_mode()
        {
            string mode = null;
            string Auto = Read_DB11_DBB6_0();
            string MDA = Read_DB11_DBB6_1();
            string Jog = Read_DB11_DBB6_2();
            string refpoint = Read_DB11_DBB7_2();

            if (Auto.Equals("True") && Jog.Equals("False"))
            {
                mode = "Auto Mode";
            }
            else if (MDA.Equals("True") && Jog.Equals("False"))
            {
                mode = "MDA mode";
            }
            else if (Jog.Equals("True") && refpoint.Equals("False"))
            {
                mode = "Jog mode";
            }
            else if (Jog.Equals("True") && refpoint.Equals("True"))
            {
                mode = "ref & jog mode";
            }
            else
            {
                mode = "unidentify mode";
            }
            return mode;
        }

        [HttpPost] //calculate key position
        public string Key_pos()
        {
            string final_pos = null;
            string pos1 = Read_DB10_DBX56_5();
            string pos2 = Read_DB10_DBX56_6();
            string pos3 = Read_DB10_DBX56_7();

            if (pos1.Equals("True") && pos2.Equals("False") && pos3.Equals("False"))
            {
                final_pos = "Position 1";
            }
            else if (pos1.Equals("False") && pos2.Equals("True") && pos3.Equals("False"))
            {
                final_pos = "Position 2";

            }
            else if (pos1.Equals("False") && pos2.Equals("False") && pos3.Equals("True"))
            {
                final_pos = "Position 3";
            }
            else
            {
                final_pos = " unidentify position";
            }
            return final_pos;
        }



        [HttpPost] //Prog running
        public string DB20_DBX35_0()
        {
            string val = null;
            val = Read_DB20_DBX35_0();
            return val;
        }

        [HttpPost] //Prog running 2
        public string Q204_0()
        {
            string val = null;
            val = Read_Q204_0();
            return val;
        }

        [HttpPost] //Prog in idle
        public string Q204_1()
        {
            string val = null;
            val = Read_Q204_1();
            return val;
        }

        [HttpPost] //Alarm is active
        public string Q204_2()
        {
            string val = null;
            val = Read_Q204_2();
            return val;
        }

        [HttpPost] //feed start
        public string Q1_7()
        {
            string val = null;
            val = Feed_start();
            return val;
        }

        [HttpPost] //feed stop
        public string Q1_6()
        {
            string val = null;
            val = Feed_stop();
            return val;
        }

        [HttpPost] //Spindle start / stop
        public string spindle_start_stop()
        {
            string spindle_start_val = Spindle_start();
            string spindle_stop_val = Spindle_stop();

            if (spindle_start_val.Equals("True") && spindle_stop_val.Equals("False"))
            {
                return "Spindle Start";
            }
            else if (spindle_stop_val.Equals("True") && spindle_start_val.Equals("False"))
            {
                return "Spindle stop";
            }
            else
            {
                return "wrong spindle setting!";
            }       
        }

        [HttpPost] //Spindle start / stop
        public string feed_start_stop()
        {
            string feed_start_val = Feed_start();
            string feed_stop_val = Feed_stop();

            if (feed_start_val.Equals("True") && feed_stop_val.Equals("False"))
            {
                return "Feed Start";
            }
            else if (feed_stop_val.Equals("True") && feed_start_val.Equals("False"))
            {
                return "Feed stop";
            }
            else
            {
                return "wrong Feed setting!";
            }
        }

        [HttpPost] // List of axis movements
        public List<string> Axis_movements()
        {
            List<string> axis_list = new List<string>();
            axis_list = Axis_movement();

            return axis_list;
        }

        [HttpPost] //feed stop
        public string Prog_name()
        {
            string val = null;
            val = Read_prog_name();
            return val;
        }

        

        /*
         * 
         * 
         */

        private string Read_IB3()
        {


            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/IB3", 2).ToString());

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);

            return results[0];
        }

        private string Read_IB0()
        {


            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/IB0", 2).ToString());

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

        //Axis movement
        private List<string> Axis_movement()
        {
            List<string> axis_results = new List<string>();
            List<string> nodesToRead = new List<string>();

            nodesToRead.Add(new NodeId("/Plc/DB31.DBX64.6", 2).ToString()); // x axis minus
            nodesToRead.Add(new NodeId("/Plc/DB31.DBX64.7", 2).ToString()); // x axis plus

            nodesToRead.Add(new NodeId("/Plc/DB32.DBX64.6", 2).ToString()); // y axis minux
            nodesToRead.Add(new NodeId("/Plc/DB32.DBX64.7", 2).ToString()); // y axis plus

            nodesToRead.Add(new NodeId("/Plc/DB33.DBX64.6", 2).ToString()); // z axis minus
            nodesToRead.Add(new NodeId("/Plc/DB33.DBX64.7", 2).ToString()); // z axis plus

            nodesToRead.Add(new NodeId("/Plc/DB34.DBX64.6", 2).ToString()); // w axis minus
            nodesToRead.Add(new NodeId("/Plc/DB34.DBX64.7", 2).ToString()); // w axis plus

            nodesToRead.Add(new NodeId("/Plc/DB36.DBX64.6", 2).ToString()); // s axis minus
            nodesToRead.Add(new NodeId("/Plc/DB36.DBX64.7", 2).ToString()); // s axis plus


            axis_results = OpcUastartup.get_m_server().ReadValues(nodesToRead);


            return axis_results;
        }


        private string Read_prog_name()
        {
            List<string> nodesToRead = new List<string>();
            List<string> results = new List<string>();

            nodesToRead.Add(new NodeId("/Channel/ProgramInfo/selectedWorkPProg[u1,1]", 2).ToString());

            results = OpcUastartup.get_m_server().ReadValues(nodesToRead);


            return results[0];
        }

    }
}