using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Appolo.RifleChambers.Clerk
{
    /// <summary>
    /// Логика взаимодействия для Name.xaml
    /// </summary>
    public partial class Email : Page, IPageManagerHandler
    {
        private PageManager _pageManager;
        private String _email;
        private HttpClient _client = new HttpClient();

        [DllImport("user32.dll")]
        public static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);
        [DllImport("user32.dll")]
        public static extern long GetKeyboardLayoutName(System.Text.StringBuilder pwszKLID);
        public Email()
        {
            InitializeComponent();
            InputLanguageManager.SetInputLanguage(Email_Field, CultureInfo.CreateSpecificCulture("en"));
            LoadKeyboardLayout("00000409", 1);
        }

        public PageManager PageManager { get => _pageManager; set => _pageManager = value; }

        public void PreNavigate(NavigationToArgs args)
        {

        }

        public void AfterNavigate(NavigationToArgs args)
        {

        }

        public void AfterNavigateFrom(NavigationFromArgs args)
        {

        }

        public void PreNavigateFrom(NavigationFromArgs args)
        {

        }

        private MailMessage GetMailWithImg()
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.AlternateViews.Add(GetEmbeddedImage($"{PathHelpers.ExecutableDirectory()}/image.png"));
            mail.From = new MailAddress(Config<AppConfig>.Value.Mail);
            mail.To.Add(_email);
            mail.Subject = "Грамота";
            return mail;
        }

        private AlternateView GetEmbeddedImage(String filePath)
        {
            LinkedResource res = new LinkedResource(filePath);
            res.ContentId = Guid.NewGuid().ToString();
            string htmlBody = @"<P>Здрав будь, стрелец!</P><P>Изволь получить свою грамоту!</P><img src='cid:" + res.ContentId + @"'/><P>Прощай!</P>";

            Trace.WriteLine(htmlBody);

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }

        private void SendMessage()
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Host = "smtp.yandex.com";
                client.Port = 587; // Обратите внимание что порт 587
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Config<AppConfig>.Value.Mail, Config<AppConfig>.Value.Password); // Ваши логин и пароль

                Trace.WriteLine(Config<AppConfig>.Value.Mail);
                Trace.WriteLine(Config<AppConfig>.Value.Password);

                MailMessage mailWithImg = GetMailWithImg();
                client.Send(mailWithImg);

                _pageManager.Navigate(typeof(Wait2));
            } catch
            {
                Incorrect_Field.Text = "Некоректная почта";
            }
        }

        private void Send_Click(object sStarter, RoutedEventArgs e)
        {
            _email = Email_Field.Text.ToString();
            Trace.WriteLine(_email);
            SendMessage();
        }

        private void Exit_Button(object sStarter, RoutedEventArgs e)
        {
            _client.GetAsync($"{Config<AppConfig>.Value.SecondSensor}?available=true");
            _client.GetAsync($"{Config<AppConfig>.Value.Player}?state=0");
            _pageManager.Navigate(typeof(Start));
        }
    }
}
