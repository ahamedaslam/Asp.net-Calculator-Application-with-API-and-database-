using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace calculator1
{
    public partial class Form1 : Form
    {
        int value1;
        int value2;
        Double result = 0;
        private string sign;

        public Form1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            textBox1.Text = textBox1.Text + button.Text;
        }

        private void buttonplus_Click(object sender, EventArgs e)
        {
            value1 = Convert.ToInt32(textBox1.Text);
            sign = "+";
            label1.Text = textBox1.Text;
            label3.Text = "+";
            textBox1.Text = "";
        }

        private void buttonminus_click(object sender, EventArgs e)
        {
            value1 = Convert.ToInt32(textBox1.Text);
            sign = "-";
            label1.Text = textBox1.Text;
            label3.Text = "-";
            textBox1.Text = "";
        }

        private void buttonmul_Click(object sender, EventArgs e)
        {
            value1 = Convert.ToInt32(textBox1.Text);
            sign = "*";
            label1.Text = textBox1.Text;
            label3.Text = "*";
            textBox1.Text = "";
        }

        private void buttondiv_Click(object sender, EventArgs e)
        {
            value1 = Convert.ToInt32(textBox1.Text);
            sign = "/";
            label1.Text = textBox1.Text;
            label3.Text = "/";
            textBox1.Text = "";
        }

        private async void buttonequalto_Click(object sender, EventArgs e)
        {
            value2 = Convert.ToInt32(textBox1.Text);
            label2.Text = textBox1.Text;

            if (sign == "+")
            {
                int result = await CallApiAdd(value1, value2);
                textBox1.Text = Convert.ToString(result);
                MessageBox.Show("Api called successfully..!!");
            }
            else if (sign == "-")
            {
                int result = await CallApiSub(value1, value2);
                textBox1.Text = Convert.ToString(result);
                MessageBox.Show("Api called successfully..!!");
            }
            else if (sign == "*")
            {
                int result = await CallApiMul(value1, value2);
                textBox1.Text = Convert.ToString(result);
                MessageBox.Show("Api called successfully..!!");
            }
            else if (sign == "/")
            {
                int result = await CallApidiv(value1, value2);
                textBox1.Text = Convert.ToString(result);
                MessageBox.Show("Api called successfully..!!");
            }
        }

        private async Task<int> CallApiAdd(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/addition/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result");
            }
        }

        private async Task<int> CallApiSub(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/subtraction/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result1");
            }
        }

        private async Task<int> CallApiMul(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/multiplication/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result2");
            }
        }

        private async Task<int> CallApidiv(int value1, int value2)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7086/api/Calculator/division/{value1}/{value2}");
                var content = await response.Content.ReadAsStringAsync();
                var resultObject = JObject.Parse(content);
                return resultObject.Value<int>("result3");
            }
        }

        private void data_save_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=osmaniauniversity.database.windows.net;Initial Catalog=sandbox;User ID=khalid;Password=Osmaniauniversity*$321");
            SqlCommand cmd = new SqlCommand("INSERT INTO table1_calc (value_1, operator, value_2, total) VALUES (@value1, @sign, @value2, @total)", con);
            cmd.Parameters.AddWithValue("@value1", value1);
            cmd.Parameters.AddWithValue("@sign", sign);
            cmd.Parameters.AddWithValue("@value2", value2);
            cmd.Parameters.AddWithValue("@total", textBox1.Text);

            try
            {
                con.Open();
                int i = cmd.ExecuteNonQuery();
                if (i != 0)
                {
                    MessageBox.Show("Data Saved");
                }
                else
                {
                    MessageBox.Show("Error..!!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
