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

namespace TaskThreadAoo
{
    public partial class Form1 : Form
    {
        public static int Counter { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var aTask = Go(progressBar1);
            var bTask = Go(progressBar2);

            await Task.WhenAll(aTask, bTask);
        }

        public async Task Go(ProgressBar pb)
        {

            await Task.Run(() => // Run() => ASYNC metotlar illaki thread kullanmaz. kullanacağı durumlarda olur kullanmayacağı durumlarda olur.
                                 // Buradaki asıl mevzu async metodu çağırdığım yerde o an ki thread'i bloklamaması.
                                 // Peki biz bazı kodlarımızı ayrı thread üzerinde çalıştırmak istersek napacağız?
                                 // Burada Run() metodu devreye giriyor. Run metodu içerisine yazmış olduğumuz kodlar tamamen ayrı bir thread üzerinde çalışır.
                                 // Yani bizzar bu kodların ayrı bir thread üzer,nde çalışması gerektiğini programıma bildirmiş oluyorum.
            {
                Enumerable.Range(1, 100).ToList().ForEach(x =>
                {
                    Thread.Sleep(40);
                    pb.Invoke((MethodInvoker) delegate { pb.Value = x; });
                });
            });



        }

        private void btnCounter_Click(object sender, EventArgs e)
        {
            btnCounter.Text = Counter++.ToString();
        }
    }
}
