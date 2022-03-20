using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace BinaryGame_GUI
{
    public class cls_game
    {
        static SerialPort comport;
        string[] Vmyports = { };
        string Vselectedport;
        bool Vrace = false;
        string Vcorrect = "";
        public string[] myports
        {
            get { return Vmyports; }
            set { Vmyports = value; }
        }
        public string selectedport
        {
            get { return Vselectedport; }
            set { Vselectedport = value; }
        }
        public bool race
        {
            get { return Vrace; }
            set { Vrace = value; }
        }
        public string corrcet
        {
            get { return Vcorrect; }
            set { Vcorrect = value; }
        }


        public void portslist()
        {
            Vmyports = SerialPort.GetPortNames();
        }
        public void connect()
        {
            try
            {
                SerialPort comport = new SerialPort(Vselectedport, 9600);
                comport.Open();
                comport.Write("1");
                comport.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void icebreaker()
        {
            try
            {
                SerialPort comport = new SerialPort(Vselectedport, 9600);
                comport.Open();
                comport.Write("2");
                comport.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
