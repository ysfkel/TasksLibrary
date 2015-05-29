using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Task t = new Task(() =>
            {
                string data = "";
                data = getData();
             
            });

            Task t2=t.ContinueWith((previousTask)=>{
               listBox1.Items.Insert(0, "data");
            },TaskScheduler.FromCurrentSynchronizationContext());

            t.Start();
        }
        int requestNumber = 0;
        private string getData()
        {
            const int upperLimit = 20000000;
            string temp = "";
            for (int i = 0; i < upperLimit; i++)
            {
                temp = i.ToString();
            }
            requestNumber++;
            return string.Format("Request number:{0} -datetime:{1}", requestNumber, DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongDateString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
