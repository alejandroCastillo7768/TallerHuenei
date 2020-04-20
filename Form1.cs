using agregandoOperatividadFrm.Usuario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace agregandoOperatividadFrm
{
    public partial class Form1 : Form
    {

        int IDCliente = 0;
        private int n = 0;

        public object MesaggeBoxButtons { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Guardar();
            Mensaje();
            Actualizar();
            Limpiar();

        }

        private void txtApellido_TextChanged(object sender, EventArgs e)

        {
            ControlApellido();
        }

        private void dgvBuscar_Click(object sender, EventArgs e)
        {

            IDCliente = Convert.ToInt32(dgvBuscar.CurrentRow.Cells[0].Value);
            txtNombre.Text = Convert.ToString(dgvBuscar.CurrentRow.Cells[1].Value);
            txtApellido.Text = Convert.ToString(dgvBuscar.CurrentRow.Cells[2].Value);

            dtpFechaNacimiento.Value = Convert.ToDateTime(dgvBuscar.CurrentRow.Cells[3].Value);
            txtBoxDireccion.Text = Convert.ToString(dgvBuscar.CurrentRow.Cells[4].Value);

        }

        private void dgvBuscar_CellClick(object sender, DataGridViewCellEventArgs e)

        {
            int n = e.RowIndex;

            if (n != -1)
            {
                lblSeleccion.Text = (string)dgvBuscar.Rows[n].Cells[0].Value;
            }


        }

        private void Guardar()
        {
            string strNombre, strApellido, strFechaNacimiento, strDireccion;
            strNombre = txtNombre.Text;
            strApellido = txtApellido.Text;
            strFechaNacimiento = dtpFechaNacimiento.Value.Year.ToString() + '/' + dtpFechaNacimiento.Value.Month.ToString() + '/' + dtpFechaNacimiento.Value.Day.ToString();
            strDireccion = txtBoxDireccion.Text;

            SqlConnection conectar = new SqlConnection("Data Source=TOXIC2-PC\\SQLEXPRESS;Initial Catalog=Taller;Integrated Security=true;");

            string consulta = string.Format("Insert into clientes (Nombre, Apellido, Fecha_Nacimiento, Direccion) values ('{0}','{1}','{2}','{3}')",
            strNombre, strApellido, strFechaNacimiento, strDireccion);

            SqlCommand comando = new SqlCommand(consulta, conectar);

            conectar.Open();
            comando.ExecuteNonQuery();
            conectar.Close();

        }

        private void Actualizar()
        {
            List<Cliente> lista = new List<Cliente>();

            SqlConnection conectar = new SqlConnection("Data Source=TOXIC2-PC\\SQLEXPRESS;Initial Catalog=Taller;Integrated Security=true;");
            String consulta = "SELECT IdCliente, Nombre, Apellido, Fecha_Nacimiento, Direccion FROM clientes";
            SqlCommand comando = new SqlCommand(consulta, conectar);

            conectar.Open();
            SqlDataReader reader = comando.ExecuteReader();

            while (reader.Read())
            {
                Cliente pCliente = new Cliente();
                pCliente.Id = reader.GetInt32(0);
                pCliente.Nombre = reader.GetString(1);
                pCliente.Apellido = reader.GetString(2);
                pCliente.Fecha_Nac = reader.GetString(3);
                pCliente.Direccion = reader.GetString(4);

                lista.Add(pCliente);
            }
            conectar.Close();
            dgvBuscar.DataSource = lista;

        }

        private void Limpiar()
        {
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtBoxDireccion.Text = "";
            dtpFechaNacimiento.Text = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {

            Limpiar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnGuardar.Enabled = false;
            Actualizar();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
          ControlNombre();
         /*   ControlApellido();
            ControlDireccion(); */
        }

        private void ControlNombre()
        {
            if (txtNombre.Text.Trim() != string.Empty && txtNombre.Text.All(Char.IsLetter))
            {
                btnGuardar.Enabled = true;
                errorProvider1.SetError(txtNombre, "");
            }
            else
            {
                if (!(txtNombre.Text.All(Char.IsLetter)))
                {
                    errorProvider1.SetError(txtNombre, "El NOMBRE sólo puede contener letras");
                }
                else
                {
                    errorProvider1.SetError(txtNombre, "Debe llenar el campo NOMBRE");
                }
                btnGuardar.Enabled = false;
              //  txtNombre.Focus();
            }
        }

        private void ControlApellido()
        {
            if (txtApellido.Text.Trim() != string.Empty && txtApellido.Text.All(Char.IsLetter))
            {
                btnGuardar.Enabled = true;
                errorProvider1.SetError(txtApellido, "");
            }
            else
            {
                if (!(txtApellido.Text.All(Char.IsLetter)))
                {
                    errorProvider1.SetError(txtApellido, "El APELLIDO sólo puede contener letras");
                }
                else
                {
                    errorProvider1.SetError(txtApellido, "Debe llenar el campo APELLIDO");
                }
                btnGuardar.Enabled = false;
               // txtApellido.Focus();
            }
        }

        private void ControlDireccion()
        {
            if (txtBoxDireccion.Text != string.Empty)
            {
                btnGuardar.Enabled = true;
                errorProvider1.SetError(txtBoxDireccion, "");
            }
            else         
                {
                    errorProvider1.SetError(txtBoxDireccion, "Debe llenar el campo DIRECCION");
                }
                btnGuardar.Enabled = false;
              //  txtBoxDireccion.Focus();
        }

        private void Mensaje()
        {
            string mensaje = "Has sido registrado con éxito!";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(mensaje, "Aviso", buttons);

        }

        private void txtBoxDireccion_TextChanged(object sender, EventArgs e)

        {
            ControlDireccion();
        }
    }
}