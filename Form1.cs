using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeeWorkers
{
    public partial class Form1 : Form
    {
        Queen queen;
        public Form1()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0; 

            Worker[] workers = new Worker[4]; 
            workers[0] = new Worker
                (new string[] { "Nectar collector", "Honey manufacturing" }); 
            workers[1] = new Worker
                (new string[] { "Egg care", "Baby bee tutoring" }); 
            workers[2] = new Worker
                (new string[] { "Hive maintenance", "Sting patrol" }); 
            workers[3] = new Worker
                (new string[] { "Nectar collector", "Honey manufacturing", "Egg care", "Baby bee tutoring", "Hive maintenance", "Sting patrol" }); 
            queen = new Queen(workers);

            textBox1.Text = "START \r\n";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (queen.AssignWork(comboBox1.SelectedItem.ToString(), (int)numericUpDown1.Value))
                textBox1.Text += "pchelka naidena \r\n";
            else
                textBox1.Text += "pchelka NE naidena \r\n";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += queen.WorkTheNextShift();

        }
    }
}
