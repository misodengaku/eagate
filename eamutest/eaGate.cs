using I18N.CJK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eamutest
{
    public class eaGate
    {
        CookieContainer cookieContainer = new CookieContainer();
        string id, pass, otp;

        public eaGate(string id, string pass, string otp = "")
        {
            this.id = id;
            this.pass = pass;
            this.otp = otp;
        }

        public async Task<string> Login()
        {
            var cp932 = new CP932();
            var uri = new Uri("https://p.eagate.573.jp/gate/p/login.html");
            
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = uri })
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("KID", id),
                    new KeyValuePair<string, string>("pass", pass),
                    new KeyValuePair<string, string>("OTP", otp),
                });
                var response = await client.GetByteArrayAsync(uri);
                var responseString = cp932.GetString(response, 0, response.Length - 1);

                var result = await client.PostAsync(uri, content);
                var resultByte = await result.Content.ReadAsByteArrayAsync();
                responseString = cp932.GetString(resultByte, 0, resultByte.Length - 1);

                //responseString = Encoding.GetEncoding(932).GetString(result.Content, 0, result.Content.Length - 1);
                return responseString;
            }
        }
    }
}
