using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;


namespace Login_Registration_WPF_XML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private LoginRegisterManager manager;
        public MainWindow()
        {
            InitializeComponent();
            manager = new LoginRegisterManager();
        }
        private void RegisterUser(object sender, RoutedEventArgs e)
        {
            string registerUsername = UsernameTextBox.Text;
            string registerPassword = PasswordBox.Password;

            if (string.IsNullOrEmpty(registerUsername) || string.IsNullOrEmpty(registerPassword))
            {
                Debug.WriteLine("Please provide username and password");
            }
            //if the register username exists we show a message
            if (manager.DoesUserExistRegister(registerUsername))
            {
                Debug.WriteLine("Username Already Exists");
                UserExistsMessage.Text = "Username Already Exists";
                UserExistsMessage.Visibility = Visibility.Visible;
            }
            else
            {
                manager.Register(registerUsername, registerPassword);

                //hide the registration and login panel
                RegistrationLoginPanel.Visibility = Visibility.Collapsed;

                //display the registration message 
                RegistrationMessage.Text = "Registered New User " + registerUsername + " Welcome to the application";
                RegistrationMessage.Visibility = Visibility.Visible;
                UserExistsMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void LoginUser(object sender, RoutedEventArgs e)
        {
            string loginUsername = UsernameTextBox.Text;
            string loginPassword = PasswordBox.Password;
            if (string.IsNullOrEmpty(loginUsername) || string.IsNullOrEmpty(loginPassword))
            {
                Debug.WriteLine("Please provide username and password");
                UserExistsMessage.Visibility = Visibility.Visible;
                UserExistsMessage.Text = "Please provide username and password";
            }
            else
            //we login,enable and disable the correct messages
            {
                manager.Login(loginUsername, loginPassword);
                RegistrationLoginPanel.Visibility = Visibility.Collapsed;
                LoginMessage.Text = "You are now logged in " + loginUsername + " Welcome to the application";
                LoginMessage.Visibility = Visibility.Visible;
                UserExistsMessage.Visibility = Visibility.Collapsed;
            }
        }
        //we exit the application
        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}