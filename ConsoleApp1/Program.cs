using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusCommunication
{
    class Program
    {
        private static  ModbusClient modbusClient;

        public static void CloseConnection()
        {
            try
            {
                modbusClient.Disconnect();
            }
            catch
            {
                Console.WriteLine("Connection cannot be closed");
            } 
        }

        static string CheckStatus()
        {
            if(!modbusClient.Connected)
            {
                return "not connected";
            }
            else
            {
                return "connected!";
            }
        }

        public static  void CreateConnection()
        {
            modbusClient = new ModbusClient("192.168.90.146", 502);    //Ip-Address and Port of Modbus-TCP-Server
            modbusClient.ConnectionTimeout = 10000;

            try
            {
                modbusClient.Connect();
            }
            catch
            {
                Console.WriteLine("not connected!");
            }

            Console.WriteLine(modbusClient.Connected);

            if (!modbusClient.Connected)
            {
                Console.WriteLine("error in connection");

            }

        }

        public static async Task Method1()
        {
            await Task.Run(async() =>
            {
                double Wh = 0;

                for (int i = 0; i < 100; i++)
                {

                    Wh = ModbusClient.ConvertRegistersToFloat(modbusClient.ReadHoldingRegisters(65, 2), ModbusClient.RegisterOrder.HighLow);

                    Console.WriteLine("wh: " + Wh);

                    await Task.Delay(200);

                }
            } );
        }

        public static async Task<double> Method2()
        {
            double Wh = 0;

            await Task.Run(async () =>
            {
               

                for (int i = 0; i < 100; i++)
                {

                    Wh = ModbusClient.ConvertRegistersToFloat(modbusClient.ReadHoldingRegisters(67, 2), ModbusClient.RegisterOrder.HighLow);

                    Console.WriteLine("dd: " + Wh);

                    await Task.Delay(100);

                }
            });

            return Wh;
        }

        public static  void  ReadValues()
        {


            double Wh = 0;

            for (int i = 0; i < 100; i++)
            {

                 Wh = ModbusClient.ConvertRegistersToFloat(modbusClient.ReadHoldingRegisters(65, 2), ModbusClient.RegisterOrder.HighLow);

                Console.WriteLine("wh: " + Wh);

                System.Threading.Thread.Sleep(1000);

            }

        }

        public static void liczGray_to_dec()
        {
          

            int value = 144;


            string Greyed = Convert.ToString(value, 2);
            string binary = Greyed.Substring(0, 4);
            string  result = Convert.ToInt32(binary, 2).ToString();
            int result_int = int.Parse(result);
            int inv = 0;

            // Taking xor until n becomes zero 
            for (; result_int != 0; result_int = result_int >> 1)
                inv ^= result_int;


            Console.WriteLine("afsd"+  inv);





        }

        static void Main(string[] args)
       {
            liczGray_to_dec();
          //  CreateConnection();
           // CheckStatus();
            //ReadValues();
        //      Method1();
          //   Method2();
             CloseConnection();

            Console.ReadKey();
        }
    }
}
