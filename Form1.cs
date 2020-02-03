using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace testefirebase
{
    public partial class Form1 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "X30A726Yojr4KcC6fyHubhnHgOqIMZXNytwaQ7bq",
            BasePath = "https://testerecuperacao-590b8.firebaseio.com/"
        };

        IFirebaseClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);

            if(client!=null)
            {
                MessageBox.Show("Conectado ao banco");
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                Nome = textBox1.Text,
                Categoria = textBox2.Text,
                Rendimento = textBox3.Text,
                TempoPreparo = textBox4.Text,
                Ingredientes = textBox5.Text,
                ModoPreparo = textBox6.Text
            };

            SetResponse response = await client.SetTaskAsync("Information/" + textBox1.Text, data);
            Data result = response.ResultAs<Data>();

            MessageBox.Show("Receita Cadastrada!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetTaskAsync("Information/" + textBox1.Text);
            Data obj = response.ResultAs<Data>();

            textBox1.Text = obj.Nome;
            textBox2.Text = obj.Categoria;
            textBox3.Text = obj.Rendimento;
            textBox4.Text = obj.TempoPreparo;
            textBox5.Text = obj.Ingredientes;
            textBox6.Text = obj.ModoPreparo;

            MessageBox.Show("Busca realizada!");
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var data = new Data
            {
                Nome = textBox1.Text,
                Categoria = textBox2.Text,
                Rendimento = textBox3.Text,
                TempoPreparo = textBox4.Text,
                Ingredientes = textBox5.Text,
                ModoPreparo = textBox6.Text
            };


            FirebaseResponse response = await client.UpdateTaskAsync("Information/" + textBox1.Text, data);
            Data result = response.ResultAs<Data>();

            MessageBox.Show("Atualização concluida!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.DeleteTaskAsync("Information/" + textBox1.Text);

            MessageBox.Show("Receita excluida com sucesso!");

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
