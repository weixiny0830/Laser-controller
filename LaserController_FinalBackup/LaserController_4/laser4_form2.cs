using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserController_4
{
    public partial class laser4_form2 : Form
    {
        public laser4_form2()
        {
            InitializeComponent();
        }
        public static SerialPort serialport4 = new SerialPort();
        private void laser4_form2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //add baud rate list
            string[] baud = { "9600", "115200" };
            Tbbaudrate4.Items.AddRange(baud);

            //setup the default value
            Tbbaudrate4.Text = "115200";

            //read available COM list
            Tbcom4.Items.AddRange(SerialPort.GetPortNames());

            CheckForIllegalCrossThreadCalls = false;
            serialport4.DataReceived += new SerialDataReceivedEventHandler(sp4_dataReceived);
            serialport4.DtrEnable = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.laser_form4_FormClosing);

            if (Common_laser4.Received_data_tem_4 != null && Common_laser4.close_flag4 == true)
            {
                Tbcom4.Enabled = false;
                Tbbaudrate4.Enabled = false;
                Tbcom4.Text = Common_laser4.Tbcom_text_tem4;
                Tbbaudrate4.Text = Common_laser4.Tbbaud_text_tem4;

                BtopenCOM4.BackColor = Color.ForestGreen;
                BtopenCOM4.Text = "Close the COM";
            }
        }
        public void sp4_dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Form1 frm1 = (Form1)this.Owner;
            System.Threading.Thread.Sleep(100);//wait until all data received
            if (serialport4.IsOpen)
            {
                _ = new System.Text.UTF8Encoding();
                try
                {
                    //string manipulation, split laser parameters by ','

                    string receivedData_buff = serialport4.ReadExisting();//storage buff to save serial read string
                    string receivedData = "";//save the first line of the received string
                    foreach (char c in receivedData_buff)//split the first line, and give it the receivedData
                    {
                        receivedData += c;
                        if (c == '\r')
                            break;
                    }
                    Int32 length = 0;
                    string[] received_data_Arr = receivedData.Split(',');//use ',' to split the received data
                    Common_laser4.Received_data_tem_4 = receivedData;

                    if (received_data_Arr[0] == "Laser" && received_data_Arr[received_data_Arr.Length - 3] == "End")//if the data starts with "Laser" and ends with "End"
                    {//data is correct
                        for (uint i = 0; i < received_data_Arr.Length - 2; i++)
                        {
                            length += received_data_Arr[i].Length;//calculate the length of the received string
                        }
                        if (length == int.Parse(received_data_Arr[received_data_Arr.Length - 2]))//if the calculated length equals to the checksum, means no data loss
                        {
                            if (received_data_Arr[1] == "1")//first data is laser on/off
                            {
                                frm1.BtLaser4On.Enabled = false;
                                frm1.BtLaser4Off.Enabled = true;
                                frm1.BtLaser4Off.ForeColor = Color.Red;
                                frm1.LbLasEna4.Text = "Status: On";
                            }
                            else
                            {
                                frm1.BtLaser4Off.Enabled = false;
                                frm1.BtLaser4On.Enabled = true;
                                frm1.BtLaser4On.ForeColor = Color.Green;
                                frm1.LbLasEna4.Text = "Status: Off";
                            }
                            frm1.LaserPow4_frm1 = (float.Parse(received_data_Arr[3])).ToString();
                            toolStripStatusLabel2_4.Text = "Total Service Time: " + received_data_Arr[4] + " Hours";
                            Common_frm1.LaserMaxPow4 = float.Parse(received_data_Arr[9]);

                            frm1.LbMaxPow5_4.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow4_4.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow3_4.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow2_4.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow1_4.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow0_4.Font = new Font("Times New Roman", 8);

                            frm1.LbMaxPow5_4.Text = Common_frm1.LaserMaxPow4.ToString() + "mW";
                            frm1.LbMaxPow4_4.Text = (Common_frm1.LaserMinPow4 + (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4) * 0.8).ToString() + "mW";
                            frm1.LbMaxPow3_4.Text = (Common_frm1.LaserMinPow4 + (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4) * 0.6).ToString() + "mW";
                            frm1.LbMaxPow2_4.Text = (Common_frm1.LaserMinPow4 + (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4) * 0.4).ToString() + "mW";
                            frm1.LbMaxPow1_4.Text = (Common_frm1.LaserMinPow4 + (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4) * 0.2).ToString() + "mW";
                        }
                        else
                        {
                            MessageBox.Show("Data loss...");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message, "Mistake Occurs");
                }
            }
        }
        public string Reced_Data4
        {
            set { Common_laser4.Received_data_tem_4 = value; }
            get { return Common_laser4.Received_data_tem_4; }
        }

        private void BtopenCOM4_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 frm1 = (Form1)this.Owner;
                //add the codepack in the try which exception occurs
                //Judge to open the COM based on the COM attribute
                frm1.BtLaser4On.Enabled = true;
                frm1.BtLaser4Off.Enabled = true;
                frm1.BtLaser4On.ForeColor = Color.Black;
                frm1.BtLaser4Off.ForeColor = Color.Black;
                if (serialport4.IsOpen || Common_laser4.close_flag4 == true)
                {
                    serialport4.Close();
                    BtopenCOM4.Text = "Open the COM";
                    Tbcom4.Enabled = true;
                    Tbbaudrate4.Enabled = true;
                    BtopenCOM4.BackColor = Color.Transparent;
                    Common_laser4.close_flag4 = false;
                    frm1.LbLasEna4.Text = "Status:";
                }
                else
                {
                    //if the COM is close, then setup the attribute to open it
                    Tbcom4.Enabled = false;
                    Tbbaudrate4.Enabled = false;
                    Common_laser4.close_flag4 = false;

                    serialport4.PortName = Tbcom4.Text;
                    Common_laser4.Tbcom_text_tem4 = Tbcom4.Text;
                    serialport4.BaudRate = Convert.ToInt32(Tbbaudrate4.Text);
                    Common_laser4.Tbbaud_text_tem4 = Tbbaudrate4.Text;
                    serialport4.DataBits = 8;
                    serialport4.Parity = Parity.None;
                    serialport4.StopBits = StopBits.One;
                    serialport4.Open();

                    BtopenCOM4.BackColor = Color.ForestGreen;
                    BtopenCOM4.Text = "Close the COM";
                }
            }
            catch (Exception ex)
            {
                serialport4 = new SerialPort();
                //refresh the COM option
                Tbcom4.Items.Clear();
                Tbcom4.Items.AddRange(SerialPort.GetPortNames());
                //ring if mistake
                System.Media.SystemSounds.Beep.Play();
                BtopenCOM4.Text = "Open the COM";
                BtopenCOM4.BackColor = Color.Transparent;
                MessageBox.Show(ex.Message, "Error");
                Tbcom4.Enabled = true;
                Tbbaudrate4.Enabled = true;
            }
        }
        private void laser_form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common_laser4.close_flag4 = true;
        }

        private void Btcalibrate4_Click(object sender, EventArgs e)
        {
            try
            {
                Lbcalibrate4.Font = new Font("Times New Roman", 12);
                Lbcalibrate4.Text = "Calibrating...";
                serialport4.Write("{cal ");
                serialport4.Write(Tbcalibrate4.Text);
                serialport4.Write("}\n");
                MessageBox.Show("Calibration Done!");
                Tbcalibrate4.Text = string.Empty;
                Lbcalibrate4.Text = "";
            }
            catch (Exception ex)
            {
                Lbcalibrate4.Text = "";
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }

    public static class Common_laser4
    {
        public static string Received_data_tem_4 { get; set; }
        public static bool close_flag4 { get; set; }
        public static string Tbcom_text_tem4 { get; set; }
        public static string Tbbaud_text_tem4 { get; set; }
    }
}
