using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserController_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TbMaxPow0.Hide();
            TbMaxPow0.KeyPress += new KeyPressEventHandler(TbMaxPow0_Press);
            TbMaxPow5.Hide();
            TbMaxPow5.KeyPress += new KeyPressEventHandler(TbMaxPow5_Press);
            this.MaximizeBox = false;
        }

        public string LaserPow_frm1
        {
            set { this.TbLaserPow.Text = value; }
            get { return this.TbLaserPow.Text; }
        }
        private void BtAdvance_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Owner = this;
            frm2.ShowDialog();                   
        }

        private void BtLaser1On_Click(object sender, EventArgs e)
        {
            try 
            {
                Form2.serialPort1.Write("{lason}\n");
                lbLaserEnable.Font = new Font("Times New Roman", 12);
                lbLaserEnable.Text = "Laser Emission Starting...";
                //Form2.Btcalibrate.Enabled = true;
                Btpowerset.Enabled = true;
            }
            catch (Exception ex)
            {
                lbLaserEnable.Text = "Status:";
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void BtLaser1Off_Click(object sender, EventArgs e)
        {
            try
            {
                Form2.serialPort1.Write("{lasoff}\n");
                lbLaserEnable.Font = new Font("Times New Roman", 12);
                lbLaserEnable.Text = "Laser Emission Stopping...";
                //Btcalibrate.Enabled = false;
                Btpowerset.Enabled = false;
            }
            catch (Exception ex)
            {
                lbLaserEnable.Text = "Status:";
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Btpowerset_Click(object sender, EventArgs e)
        {
            try
            {
                Form2.serialPort1.Write("{setpower ");
                Form2.serialPort1.Write(TbPowSet.Text);
                TbarPowerSet.Value = Convert.ToInt32(((float.Parse(TbPowSet.Text) - Common_frm1.LaserMinPow)/ (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow))*100);
                Form2.serialPort1.Write("}\n");
                MessageBox.Show("Power Set Done!");
            }
            catch (Exception ex)
            {
                TbPowSet.Text = "0";
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void LbMaxPow5_Click(object sender, EventArgs e)
        {
            TbMaxPow5.Show();
        }
        public void TbMaxPow5_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow5.Hide();
                    Common_frm1.LaserMaxPow = float.Parse(TbMaxPow5.Text);

                    Form2.serialPort1.Write("{setpowerMax ");
                    Form2.serialPort1.Write(TbMaxPow5.Text);
                    Form2.serialPort1.Write("}\n");
                    MessageBox.Show("Maximum Power Set Done!");

                    LbMaxPow5.Font = new Font("Times New Roman", 8);
                    LbMaxPow4.Font = new Font("Times New Roman", 8);
                    LbMaxPow3.Font = new Font("Times New Roman", 8);
                    LbMaxPow2.Font = new Font("Times New Roman", 8);
                    LbMaxPow1.Font = new Font("Times New Roman", 8);
                    LbMaxPow0.Font = new Font("Times New Roman", 8);

                    LbMaxPow5.Text = Common_frm1.LaserMaxPow.ToString() + "mW";
                    LbMaxPow4.Text = (Common_frm1.LaserMaxPow * 0.8).ToString() + "mW";
                    LbMaxPow3.Text = (Common_frm1.LaserMaxPow * 0.6).ToString() + "mW";
                    LbMaxPow2.Text = (Common_frm1.LaserMaxPow * 0.4).ToString() + "mW";
                    LbMaxPow1.Text = (Common_frm1.LaserMaxPow * 0.2).ToString() + "mW";
                }
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Please enter a number");
            }
        }

        public void TbMaxPow0_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow0.Hide();
                    if (Form2.serialPort1.IsOpen)
                    {
                        if (float.Parse(TbMaxPow0.Text) != 0 && float.Parse(TbMaxPow0.Text) < Common_frm1.LaserMaxPow)
                        {

                            Common_frm1.LaserMinPow = float.Parse(TbMaxPow0.Text);
                            float number_gap = (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow) / 5;

                            LbMaxPow5.Font = new Font("Times New Roman", 8);
                            LbMaxPow4.Font = new Font("Times New Roman", 8);
                            LbMaxPow3.Font = new Font("Times New Roman", 8);
                            LbMaxPow2.Font = new Font("Times New Roman", 8);
                            LbMaxPow1.Font = new Font("Times New Roman", 8);
                            LbMaxPow0.Font = new Font("Times New Roman", 8);

                            LbMaxPow5.Text = Common_frm1.LaserMaxPow.ToString() + "mW";
                            LbMaxPow4.Text = (float.Parse(TbMaxPow0.Text) + number_gap * 4).ToString() + "mW";
                            LbMaxPow3.Text = (float.Parse(TbMaxPow0.Text) + number_gap * 3).ToString() + "mW";
                            LbMaxPow2.Text = (float.Parse(TbMaxPow0.Text) + number_gap * 2).ToString() + "mW";
                            LbMaxPow1.Text = (float.Parse(TbMaxPow0.Text) + number_gap * 1).ToString() + "mW";
                            LbMaxPow0.Text = (float.Parse(TbMaxPow0.Text)).ToString() + "mW";
                        }
                        else
                        {
                            MessageBox.Show("Please enter correct number");
                        }
                    }
                    else
                    {
                        MessageBox.Show("port is closed");
                    }
                }
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Please enter a number");
            }


        }

        private void LbMaxPow0_Click(object sender, EventArgs e)
        {
            TbMaxPow0.Show();
        }

        private void TbarPowerSet_Scroll(object sender, EventArgs e)
        {
            try
            {
                TbPowSet.Text = (TbarPowerSet.Value * (Common_frm1.LaserMaxPow - Common_frm1.LaserMinPow) * 0.01 + Common_frm1.LaserMinPow).ToString();
            }
            catch (Exception ex)
            {
                TbPowSet.Text = "0";
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void TbMaxPow5_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public static class Common_frm1
    {
        public static float current_parm { get; set; }
        public static float laser1_on_flag { get; set; }
        public static float laser1_off_flag { get; set; }
        public static float LaserMaxPow { get; set; }
        public static float LaserMinPow { get; set; }
    }
}
