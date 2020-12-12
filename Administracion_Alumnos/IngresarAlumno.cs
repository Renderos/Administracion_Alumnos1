using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Administracion_Alumnos
{
    public partial class IngresarAlumno : UserControl
    {
        public IngresarAlumno()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                ConnectionDB.ExecuteNonQuery($"insert into alumno(carnet, nombres, apellidos, telefono, correo) values (" +
                                         $"'{textBox5.Text}'," +
                                         $"'{textBox1.Text}'," +
                                         $"'{textBox2.Text}'," +
                                         $"'{textBox4.Text}'," +
                                         $"'{textBox3.Text}')");

                MessageBox.Show("Alumno insertado satisfactoriamente");

            }
            catch (Exception ex)
            {
                // Mostrar cualquier error
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
