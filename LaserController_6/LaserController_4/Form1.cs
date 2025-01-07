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

            TbMaxPow0_2.Hide();
            TbMaxPow0_2.KeyPress += new KeyPressEventHandler(TbMaxPow0_2_Press);
            TbMaxPow5_2.Hide();
            TbMaxPow5_2.KeyPress += new KeyPressEventHandler(TbMaxPow5_2_Press);

            TbMaxPow0_3.Hide();
            TbMaxPow0_3.KeyPress += new KeyPressEventHandler(TbMaxPow0_3_Press);
            TbMaxPow5_3.Hide();
            TbMaxPow5_3.KeyPress += new KeyPressEventHandler(TbMaxPow5_3_Press);

            TbMaxPow0_4.Hide();
            TbMaxPow0_4.KeyPress += new KeyPressEventHandler(TbMaxPow0_4_Press);
            TbMaxPow5_4.Hide();
            TbMaxPow5_4.KeyPress += new KeyPressEventHandler(TbMaxPow5_4_Press);

            this.MaximizeBox = false;
        }

        public string LaserPow_frm1
        {
            set { this.TbLaserPow.Text = value; }
            get { return this.TbLaserPow.Text; }
        }
        public string LaserPow2_frm1
        {
            set { this.TbLaserPow2.Text = value; }
            get { return this.TbLaserPow2.Text; }
        }
        public string LaserPow3_frm1
        {
            set { this.TbLaserPow3.Text = value; }
            get { return this.TbLaserPow3.Text; }
        }
        public string LaserPow4_frm1
        {
            set { this.TbLaserPow4.Text = value; }
            get { return this.TbLaserPow4.Text; }
        }

        private void BtAdvance_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Owner = this;
            frm2.ShowDialog();                   
        }
        private void BtAdvance2_Click(object sender, EventArgs e)
        {
            laser2_form2 laser2_frm2 = new laser2_form2();
            laser2_frm2.Owner = this;
            laser2_frm2.ShowDialog();
        }
        private void BtAdvance3_Click(object sender, EventArgs e)
        {
            laser3_form2 laser3_frm2 = new laser3_form2();
            laser3_frm2.Owner = this;
            laser3_frm2.ShowDialog();
        }
        private void BtAdvance4_Click(object sender, EventArgs e)
        {
            laser4_form2 laser4_frm2 = new laser4_form2();
            laser4_frm2.Owner = this;
            laser4_frm2.ShowDialog();
        }

        private void BtLaser1On_Click(object sender, EventArgs e)
        {
            try 
            {
                Form2.serialPort1.Write("{lason}\n");
                //Form2.Btcalibrate.Enabled = true;
                Btpowerset.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }
        private void BtLaser2On_Click(object sender, EventArgs e)
        {
            try
            {
                laser2_form2.serialport2.Write("{lason}\n");
                //Form2.Btcalibrate.Enabled = true;
                Btpowerset2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void BtLaser3On_Click(object sender, EventArgs e)
        {
            try
            {
                laser3_form2.serialport3.Write("{lason}\n");
                //Form2.Btcalibrate.Enabled = true;
                Btpowerset3.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void BtLaser4On_Click(object sender, EventArgs e)
        {
            try
            {
                laser4_form2.serialport4.Write("{lason}\n");
                //Form2.Btcalibrate.Enabled = true;
                Btpowerset4.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtLaser1Off_Click(object sender, EventArgs e)
        {
            try
            {
                Form2.serialPort1.Write("{lasoff}\n");
                //Btcalibrate.Enabled = false;
                Btpowerset.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void BtLaser2Off_Click(object sender, EventArgs e)
        {
            try
            {
                laser2_form2.serialport2.Write("{lasoff}\n");
                //Btcalibrate.Enabled = false;
                Btpowerset2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void BtLaser3Off_Click(object sender, EventArgs e)
        {
            try
            {
                laser3_form2.serialport3.Write("{lasoff}\n");
                //Btcalibrate.Enabled = false;
                Btpowerset3.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void BtLaser4Off_Click(object sender, EventArgs e)
        {
            try
            {
                laser4_form2.serialport4.Write("{lasoff}\n");
                //Btcalibrate.Enabled = false;
                Btpowerset4.Enabled = false;
            }
            catch (Exception ex)
            {
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
        private void Btpowerset2_Click(object sender, EventArgs e)
        {
            try
            {
                laser2_form2.serialport2.Write("{setpower ");
                laser2_form2.serialport2.Write(TbPowSet2.Text);
                TbarPowerSet2.Value = Convert.ToInt32(((float.Parse(TbPowSet2.Text) - Common_frm1.LaserMinPow2) / (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2)) * 100);
                laser2_form2.serialport2.Write("}\n");
                MessageBox.Show("Power Set Done!");
            }
            catch (Exception ex)
            {
                TbPowSet.Text = "0";
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Btpowerset3_Click(object sender, EventArgs e)
        {
            try
            {
                laser3_form2.serialport3.Write("{setpower ");
                laser3_form2.serialport3.Write(TbPowSet3.Text);
                TbarPowerSet3.Value = Convert.ToInt32(((float.Parse(TbPowSet3.Text) - Common_frm1.LaserMinPow3) / (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3)) * 100);
                laser3_form2.serialport3.Write("}\n");
                MessageBox.Show("Power Set Done!");
            }
            catch (Exception ex)
            {
                TbPowSet.Text = "0";
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void Btpowerset4_Click(object sender, EventArgs e)
        {
            try
            {
                laser4_form2.serialport4.Write("{setpower ");
                laser4_form2.serialport4.Write(TbPowSet4.Text);
                TbarPowerSet4.Value = Convert.ToInt32(((float.Parse(TbPowSet4.Text) - Common_frm1.LaserMinPow4) / (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4)) * 100);
                laser4_form2.serialport4.Write("}\n");
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
        private void LbMaxPow5_2_Click_1(object sender, EventArgs e)
        {
            TbMaxPow5_2.Show();
        }
        private void LbMaxPow5_3_Click(object sender, EventArgs e)
        {
            TbMaxPow5_3.Show();
        }
        private void LbMaxPow5_4_Click(object sender, EventArgs e)
        {
            TbMaxPow5_4.Show();
        }

        private void LbMaxPow0_Click(object sender, EventArgs e)
        {
            TbMaxPow0.Show();
        }
        private void LbMaxPow0_2_Click(object sender, EventArgs e)
        {
            TbMaxPow0_2.Show();
        }
        private void LbMaxPow0_3_Click(object sender, EventArgs e)
        {
            TbMaxPow0_3.Show();
        }
        private void LbMaxPow0_4_Click(object sender, EventArgs e)
        {
            TbMaxPow0_4.Show();
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
        public void TbMaxPow5_2_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow5_2.Hide();
                    Common_frm1.LaserMaxPow2 = float.Parse(TbMaxPow5_2.Text);

                    laser2_form2.serialport2.Write("{setpowerMax ");
                    laser2_form2.serialport2.Write(TbMaxPow5_2.Text);
                    laser2_form2.serialport2.Write("}\n");
                    MessageBox.Show("Maximum Power Set Done!");

                    LbMaxPow5_2.Font = new Font("Times New Roman", 8);
                    LbMaxPow4_2.Font = new Font("Times New Roman", 8);
                    LbMaxPow3_2.Font = new Font("Times New Roman", 8);
                    LbMaxPow2_2.Font = new Font("Times New Roman", 8);
                    LbMaxPow1_2.Font = new Font("Times New Roman", 8);
                    LbMaxPow0_2.Font = new Font("Times New Roman", 8);

                    LbMaxPow5_2.Text = Common_frm1.LaserMaxPow2.ToString() + "mW";
                    LbMaxPow4_2.Text = (Common_frm1.LaserMaxPow2 * 0.8).ToString() + "mW";
                    LbMaxPow3_2.Text = (Common_frm1.LaserMaxPow2 * 0.6).ToString() + "mW";
                    LbMaxPow2_2.Text = (Common_frm1.LaserMaxPow2 * 0.4).ToString() + "mW";
                    LbMaxPow1_2.Text = (Common_frm1.LaserMaxPow2 * 0.2).ToString() + "mW";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Please enter a number");
            }
        }
        public void TbMaxPow5_3_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow5_3.Hide();
                    Common_frm1.LaserMaxPow3 = float.Parse(TbMaxPow5_3.Text);

                    laser3_form2.serialport3.Write("{setpowerMax ");
                    laser3_form2.serialport3.Write(TbMaxPow5_3.Text);
                    laser3_form2.serialport3.Write("}\n");
                    MessageBox.Show("Maximum Power Set Done!");

                    LbMaxPow5_3.Font = new Font("Times New Roman", 8);
                    LbMaxPow4_3.Font = new Font("Times New Roman", 8);
                    LbMaxPow3_3.Font = new Font("Times New Roman", 8);
                    LbMaxPow2_3.Font = new Font("Times New Roman", 8);
                    LbMaxPow1_3.Font = new Font("Times New Roman", 8);
                    LbMaxPow0_3.Font = new Font("Times New Roman", 8);

                    LbMaxPow5_3.Text = Common_frm1.LaserMaxPow3.ToString() + "mW";
                    LbMaxPow4_3.Text = (Common_frm1.LaserMaxPow3 * 0.8).ToString() + "mW";
                    LbMaxPow3_3.Text = (Common_frm1.LaserMaxPow3 * 0.6).ToString() + "mW";
                    LbMaxPow2_3.Text = (Common_frm1.LaserMaxPow3 * 0.4).ToString() + "mW";
                    LbMaxPow1_3.Text = (Common_frm1.LaserMaxPow3 * 0.2).ToString() + "mW";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Please enter a number");
            }
        }
        public void TbMaxPow5_4_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow5_4.Hide();
                    Common_frm1.LaserMaxPow4 = float.Parse(TbMaxPow5_4.Text);

                    laser4_form2.serialport4.Write("{setpowerMax ");
                    laser4_form2.serialport4.Write(TbMaxPow5_4.Text);
                    laser4_form2.serialport4.Write("}\n");
                    MessageBox.Show("Maximum Power Set Done!");

                    LbMaxPow5_4.Font = new Font("Times New Roman", 8);
                    LbMaxPow4_4.Font = new Font("Times New Roman", 8);
                    LbMaxPow3_4.Font = new Font("Times New Roman", 8);
                    LbMaxPow2_4.Font = new Font("Times New Roman", 8);
                    LbMaxPow1_4.Font = new Font("Times New Roman", 8);
                    LbMaxPow0_4.Font = new Font("Times New Roman", 8);

                    LbMaxPow5_4.Text = Common_frm1.LaserMaxPow4.ToString() + "mW";
                    LbMaxPow4_4.Text = (Common_frm1.LaserMaxPow4 * 0.8).ToString() + "mW";
                    LbMaxPow3_4.Text = (Common_frm1.LaserMaxPow4 * 0.6).ToString() + "mW";
                    LbMaxPow2_4.Text = (Common_frm1.LaserMaxPow4 * 0.4).ToString() + "mW";
                    LbMaxPow1_4.Text = (Common_frm1.LaserMaxPow4 * 0.2).ToString() + "mW";
                }
            }
            catch (System.Exception ex)
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
        public void TbMaxPow0_2_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow0_2.Hide();
                    if (laser2_form2.serialport2.IsOpen)
                    {
                        if (float.Parse(TbMaxPow0_2.Text) != 0 && float.Parse(TbMaxPow0_2.Text) < Common_frm1.LaserMaxPow2)
                        {

                            Common_frm1.LaserMinPow2 = float.Parse(TbMaxPow0_2.Text);
                            float number_gap = (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2) / 5;

                            LbMaxPow5_2.Font = new Font("Times New Roman", 8);
                            LbMaxPow4_2.Font = new Font("Times New Roman", 8);
                            LbMaxPow3_2.Font = new Font("Times New Roman", 8);
                            LbMaxPow2_2.Font = new Font("Times New Roman", 8);
                            LbMaxPow1_2.Font = new Font("Times New Roman", 8);
                            LbMaxPow0_2.Font = new Font("Times New Roman", 8);

                            LbMaxPow5_2.Text = Common_frm1.LaserMaxPow2.ToString() + "mW";
                            LbMaxPow4_2.Text = (float.Parse(TbMaxPow0_2.Text) + number_gap * 4).ToString() + "mW";
                            LbMaxPow3_2.Text = (float.Parse(TbMaxPow0_2.Text) + number_gap * 3).ToString() + "mW";
                            LbMaxPow2_2.Text = (float.Parse(TbMaxPow0_2.Text) + number_gap * 2).ToString() + "mW";
                            LbMaxPow1_2.Text = (float.Parse(TbMaxPow0_2.Text) + number_gap * 1).ToString() + "mW";
                            LbMaxPow0_2.Text = (float.Parse(TbMaxPow0_2.Text)).ToString() + "mW";
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
        public void TbMaxPow0_3_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow0_3.Hide();
                    if (laser3_form2.serialport3.IsOpen)
                    {
                        if (float.Parse(TbMaxPow0_3.Text) != 0 && float.Parse(TbMaxPow0_3.Text) < Common_frm1.LaserMaxPow3)
                        {

                            Common_frm1.LaserMinPow3 = float.Parse(TbMaxPow0_3.Text);
                            float number_gap = (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3) / 5;

                            LbMaxPow5_3.Font = new Font("Times New Roman", 8);
                            LbMaxPow4_3.Font = new Font("Times New Roman", 8);
                            LbMaxPow3_3.Font = new Font("Times New Roman", 8);
                            LbMaxPow2_3.Font = new Font("Times New Roman", 8);
                            LbMaxPow1_3.Font = new Font("Times New Roman", 8);
                            LbMaxPow0_3.Font = new Font("Times New Roman", 8);

                            LbMaxPow5_3.Text = Common_frm1.LaserMaxPow3.ToString() + "mW";
                            LbMaxPow4_3.Text = (float.Parse(TbMaxPow0_3.Text) + number_gap * 4).ToString() + "mW";
                            LbMaxPow3_3.Text = (float.Parse(TbMaxPow0_3.Text) + number_gap * 3).ToString() + "mW";
                            LbMaxPow2_3.Text = (float.Parse(TbMaxPow0_3.Text) + number_gap * 2).ToString() + "mW";
                            LbMaxPow1_3.Text = (float.Parse(TbMaxPow0_3.Text) + number_gap * 1).ToString() + "mW";
                            LbMaxPow0_3.Text = (float.Parse(TbMaxPow0_3.Text)).ToString() + "mW";
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
        public void TbMaxPow0_4_Press(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar.Equals('\r'))
                {
                    TbMaxPow0_4.Hide();
                    if (laser4_form2.serialport4.IsOpen)
                    {
                        if (float.Parse(TbMaxPow0_4.Text) != 0 && float.Parse(TbMaxPow0_4.Text) < Common_frm1.LaserMaxPow4)
                        {

                            Common_frm1.LaserMinPow4 = float.Parse(TbMaxPow0_4.Text);
                            float number_gap = (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4) / 5;

                            LbMaxPow5_4.Font = new Font("Times New Roman", 8);
                            LbMaxPow4_4.Font = new Font("Times New Roman", 8);
                            LbMaxPow3_4.Font = new Font("Times New Roman", 8);
                            LbMaxPow2_4.Font = new Font("Times New Roman", 8);
                            LbMaxPow1_4.Font = new Font("Times New Roman", 8);
                            LbMaxPow0_4.Font = new Font("Times New Roman", 8);

                            LbMaxPow5_4.Text = Common_frm1.LaserMaxPow4.ToString() + "mW";
                            LbMaxPow4_4.Text = (float.Parse(TbMaxPow0_4.Text) + number_gap * 4).ToString() + "mW";
                            LbMaxPow3_4.Text = (float.Parse(TbMaxPow0_4.Text) + number_gap * 3).ToString() + "mW";
                            LbMaxPow2_4.Text = (float.Parse(TbMaxPow0_4.Text) + number_gap * 2).ToString() + "mW";
                            LbMaxPow1_4.Text = (float.Parse(TbMaxPow0_4.Text) + number_gap * 1).ToString() + "mW";
                            LbMaxPow0_4.Text = (float.Parse(TbMaxPow0_4.Text)).ToString() + "mW";
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
        private void TbarPowerSet2_Scroll(object sender, EventArgs e)
        {
            try
            {
                TbPowSet2.Text = (TbarPowerSet2.Value * (Common_frm1.LaserMaxPow2 - Common_frm1.LaserMinPow2) * 0.01 + Common_frm1.LaserMinPow2).ToString();
            }
            catch (Exception ex)
            {
                TbPowSet.Text = "0";
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void TbarPowerSet3_Scroll(object sender, EventArgs e)
        {
            try
            {
                TbPowSet3.Text = (TbarPowerSet3.Value * (Common_frm1.LaserMaxPow3 - Common_frm1.LaserMinPow3) * 0.01 + Common_frm1.LaserMinPow3).ToString();
            }
            catch (Exception ex)
            {
                TbPowSet.Text = "0";
                MessageBox.Show(ex.Message, "Error");
            }
        }
        private void TbarPowerSet4_Scroll(object sender, EventArgs e)
        {
            try
            {
                TbPowSet4.Text = (TbarPowerSet4.Value * (Common_frm1.LaserMaxPow4 - Common_frm1.LaserMinPow4) * 0.01 + Common_frm1.LaserMinPow4).ToString();
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
        private void TbMaxPow5_2_TextChanged(object sender, EventArgs e)
        {

        }
    }
    public static class Common_frm1
    {
        public static float LaserMaxPow { get; set; }
        public static float LaserMinPow { get; set; }
        public static float LaserMaxPow2 { get; set; }
        public static float LaserMinPow2 { get; set; }
        public static float LaserMaxPow3 { get; set; }
        public static float LaserMinPow3 { get; set; }
        public static float LaserMaxPow4 { get; set; }
        public static float LaserMinPow4 { get; set; }
    }
}
