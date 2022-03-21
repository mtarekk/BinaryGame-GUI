using System.IO.Ports;

namespace BinaryGame_GUI
{
    public partial class form_game : Form
    {
        static SerialPort comport;
        int score = 0;
        int incorrect = 0;
        string incomingdata = "";
        int timeleft = 60;
        bool timerunning = false;
        bool WeAreConnected = true;
        int binary = 0;
        public form_game()
        {
            InitializeComponent();
        }

        public void Game_Form_Load(object sender, EventArgs e)
        {
            if (form_start.myclass.race == true)
            {
                label1.Text = "Binary Game Race Mode";
                button2.Visible = true;
                label2.ForeColor = Color.Black;
                label2.Text = "0";
                label3.ForeColor = Color.Black;
                label3.Text = timeleft.ToString();

            }
            else
            {
                form_start.myclass.icebreaker();
                label1.Text = "Binary Game Practice Mode";
                label4.Visible = true;
                label5.Visible = true;
                button2.Visible = false;
                label2.ForeColor = Color.Green;
                label2.Text = "0";
                label3.ForeColor = Color.Red;
                label3.Text = "0";
            }
            comport = new SerialPort(form_start.myclass.selectedport, 9600);
            comport.DataReceived += new SerialDataReceivedEventHandler(comport_DataReceivedHandler);
            try
            {
                comport.Open();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
            timer2.Start();
        }

        private void form_game_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (comport.IsOpen)
            {
                timer2.Stop();
                comport.Write("4");
                comport.Close();
            }
            Application.Exit();
        }

        public void comport_DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            if (WeAreConnected)
            {
                incomingdata = comport.ReadLine();
                if (form_start.myclass.race == true)
                {
                    if (timerunning)
                    {
                        if (incomingdata.Trim() == "C")
                        {
                            score++;
                            label2.Invoke(new MethodInvoker(delegate { label2.Text = score.ToString(); }));
                        }
                        else
                        {
                            label5.Invoke(new MethodInvoker(delegate { label5.Text = incomingdata.Trim('F'); }));
                        }
                    }

                }
                else
                {
                    if (incomingdata.Trim() == "C")
                    {
                        score++;
                        label2.Invoke(new MethodInvoker(delegate { label2.Text = score.ToString(); }));
                    }
                    else if (incomingdata.Trim() == "F")
                    {
                        incorrect++;
                        label3.Invoke(new MethodInvoker(delegate { label3.Text = incorrect.ToString(); }));
                        label6.Invoke(new MethodInvoker(delegate { label6.Text = "Your Binary should be: " + Convert.ToString(binary, 2); }));
                        label6.Invoke(new MethodInvoker(delegate { label6.Visible = true; }));


                    }
                    else
                    {
                        binary = Convert.ToInt32(incomingdata);
                        label5.Invoke(new MethodInvoker(delegate { label5.Text = incomingdata; }));
                        label6.Invoke(new MethodInvoker(delegate { label6.Visible = false; }));
                    }


                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (form_start.myclass.race == true)
            {
                timer1.Stop();
                connect();
                label4.Visible = false;
                label5.Visible = false;
                timeleft = 60;
                score = 0;
                label2.Text = score.ToString();
                label3.Text = timeleft.ToString();
                button2.Visible = true;

            }
            else
            {
                score = 0;
                incorrect = 0;
                label2.Invoke(new MethodInvoker(delegate { label2.Text = score.ToString(); }));
                label3.Invoke(new MethodInvoker(delegate { label3.Text = incorrect.ToString(); }));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            icebreaker();
            timer1.Start();
            button2.Visible = false;
            label4.Visible = true;
            label5.Visible = true;
            timerunning = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeleft > 0)
            {
                timeleft--;
                label3.Text = timeleft.ToString();
            }
            else
            {
                timer1.Stop();
                comport.Write("3");
                timerunning = false;
                label4.Visible = false;
                label5.Text = "Time is Up!";
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (!comport.IsOpen)
            {
                WeAreConnected = false;
                timer2.Stop();
                MessageBox.Show("The BinaryGame device has been disconnected. Please re-connect the device and try again.");
                Application.Restart();
            }
        }



        public void connect()
        {
            try
            {
                comport.Write("1");
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
                comport.Write("2");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
