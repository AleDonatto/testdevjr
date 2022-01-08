using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string url = "https://jsonplaceholder.typicode.com/users";
        public Form1()
        {
            InitializeComponent();
        }

        private async void  Form1_Load(object sender, EventArgs e)
        {
            string data = await getUsers();
            List<Usuarios> list = JsonConvert.DeserializeObject<List<Usuarios>>(data);
            this.dataGridView1.DataSource = list;
        }

        public async Task<string> getUsers() {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader streamreader = new StreamReader(response.GetResponseStream());

            return await streamreader.ReadToEndAsync();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtNombre.Text = (string)dataGridView1.Rows[e.RowIndex].Cells[1].Value;
            txtUserName.Text = (string)dataGridView1.Rows[e.RowIndex].Cells[2].Value;
            txtEmail.Text = (string)dataGridView1.Rows[e.RowIndex].Cells[3].Value;
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("seleccione un usuario para ver sus post","ADVERTENCIA",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                int idUsuario = int.Parse(txtId.Text);
                PostUser postUser = new PostUser(idUsuario);
                postUser.ShowDialog();
            }
        }

        private void btnTodos_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("seleccione un usuario para ver sus Todos", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int idUsuario = int.Parse(txtId.Text);
                TodosUser postUser = new TodosUser(idUsuario);
                postUser.ShowDialog();
            }
        }
    }
}
