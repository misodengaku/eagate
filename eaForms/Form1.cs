using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eaForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {

            var uri = new Uri("https://p.eagate.573.jp/gate/p/login.html");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = uri })
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("KID", "username"),
                    new KeyValuePair<string, string>("pass", "password"),
                    new KeyValuePair<string, string>("OTP", ""),
                });
                var response = await client.GetByteArrayAsync(uri);
                var responseString = Encoding.GetEncoding(932).GetString(response, 0, response.Length - 1);

                var result = await client.PostAsync(uri, content);
                var resultByte = await result.Content.ReadAsByteArrayAsync();
                responseString = Encoding.GetEncoding(932).GetString(resultByte, 0, resultByte.Length - 1);

                webBrowser1.DocumentText = responseString;
                //responseString = Encoding.GetEncoding(932).GetString(result.Content, 0, result.Content.Length - 1);
                return;
            }
        }
    }
}
