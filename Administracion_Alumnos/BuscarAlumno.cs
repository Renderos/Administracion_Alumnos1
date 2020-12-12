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
    public partial class BuscarAlumno : UserControl
    {
        public BuscarAlumno()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {

                var dt = ConnectionDB.ExecuteQuery($"select mat.id, mat.nombre, ins.ciclo " +
                                                   $"from cursa ins, materia mat, alumno est " +
                                                   $"where ins.carnet = '{textBox1.Text}' " +
                                                   $"and ins.carnet = est.carnet " +
                                                   $"and ins.id = mat.id ");
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                // Mostrar cualquier excepción
                MessageBox.Show(ex.Message);
            }
            
        }
        
    }
}
