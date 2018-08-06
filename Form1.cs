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
                    this.dataGridView2.Rows.Clear();
                    this.dataGridView2.Refresh();
                    this.dataGridView3.Rows.Clear();
                    this.dataGridView3.Refresh();
                    this.dataGridView4.Rows.Clear();
                    this.dataGridView4.Refresh();

                    if (this.cws.ca.tickers_dataList != null&& this.cws.ca.head != null)
                        this.cws.ca.tickers_dataList.ToList().ForEach(el =>
                        {
                            string[] some_filter = textBox1.Text.Split(' ');
                            bool flag = some_filter.Length == 0;

                            foreach (var s in some_filter)
                            {
                                if (!checkBox1.Checked)
                                {
                                    if (el.Value.id.ToLower().Trim().IndexOf(s) != -1)
                                        flag = true;

                                }
                                else
                                    flag &= el.Value.id.ToLower().Trim().IndexOf(s) != -1;

                            }


                            if (flag)
                            {
                                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                                row.Cells[0].Value = el.Value.id;
                                row.Cells[1].Value = el.Value.name  ;
                                row.Cells[2].Value =  el.Value.base_unit;
                                row.Cells[3].Value =  el.Value.quote_unit ;
                                row.Cells[4].Value =  el.Value.ask_fixed ;
                                row.Cells[5].Value = el.Value.bid_fixed ;
                                row.Cells[6].Value =  el.Value.low ;
                                row.Cells[7].Value =  el.Value.high ;

                                row.Cells[8].Value =  el.Value.last ;
                                row.Cells[9].Value =  el.Value.buy ;
                                row.Cells[10].Value =  el.Value.sell ;
                                row.Cells[11].Value =  el.Value.open ;
                                row.Cells[12].Value =  el.Value.change ;
                                row.Cells[13].Value =  el.Value.volume ;
                                row.Cells[14].Value = el.Value.funds;
                                row.Cells[15].Value = el.Value.at;
                                row.Cells[16].Value =(el.Value.ask_fixed / el.Value.bid_fixed -  1)*100; 
                                row.Cells[17].Value = (el.Value.last / el.Value.open - 1) * 100;
                               

                                this.dataGridView1.Rows.Add(row);

                              


                            }
                        });

                    if (this.cws.ca.at_mining_data != null)
                    {
                        //Console.WriteLine(this.cws.ca.at_mining_data.computing_power.count);
                        //Console.WriteLine(this.cws.ca.at_mining_data.computing_power.blocks);


                    }


                    if (this.cws.ca.update_data != null)
                    {
                        this.cws.ca.update_data.ToList().ForEach(ud =>
                        {
                     

                            ud.Value.asks.ToList().ForEach(asks =>
                            {
                                var key = ud.Key.Replace("-global", "").Replace("market-", " ");
                            string[] some_filter = textBox1.Text.Split(' ');
                            bool flag = some_filter.Length == 0;

                            foreach (var s in some_filter)
                            {
                                if (!checkBox1.Checked)
                                {
                                    if (key.ToLower().Trim().IndexOf(s) != -1)
                                        flag = true;

                                }
                                else
                                    flag &= key.ToLower().Trim().IndexOf(s) != -1;

                            }


                                if (flag)
                                {

                                    DataGridViewRow row = (DataGridViewRow)dataGridView4.Rows[0].Clone();
                                    row.Cells[0].Value = key;
                                    row.Cells[1].Value = asks[0];
                                    row.Cells[2].Value = asks[1];
                                    this.dataGridView4.Rows.Add(row);
                                }
                             });
                                

                        });
                    }

                    if (this.cws.ca.trades_data != null)
                        this.cws.ca.trades_data.ToList().ForEach(el =>
                        {

                            
                            el.Value.trades.ToList().ForEach(trades =>
                            {
                               
                                string[] some_filter = textBox1.Text.Split(' ');
                                bool flag = some_filter.Length == 0;

                                foreach (var s in some_filter)
                                {
                                    if (!checkBox1.Checked)
                                    {
                                        if (el.Key.Replace("-global", "").Replace("market-", " ").ToLower().Trim().IndexOf(s) != -1)
                                            flag = true;

                                    }
                                    else
                                        flag &= el.Key.Replace("-global", "").Replace("market-", " ").ToLower().Trim().IndexOf(s) != -1;

                                }


                                if (flag)
                                {
                                    
                                    DataGridViewRow row = (DataGridViewRow)dataGridView2.Rows[0].Clone();
                                    row.Cells[0].Value = el.Key.Replace("-global", "").Replace("market-", " ");
                                    row.Cells[1].Value = trades.tid;
                                    row.Cells[2].Value = trades.type;
                                    row.Cells[3].Value = trades.price;
                                    row.Cells[4].Value = trades.amount;
                                    row.Cells[5].Value = trades.date;

                                    if (el.Key.Replace("-global", "").Replace("market-", " ").ToLower().Trim()=="ethusdt")
                                    Console.WriteLine("!!! [{0}][{1}]=>{2}", 
                                        el.Key.Replace("-global", "").Replace("market-", " "),
                                         trades.type,
                                         trades.price);

                                    this.dataGridView2.Rows.Add(row);
                                }
                            });

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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Column1.Visible = toolStripMenuItem1.Checked;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Column2.Visible = toolStripMenuItem2.Checked;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Column3.Visible = toolStripMenuItem3.Checked;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Column4.Visible = toolStripMenuItem4.Checked;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Column5.Visible = toolStripMenuItem5.Checked;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Column6.Visible = toolStripMenuItem6.Checked;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Column7.Visible = toolStripMenuItem7.Checked;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Column8.Visible = toolStripMenuItem8.Checked;
        }

        private void buyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column10.Visible = buyToolStripMenuItem.Checked;
        }

        private void sellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column20.Visible = sellToolStripMenuItem.Checked;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column21.Visible = openToolStripMenuItem.Checked;
        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column22.Visible = changeToolStripMenuItem.Checked;
        }

        private void volumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column23.Visible = volumeToolStripMenuItem.Checked;
        }

        private void fundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column24.Visible = fundsToolStripMenuItem.Checked;
        }

        private void atToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column25.Visible = atToolStripMenuItem.Checked;
        }

        private void разбросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column26.Visible = разбросToolStripMenuItem.Checked;
        }

        private void обменToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column27.Visible = обменToolStripMenuItem.Checked;
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void lastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Column9.Visible = lastToolStripMenuItem.Checked;
        }
    }
}
