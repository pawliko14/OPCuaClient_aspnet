using  EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModbusCommunication
{
    class Program
    {

     

        static void Main(string[] args)
        {
              ModbusClient modbusClient = new ModbusClient("192.168.90.146", 502);    //Ip-Address and Port of Modbus-TCP-Server
           
            modbusClient.ConnectionTimeout = 10000;
            int timeout = modbusClient.ConnectionTimeout;
            Console.WriteLine("timedoutd: " + timeout);


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
            else
            {
                Console.WriteLine("connected!");

                for (int i = 0; i < 100; i++)
                {

                    double Wh = ModbusClient.ConvertRegistersToFloat(modbusClient.ReadHoldingRegisters(65, 2));
                    Console.WriteLine("wh: " + Wh);
                    System.Threading.Thread.Sleep(1000);

                }
            }
            modbusClient.Disconnect();


            Console.ReadKey();
        }
    }
}
