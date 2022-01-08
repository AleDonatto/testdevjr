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
    public partial class PostUser : Form
    {
        int userId = 0;
        public PostUser(int IdUser)
        {
            userId = IdUser;
            InitializeComponent();
        }


        public async Task<string> getPostUser() {
            string url = "https://jsonplaceholder.typicode.com/users/"+userId+"/posts";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            StreamReader streamreader = new StreamReader(response.GetResponseStream());

            return await streamreader.ReadToEndAsync();
        }

        private async void PostUser_Load(object sender, EventArgs e)
        {
            string data = await getPostUser();
            List<PostUserModel> list = JsonConvert.DeserializeObject<List<PostUserModel>>(data);
            this.dataGridView1.DataSource = list;
        }
    }
}
