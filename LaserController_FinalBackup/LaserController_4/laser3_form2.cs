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
    public partial class laser3_form2 : Form
    {
        public laser3_form2()
        {
            InitializeComponent();
        }
        public static SerialPort serialport3 = new SerialPort();
        private void laser3_form2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //add baud rate list
            string[] baud = { "9600", "115200" };
            Tbbaudrate3.Items.AddRange(baud);

            //setup the default value
            Tbbaudrate3.Text = "115200";

            //read available COM list
            Tbcom3.Items.AddRange(SerialPort.GetPortNames());

            CheckForIllegalCrossThreadCalls = false;
            serialport3.DataReceived += new SerialDataReceivedEventHandler(sp3_dataReceived);
            serialport3.DtrEnable = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.laser_form3_FormClosing);

            if (Common_laser3.Received_data_tem_3 != null && Common_laser3.close_flag3 == true)
            {
                Tbcom3.Enabled = false;
                Tbbaudrate3.Enabled = false;
                Tbcom3.Text = Common_laser3.Tbcom_text_tem3;
                Tbbaudrate3.Text = Common_laser3.Tbbaud_text_tem3;

                BtopenCOM3.BackColor = Color.ForestGreen;
                BtopenCOM3.Text = "Close the COM";
            }
        }
        public void sp3_dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Form1 frm1 = (Form1)this.Owner;
            System.Threading.Thread.Sleep(100);//wait until all data received
            if (serialport3.IsOpen)
            {
                _ = new System.Text.UTF8Encoding();
                try
                {
                    //string manipulation, split laser parameters by ','

                    string receivedData_buff = serialport3.ReadExisting();//storage buff to save serial read string
                    string receivedData = "";//save the first line of the received string
                    foreach (char c in receivedData_buff)//split the first line, and give it the receivedData
                    {
                        receivedData += c;
                        if (c == '\r')
                            break;
                    }
                    Int32 length = 0;
                    string[] received_data_Arr = receivedData.Split(',');//use ',' to split the received data
                    Common_laser3.Received_data_tem_3 = receivedData;

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
                                frm1.BtLaser3On.Enabled = false;
                                frm1.BtLaser3Off.Enabled = true;
                                frm1.BtLaser3Off.ForeColor = Color.Red;
                                frm1.LbLasEna3.Text = "Status: On";
                            }
                            else
                            {
                                frm1.BtLaser3Off.Enabled = false;
                                frm1.BtLaser3On.Enabled = true;
                                frm1.BtLaser3On.ForeColor = Color.Green;
                                frm1.LbLasEna3.Text = "Status: Off";
                            }
                            frm1.LaserPow3_frm1 = (float.Parse(received_data_Arr[3])).ToString();
                            toolStripStatusLabel2_3.Text = "Total Service Time: " + received_data_Arr[4] + " Hours";
                            Common_frm1.LaserMaxPow3 = float.Parse(received_data_Arr[9]);

                            frm1.LbMaxPow5_3.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow4_3.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow3_3.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow2_3.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow1_3.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow0_3.Font = new Font("Times New Roman", 8);

                            frm1.LbMaxPow5_3.Text = Common_frm1.LaserMaxPow3.ToString() + "mW";
                            frm1.LbMaxPow4_3.Text = (Common_frm1.LaserMinPow3 + (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3) * 0.8).ToString() + "mW";
                            frm1.LbMaxPow3_3.Text = (Common_frm1.LaserMinPow3 + (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3) * 0.6).ToString() + "mW";
                            frm1.LbMaxPow2_3.Text = (Common_frm1.LaserMinPow3 + (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3) * 0.4).ToString() + "mW";
                            frm1.LbMaxPow1_3.Text = (Common_frm1.LaserMinPow3 + (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3) * 0.2).ToString() + "mW";
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
        public string Reced_Data3
        {
            set { Common_laser3.Received_data_tem_3 = value; }
            get { return Common_laser3.Received_data_tem_3; }
        }

        private void BtopenCOM3_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 frm1 = (Form1)this.Owner;
                //add the codepack in the try which exception occurs
                //Judge to open the COM based on the COM attribute
                frm1.BtLaser3On.Enabled = true;
                frm1.BtLaser3Off.Enabled = true;
                frm1.BtLaser3On.ForeColor = Color.Black;
                frm1.BtLaser3Off.ForeColor = Color.Black;
                if (serialport3.IsOpen || Common_laser3.close_flag3 == true)
                {
                    serialport3.Close();
                    BtopenCOM3.Text = "Open the COM";
                    Tbcom3.Enabled = true;
                    Tbbaudrate3.Enabled = true;
                    BtopenCOM3.BackColor = Color.Transparent;
                    Common_laser3.close_flag3 = false;
                    frm1.LbLasEna3.Text = "Status:";
                }
                else
                {
                    //if the COM is close, then setup the attribute to open it
                    Tbcom3.Enabled = false;
                    Tbbaudrate3.Enabled = false;
                    Common_laser3.close_flag3 = false;

                    serialport3.PortName = Tbcom3.Text;
                    Common_laser3.Tbcom_text_tem3 = Tbcom3.Text;
                    serialport3.BaudRate = Convert.ToInt32(Tbbaudrate3.Text);
                    Common_laser3.Tbbaud_text_tem3 = Tbbaudrate3.Text;
                    serialport3.DataBits = 8;
                    serialport3.Parity = Parity.None;
                    serialport3.StopBits = StopBits.One;
                    serialport3.Open();

                    BtopenCOM3.BackColor = Color.ForestGreen;
                    BtopenCOM3.Text = "Close the COM";
                }
            }
            catch (Exception ex)
            {
                serialport3 = new SerialPort();
                //refresh the COM option
                Tbcom3.Items.Clear();
                Tbcom3.Items.AddRange(SerialPort.GetPortNames());
                //ring if mistake
                System.Media.SystemSounds.Beep.Play();
                BtopenCOM3.Text = "Open the COM";
                BtopenCOM3.BackColor = Color.Transparent;
                MessageBox.Show(ex.Message, "Error");
                Tbcom3.Enabled = true;
                Tbbaudrate3.Enabled = true;
            }
        }
        private void laser_form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common_laser3.close_flag3 = true;
        }

        private void Btcalibrate3_Click(object sender, EventArgs e)
        {
            try
            {
                Lbcalibrate3.Font = new Font("Times New Roman", 12);
                Lbcalibrate3.Text = "Calibrating...";
                serialport3.Write("{cal ");
                serialport3.Write(Tbcalibrate3.Text);
                serialport3.Write("}\n");
                MessageBox.Show("Calibration Done!");
                Tbcalibrate3.Text = string.Empty;
                Lbcalibrate3.Text = "";
            }
            catch (Exception ex)
            {
                Lbcalibrate3.Text = "";
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
    public static class Common_laser3
    {
        public static string Received_data_tem_3 { get; set; }
        public static bool close_flag3 { get; set; }
        public static string Tbcom_text_tem3 { get; set; }
        public static string Tbbaud_text_tem3 { get; set; }
    }
}
