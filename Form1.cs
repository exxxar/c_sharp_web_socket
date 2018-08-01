using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace web_socket
{
    public partial class Form1 : Form
    {

        CryptoWebSocket cws = new CryptoWebSocket();
        public Form1()
        {

            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => cws.run());
            Task.Run(() => doit());

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.Text = this.cws.ca.head != null ? this.cws.ca.head._event : "empty text";
        }

        public async void doit()
        {
            var speed = 1000;
          

            int i = 1;
            while (true)
            {
                this.BeginInvoke(new Action(delegate ()
                {
                    speed = this.trackBar1.Value * 1000;
                    

                    this.dataGridView1.Rows.Clear();
                    this.dataGridView1.Refresh();

                    if (this.cws.ca.tickers_dataList != null)
                        this.cws.ca.tickers_dataList.ToList().ForEach(el =>
                        {
                            string[] some_filter = textBox1.Text.Split(' ');
                            bool flag = some_filter.Length == 0;

                            foreach (var s in some_filter)
                            {
                                if (!checkBox1.Checked)
                                {
                                    if (el.Value.name.ToLower().Trim().IndexOf(s) != -1)
                                        flag = true;

                                }
                                else
                                    flag &= el.Value.name.ToLower().Trim().IndexOf(s) != -1;
                                
                            }


                            if (flag)
                            {
                                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                                row.Cells[0].Value = this.cws.ca.head != null ? el.Value.id : "empty text";
                                row.Cells[1].Value = this.cws.ca.head != null ? el.Value.name : "empty text";
                                row.Cells[2].Value = this.cws.ca.head != null ? el.Value.low : "empty text";
                                row.Cells[3].Value = this.cws.ca.head != null ? el.Value.high : "empty text";
                                row.Cells[4].Value = this.cws.ca.head != null ? el.Value.last : "empty text";
                                row.Cells[5].Value = this.cws.ca.head != null ? el.Value.open : "empty text";
                                row.Cells[6].Value = this.cws.ca.head != null ? el.Value.sell : "empty text";
                                row.Cells[7].Value = this.cws.ca.head != null ? el.Value.volume : "empty text";
                                row.Cells[8].Value = this.cws.ca.head != null ? el.Value.quote_unit : "empty text";
                                this.dataGridView1.Rows.Add(row);
                            }
                        });
                   
                }));
                Thread.Sleep(speed);
                i++;
            }

        }

        private void propertyGrid1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
