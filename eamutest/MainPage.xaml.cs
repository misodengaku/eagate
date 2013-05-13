using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using I18N.CJK;

namespace eamutest
{
    public partial class MainPage : PhoneApplicationPage
    {
        // コンストラクター
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var ea = new eaGate(idBox.Text, passBox.Password);

            var html = await ea.Login();
            webBrowser1.NavigateToString(html);
        }
    }
}