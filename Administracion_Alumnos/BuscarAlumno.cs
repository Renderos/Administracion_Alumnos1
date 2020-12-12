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
            string sql = $"select mat.id, mat.nombre, ins.ciclo " +
                         $"from cursa ins, materia mat, alumno est ";
            DataTable dt = ConnectionDB.ExecuteQuery(sql);

            AVL arbol = new AVL();
            foreach (DataRow fila in dt.Rows)
            {
                Registro rg = new Registro();
                rg.id = Convert.ToInt32(fila[0].ToString());
                rg.nombre = fila[1].ToString();
                rg.ciclo = fila[2].ToString();

                Console.WriteLine(rg.id);
                Console.WriteLine(rg.nombre);
                Console.WriteLine(rg.ciclo);

                arbol.Insertar(rg, arbol);
                
            }
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
