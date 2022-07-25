using System.Collections.Generic;
using SafeWord;

public class Control
{
    public Control()
    {

    }

    public void Signup(StartWindow startWindow)
    {
        startWindow.Hide();
        SignupWindow signup = new SignupWindow();
        signup.Show();
    }
    public void Confirm_Signup(string email, string user, string password, SignupWindow signup)
    {
        Account account = new Account(user, password, email);
        Account.Add_Acc(account);
        StartWindow start = new StartWindow();
        start.Show();
        signup.Hide();
        return;
    }
    public bool Confirm_Login(string user, string pass)
    {
        if (Account.Login_Acc(user, pass))
        {
            return true;
        }
        return false;
    }
    public bool Add_Password(string website, string user, string password)
    {
        return Account.Acc_Add_Password(website, user, password);
    }

    public static List<string> Find_Password(string txt)
    {
        return Account.Find_Pass_Acc(txt);
    }
    public static string Find_Password_By_URL(string url)
    {
        return Account.Acc_FInd_Password(url);
    }

    public static Home home = new Home();
    public static ViewPassword viewpassword = new ViewPassword();
    public static AddPasswordWindow addpassword = new AddPasswordWindow();
}
