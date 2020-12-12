using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administracion_Alumnos
{
    public partial class Incripcion : UserControl
    {
        public Incripcion()
        {
            InitializeComponent();
            var dt = ConnectionDB.ExecuteQuery($"select * from materia");
            var assignaturesCombo = new List<string>();

            foreach (DataRow dr in dt.Rows)
            {
                assignaturesCombo.Add(dr[1].ToString());
            }

            comboBox1.DataSource = assignaturesCombo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals(""))
            {
                MessageBox.Show("No se pueden dejar campos vacios");
            }
            else
            {
                try
                {
                    string query = $"select id from materia where nombre = '{comboBox1.SelectedItem.ToString()}'";

                    var dt = ConnectionDB.ExecuteQuery(query);
                    var dr = dt.Rows[0];
                    var idMateria = Convert.ToInt32(dr[0].ToString());

                    string nonQuery = $"insert into cursa(carnet, id, ciclo) values(" +
                                      $"'{textBox1.Text}'," +
                                      $"{idMateria}," +
                                      $"'{textBox2.Text}')";

                    ConnectionDB.ExecuteNonQuery(nonQuery);

                    MessageBox.Show("Se ha inscrito la materia");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }
    }
}
