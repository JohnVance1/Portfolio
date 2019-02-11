using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ValueEditorTool
{
    public partial class Form1 : Form
    {
        // Fields
        string filename;

        // Constructor
        public Form1()
        {
            InitializeComponent();
            filename = "../../../../infoFile.txt";
        }
        
        // Methods
        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter output = null;

            try
            {
                output = new StreamWriter(filename);

                // Player info
                output.WriteLine(PlayerHealthValue.Value);
                output.WriteLine(playerAttackValue.Value);

                if(radioButton2.Checked)
                {
                    output.WriteLine(1000);
                }
                else if (radioButton3.Checked)
                {
                    output.WriteLine(500);
                }
                else
                {
                    output.WriteLine(-1);
                }

                // Zombie info
                output.WriteLine(zombieHealthValue.Value);
                output.WriteLine(zombieAttackValue.Value);
                output.WriteLine(zombieCount.Value);

                // Boss info
                output.WriteLine(bossHealthValue.Value);
                output.WriteLine(bossAttackValue.Value);
            }
            catch(Exception exception)
            {
                Console.WriteLine("Error saving: " + exception.Message);
            }
            finally
            {
                if (output != null)
                    output.Close();
            }
        }
    }
}
