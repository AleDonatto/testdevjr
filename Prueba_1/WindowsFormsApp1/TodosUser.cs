using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class TodosUser : Form
    {
        int userId = 0;
        public TodosUser(int IdUsuario)
        {
            userId = IdUsuario;
            InitializeComponent();
        }

        private async void TodosUser_Load(object sender, EventArgs e)
        {
            string data = await getTodosUsuario();
            List<TodoUserModel> list = JsonConvert.DeserializeObject<List<TodoUserModel>>(data);
            //this.dataGridView1.DataSource = list;
            this.dataGridView1.DataSource = (from o in list
                           orderby o.id descending
                           select o).ToList();
        }

        public async Task<string> getTodosUsuario() {
            string url = "https://jsonplaceholder.typicode.com/users/"+userId+"/todos";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader streamreader = new StreamReader(response.GetResponseStream());

            return await streamreader.ReadToEndAsync();
        }

        public string postTodoUser() {

            string data = "";

            string url = "https://jsonplaceholder.typicode.com/posts";
            string titulo = txtTitulo.Text;
            bool completo = cbTerminado.Checked;

            TareasUsuarioModel tareas = new TareasUsuarioModel() { id=userId ,title=titulo, completed=completo };
            WebRequest request = WebRequest.Create(url);
            request.Method = "post";
            request.ContentType = "application/json; charset=UTF-8";

            using (var streamWriter = new StreamWriter(request.GetRequestStream())) {
                string json = JsonConvert.SerializeObject(tareas, Formatting.Indented);
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            WebResponse response = request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream())) {
                data = streamReader.ReadToEnd().Trim();
            }

            return data;

        }

        private void txtAgregar_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text == "")
            {
                MessageBox.Show("Agregue un Titulo", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {
                string message = postTodoUser();
                MessageBox.Show(message, "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
