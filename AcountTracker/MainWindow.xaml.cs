using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Input;

namespace AcountTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Signup signupwindow = new Signup();
        static readonly string datasource = "Data Source = C:\\Users\\Dondi\\source\\repos\\AcountTracker\\AcountTracker\\bin\\Debug\\LoginDb.db";
        readonly SQLiteConnection conn = new SQLiteConnection(datasource);
         public MainWindow()
        {
            InitializeComponent();
        
        }
        public void EnsableSignup()
        {
            Signup.IsEnabled = true;
        }
     
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (UserExsits())
            {
                WelcomeScreen welcome = new WelcomeScreen();
                welcome.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
            conn.Close();
        }


        private bool UserExsits()
        {
            conn.Open();
            SQLiteCommand cmd = new SQLiteCommand("Select * from UserAccounts", conn);
            //Check if user exists in SQLite Database
            SQLiteDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Username"] + " " + Username.Text.ToString());
                    if (reader["username"].ToString() == Username.Text.ToString() && reader["password"].ToString() == Password.Text.ToString())
                    {
                        return true;
                    }
                }
            }

            return false;

        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {          
            signupwindow.Show();
                  
            //if signup returns true, disable the button, otherwise, don't disable the button

        }
      
        //return key implimentation 
        private void Username_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
           
                if (e.Key == Key.Return)
                {
                    Password.Focus();
                }
            }

        
        private void Password_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
        if (e.Key == Key.Return)
        {
                Login.Focus();
            
        }
    }
        private void Login_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return) 
            {
                Login_Click( sender ,  e);
            }
        }
    }
}




