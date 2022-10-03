using System;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AcountTracker
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {

        static readonly string datasource = "Data Source = C:\\Users\\Dondi\\source\\repos\\AcountTracker\\AcountTracker\\bin\\Debug\\LoginDb.db";
        readonly SQLiteConnection conn = new SQLiteConnection(datasource);
        public Signup()
        {
            InitializeComponent();
            conn.Open();
        }

        public string GetText(TextBox mytextBox)
        {
            return mytextBox.Text;
        }


        private bool ConfirmPassword()
        {
            if (GetText(PasswordTextBox) == GetText(ConfirmPasswordTextBox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Signupbutton_Click(object sender, RoutedEventArgs e)

        {
            string query = "INSERT INTO UserAccounts ('username','password') VALUES (@username, @password)";

            if (ConfirmPassword()) 
            {
                //Ensures all usernames are unique
                if (UserExsits())
                {
                    MessageBox.Show("Username already exists");
                }
                else
                {

                    SQLiteCommand newAccount = new SQLiteCommand(query, conn);
                    newAccount.Parameters.AddWithValue("@Username", Usernamebox.Text);
                    newAccount.Parameters.AddWithValue("@Password", PasswordTextBox.Text);

                    int result = newAccount.ExecuteNonQuery();
                    Console.Write("rows affected" + result);
                    this.Hide();
                   
                }

            }
            else
            {
                MessageBox.Show("Passwords do not match");
            }
        }

        private bool UserExsits()
        {
           //Returns true if the username exists. Return flase if username does not exist
            SQLiteCommand myCommand = new SQLiteCommand("SELECT * FROM UserAccounts", conn);
            SQLiteDataReader reader = myCommand.ExecuteReader();

            if (reader.HasRows)
            {

                while (reader.Read())
                {

                    if (reader["username"].ToString() == Usernamebox.Text.ToString())
                    {

                        return true;
                    }

                }
            }
            return false;
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            conn.Close();
          
            this.Hide();
        }
        //Return key implimentation
        private void Signupbutton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if(UserExsits() && ConfirmPassword())
                {
                    Signupbutton_Click(sender, e);

                }
            }
        }

        private void Usernamebox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                PasswordTextBox.Focus();
            }
        }

        private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ConfirmPasswordTextBox.Focus();
            }
        }


        private void ConfirmPasswordTextBox_KeyDown(object sender, KeyEventArgs e)
        {
          
                if (e.Key == Key.Return)
                {
                  Signupbutton.Focus();
                }
            }

        }
    
    
   
}
