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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static SerialPort serialPort1 = new SerialPort();
        private void Form2_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            //add baud rate list
            string[] baud = { "9600", "115200" };
            Tbbaudrate.Items.AddRange(baud);

            //setup the default value
            Tbbaudrate.Text = "115200";

            //read available COM list
            Tbcom.Items.AddRange(SerialPort.GetPortNames());

            CheckForIllegalCrossThreadCalls = false;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(sp1_dataReceived);
            serialPort1.DtrEnable = true;

            if (Common_frm2.Receved_data_tem != null && Common_frm2.close_flag == true)
            {
                Tbcom.Enabled = false;
                Tbbaudrate.Enabled = false;
                Tbcom.Text = Common_frm2.Tbcom_text_tem;
                Tbbaudrate.Text = Common_frm2.Tbbaud_text_tem;

                BtopenCOM.BackColor = Color.ForestGreen;
                BtopenCOM.Text = "Close the COM";
            }

        }
        
        public void sp1_dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Form1 frm1 = (Form1)this.Owner;
            System.Threading.Thread.Sleep(100);//wait until all data received
            if (serialPort1.IsOpen)
            {
                _ = new System.Text.UTF8Encoding();
                try
                {
                    //string manipulation, split laser parameters by ','

                    string receivedData_buff = serialPort1.ReadExisting();//storage buff to save serial read string
                    string receivedData = "";//save the first line of the received string
                    foreach (char c in receivedData_buff)//split the first line, and give it the receivedData
                    {
                        receivedData += c;
                        if (c == '\r')
                            break;
                    }
                    Int32 length = 0;
                    string[] received_data_Arr = receivedData.Split(',');//use ',' to split the received data
                    Common_frm2.Receved_data_tem = receivedData;

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
                                frm1.BtLaser1On.Enabled = false;
                                frm1.BtLaser1Off.Enabled = true;
                                frm1.BtLaser1Off.ForeColor = Color.Red;
                                frm1.LbLasEna.Text = "Status: On";
                            }
                            else
                            {
                                frm1.BtLaser1Off.Enabled = false;
                                frm1.BtLaser1On.Enabled = true;
                                frm1.BtLaser1On.ForeColor = Color.Green;
                                frm1.LbLasEna.Text = "Status: Off";
                            }
                            frm1.LaserPow_frm1 = (float.Parse(received_data_Arr[3])).ToString();
                            toolStripStatusLabel2.Text = "Total Service Time: " + received_data_Arr[4] + " Hours";
                            Common_frm1.LaserMaxPow = float.Parse(received_data_Arr[9]);

                            frm1.LbMaxPow5.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow4.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow3.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow2.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow1.Font = new Font("Times New Roman", 8);
                            frm1.LbMaxPow0.Font = new Font("Times New Roman", 8);

                            frm1.LbMaxPow5.Text = Common_frm1.LaserMaxPow.ToString() + "mW";
                            frm1.LbMaxPow4.Text = (Common_frm1.LaserMinPow + (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow) * 0.8).ToString() + "mW";
                            frm1.LbMaxPow3.Text = (Common_frm1.LaserMinPow + (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow) * 0.6).ToString() + "mW";
                            frm1.LbMaxPow2.Text = (Common_frm1.LaserMinPow + (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow) * 0.4).ToString() + "mW";
                            frm1.LbMaxPow1.Text = (Common_frm1.LaserMinPow + (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow) * 0.2).ToString() + "mW";
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

        public string Reced_Data
        {
            set { Common_frm2.Receved_data_tem = value; }
            get { return Common_frm2.Receved_data_tem; }
        }

        private void BtopenCOM_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 frm1 = (Form1)this.Owner;
                //add the codepack in the try which exception occurs
                //Judge to open the COM based on the COM attribute
                frm1.BtLaser1On.Enabled = true;
                frm1.BtLaser1Off.Enabled = true;
                frm1.BtLaser1On.ForeColor = Color.Black;
                frm1.BtLaser1Off.ForeColor = Color.Black;

                if (serialPort1.IsOpen || Common_frm2.close_flag == true)
                {
                    serialPort1.Close();
                    BtopenCOM.Text = "Open the COM";
                    Tbcom.Enabled = true;
                    Tbbaudrate.Enabled = true;
                    BtopenCOM.BackColor = Color.Transparent;
                    Common_frm2.close_flag = false;
                    frm1.LbLasEna.Text = "Status:";
                }
                else
                {
                    //if the COM is close, then setup the attribute to open it
                    Tbcom.Enabled = false;
                    Tbbaudrate.Enabled = false;
                    Common_frm2.close_flag = false;

                    serialPort1.PortName = Tbcom.Text;
                    Common_frm2.Tbcom_text_tem = Tbcom.Text;
                    serialPort1.BaudRate = Convert.ToInt32(Tbbaudrate.Text);
                    Common_frm2.Tbbaud_text_tem = Tbbaudrate.Text;
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = Parity.None;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Open();

                    BtopenCOM.BackColor = Color.ForestGreen;
                    BtopenCOM.Text = "Close the COM";
                }
            }
            catch (Exception ex)
            {
                serialPort1 = new SerialPort();
                //refresh the COM option
                Tbcom.Items.Clear();
                Tbcom.Items.AddRange(SerialPort.GetPortNames());
                //ring if mistake
                System.Media.SystemSounds.Beep.Play();
                BtopenCOM.Text = "Open the COM";
                BtopenCOM.BackColor = Color.Transparent;
                MessageBox.Show(ex.Message, "Error");
                Tbcom.Enabled = true;
                Tbbaudrate.Enabled = true;
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Common_frm2.close_flag = true;
        }

        private void Btcalibrate_Click(object sender, EventArgs e)
        {
            try
            {
                Lbcalibrate.Font = new Font("Times New Roman", 12);
                Lbcalibrate.Text = "Calibrating...";
                serialPort1.Write("{cal ");
                serialPort1.Write(Tbcalibrate.Text);
                serialPort1.Write("}\n");
                MessageBox.Show("Calibration Done!");
                Tbcalibrate.Text = string.Empty;
                Lbcalibrate.Text = "";
            }
            catch (Exception ex)
            {
                Lbcalibrate.Text = "";
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
    public static class Common_frm2
    {
        public static string Receved_data_tem { get; set; }
        public static bool close_flag { get; set; }
        public static string Tbcom_text_tem { get; set; }
        public static string Tbbaud_text_tem { get; set; }
    }
}//uploaded to Git Repostory