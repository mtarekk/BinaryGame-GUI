namespace BinaryGame_GUI
{
    public partial class form_start : Form
    {
        public static cls_game myclass = new cls_game();
        form_game f2 = new form_game();

        bool portselected = false;

        public form_start()
        {
            InitializeComponent();
        }

        private void Start_Form_Load(object sender, EventArgs e)
        {
            myclass.portslist();
            for (int i = 0; i <= myclass.myports.Length - 1; i++)
            {
                comboBox1.Items.Add(myclass.myports[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            myclass.portslist();
            for (int i = 0; i <= myclass.myports.Length - 1; i++)
            {
                comboBox1.Items.Add(myclass.myports[i]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (portselected)
            {
                if (radioButton2.Checked == true)
                {
                    myclass.race = true;
                }
                else
                {
                    myclass.race = false;

                }
                f2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please select your port first!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            myclass.selectedport = comboBox1.Text;
            portselected = true;
            myclass.connect();
        }
    }
}