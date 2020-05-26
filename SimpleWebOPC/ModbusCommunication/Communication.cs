using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyModbus;


namespace SimpleWebOPC.ModbusCommunication
{

    public class Communication
    {

        private ModbusClient modbusClient;


        public string ReadStatus()
        {
            if(!modbusClient.Connected)
            {
                return "connection is closed";
            }
            else
            {
                return "connection is up";
            }
        }

        public void CloseConnection()
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

        public void CreateConnection()
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
        public double ReadValues()
        {
            Console.WriteLine("connected!");


            double Wh = 0;

            for (int i = 0; i < 50; i++)
            {

                Wh = ModbusClient.ConvertRegistersToFloat(modbusClient.ReadHoldingRegisters(65, 2), ModbusClient.RegisterOrder.HighLow);

                System.Threading.Thread.Sleep(1000);

            }

            return Wh;
        }




    }
}