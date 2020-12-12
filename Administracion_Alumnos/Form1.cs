using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion_Alumnos
{
    public partial class Form1 : Form
    {
        private UserControl current = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel3.Controls.Remove(current);
            current = new IngresarAlumno();
            tableLayoutPanel3.Controls.Add(current, 0, 0);
            tableLayoutPanel3.SetColumnSpan(current, 2);
            tableLayoutPanel3.SetRowSpan(current, 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tableLayoutPanel3.Controls.Remove(current);
            current = new Incripcion();
            tableLayoutPanel3.Controls.Add(current, 0, 0);
            tableLayoutPanel3.SetColumnSpan(current, 2);
            tableLayoutPanel3.SetRowSpan(current, 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tableLayoutPanel3.Controls.Remove(current);
            current = new EliminarAlumno();
            tableLayoutPanel3.Controls.Add(current, 0, 0);
            tableLayoutPanel3.SetColumnSpan(current, 2);
            tableLayoutPanel3.SetRowSpan(current, 2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tableLayoutPanel3.Controls.Remove(current);
            current = new BuscarAlumno();
            tableLayoutPanel3.Controls.Add(current, 0, 0);
            tableLayoutPanel3.SetColumnSpan(current, 2);
            tableLayoutPanel3.SetRowSpan(current, 2);
        }
    }
}
