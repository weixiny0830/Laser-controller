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
    public partial class laser2_form2 : Form
    {
        public laser2_form2()
        {
            InitializeComponent();
        }
        public static SerialPort serialport2 = new SerialPort();

        private void laser2_form2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //add baud rate list
            string[] baud = { "9600", "115200" };
            Tbbaudrate2.Items.AddRange(baud);

            //setup the default value
            Tbbaudrate2.Text = "115200";

            //read available COM list
            Tbcom2.Items.AddRange(SerialPort.GetPortNames());

            CheckForIllegalCrossThreadCalls = false;
            serialport2.DataReceived += new SerialDataReceivedEventHandler(sp2_dataReceived);
            serialport2.DtrEnable = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.laser_form2_FormClosing);

            if (Common_laser2.Received_data_tem_2 != null && Common_laser2.close_flag2 == true)
            {
                Tbcom2.Enabled = false;
                Tbbaudrate2.Enabled = false;
                Tbcom2.Text = Common_laser2.Tbcom_text_tem2;
                Tbbaudrate2.Text = Common_laser2.Tbbaud_text_tem2;

                BtopenCOM2.BackColor = Color.ForestGreen;
                BtopenCOM2.Text = "Close the COM";
            }
        }

        public void sp2_dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Form1 frm1 = (Form1)this.Owner;
            System.Threading.Thread.Sleep(100);//wait until all data received
            if (serialport2.IsOpen)
            {
                _ = new System.Text.UTF8Encoding();
                try
                {
                    //string manipulation, split laser parameters by ','

                    string receivedData_buff = serialport2.ReadExisting();//storage buff to save serial read string
                    string receivedData = "";//save the first line of the received string
                    foreach (char c in receivedData_buff)//split the first line, and give it the receivedData
                    {
                        receivedData += c;
                        if (c == '\r')
                            break;
                    }
                    Int32 length = 0;
                    string[] received_data_Arr = receivedData.Split(',');//use ',' to split the received data
                    Common_laser2.Received_data_tem_2 = receivedData;

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
                                frm1.BtLaser2On.Enabled = false;
                                frm1.BtLaser2Off.Enabled = true;
                                frm1.BtLaser2Off.ForeColor = Color.Red;
                                frm1.LbLasEna2.Text = "Status: On";
                            }
                            else
                            {
                                frm1.BtLaser2Off.Enabled = false;
                                frm1.BtLaser2On.Enabled = true;
                                frm1.BtLaser2On.ForeColor = Color.Green;
                                frm1.LbLasEna2.Text = "Status: Off";
                            }
                            frm1.LaserPow2_frm1 = (float.Parse(received_data_Arr[3])).ToString();
                            toolStripStatusLabel2_2.Text = "Total Service Time: " + received_data_Arr[4] + " Hours";
                            Common_frm1.LaserMaxPow2 = float.Parse(received_data_Arr[9]);

                            frm1.LbMaxPow5_2.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow4_2.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow3_2.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow2_2.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow1_2.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow0_2.Font = new Font("Times New Roman", 8);

                            frm1.LbMaxPow5_2.Text = Common_frm1.LaserMaxPow2.ToString() + "mW";
                            frm1.LbMaxPow4_2.Text = (Common_frm1.LaserMinPow2 + (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2) * 0.8).ToString() + "mW";
                            frm1.LbMaxPow3_2.Text = (Common_frm1.LaserMinPow2 + (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2) * 0.6).ToString() + "mW";
                            frm1.LbMaxPow2_2.Text = (Common_frm1.LaserMinPow2 + (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2) * 0.4).ToString() + "mW";
                            frm1.LbMaxPow1_2.Text = (Common_frm1.LaserMinPow2 + (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2) * 0.2).ToString() + "mW";
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

        public string Reced_Data2
        {
            set { Common_laser2.Received_data_tem_2 = value; }
            get { return Common_laser2.Received_data_tem_2; }
        }

        private void BtopenCOM2_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 frm1 = (Form1)this.Owner;
                //add the codepack in the try which exception occurs
                //Judge to open the COM based on the COM attribute
                frm1.BtLaser2On.Enabled = true;
                frm1.BtLaser2Off.Enabled = true;
                frm1.BtLaser2On.ForeColor = Color.Black;
                frm1.BtLaser2Off.ForeColor = Color.Black;
                if (serialport2.IsOpen || Common_laser2.close_flag2 == true)
                {
                    serialport2.Close();
                    BtopenCOM2.Text = "Open the COM";
                    Tbcom2.Enabled = true;
                    Tbbaudrate2.Enabled = true;
                    BtopenCOM2.BackColor = Color.Transparent;
                    Common_laser2.close_flag2 = false;
                    frm1.LbLasEna2.Text = "Status:";
                }
                else
                {
                    //if the COM is close, then setup the attribute to open it
                    Tbcom2.Enabled = false;
                    Tbbaudrate2.Enabled = false;
                    Common_laser2.close_flag2 = false;

                    serialport2.PortName = Tbcom2.Text;
                    Common_laser2.Tbcom_text_tem2 = Tbcom2.Text;
                    serialport2.BaudRate = Convert.ToInt32(Tbbaudrate2.Text);
                    Common_laser2.Tbbaud_text_tem2 = Tbbaudrate2.Text;
                    serialport2.DataBits = 8;
                    serialport2.Parity = Parity.None;
                    serialport2.StopBits = StopBits.One;
                    serialport2.Open();

                    BtopenCOM2.BackColor = Color.ForestGreen;
                    BtopenCOM2.Text = "Close the COM";
                }
            }
            catch (Exception ex)
            {
                serialport2 = new SerialPort();
                //refresh the COM option
                Tbcom2.Items.Clear();
                Tbcom2.Items.AddRange(SerialPort.GetPortNames());
                //ring if mistake
                System.Media.SystemSounds.Beep.Play();
                BtopenCOM2.Text = "Open the COM";
                BtopenCOM2.BackColor = Color.Transparent;
                MessageBox.Show(ex.Message, "Error");
                Tbcom2.Enabled = true;
                Tbbaudrate2.Enabled = true;
            }

        }

        private void laser_form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common_laser2.close_flag2 = true;
        }


        private void Btcalibrate2_Click(object sender, EventArgs e)
        {
            try
            {
                Lbcalibrate2.Font = new Font("Times New Roman", 12);
                Lbcalibrate2.Text = "Calibrating...";
                serialport2.Write("{cal ");
                serialport2.Write(Tbcalibrate2.Text);
                serialport2.Write("}\n");
                MessageBox.Show("Calibration Done!");
                Tbcalibrate2.Text = string.Empty;
                Lbcalibrate2.Text = "";
            }
            catch (Exception ex)
            {
                Lbcalibrate2.Text = "";
                MessageBox.Show(ex.Message, "Error");
            }
        }
        
    }
    public static class Common_laser2
    {
        public static string Received_data_tem_2 { get; set; }
        public static bool close_flag2 { get; set; }
        public static string Tbcom_text_tem2 { get; set; }
        public static string Tbbaud_text_tem2 { get; set; }
    }
}