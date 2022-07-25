using System.Collections.Generic;


namespace SafeWord
{
    class Account
    {

        public Account(string user, string password, string email)
        {
            m_username = user;
            m_password = password;
            m_email = email;
            m_crypto = new Crypto();
            done = false;
        }

        private Account(Account acc)
        {
            m_username = acc.m_username;
            m_password = acc.m_password;
            m_email = acc.m_email;
            m_crypto = new Crypto(acc, acc.m_email);

        }
        private Account(string user, string pass)
        {
            m_username = user;
            m_password = pass;
            m_crypto = new Crypto();
        }

        public static void Add_Acc(Account acc)
        {
            Account temp_acc = new Account(acc);
            Crypto.Add_Acc_Entry(temp_acc.m_email, temp_acc.m_password, temp_acc.m_username);
            acc.done = true;

            return;

        }


        public string Get_User()
        {
            return m_username;
        }
        public string Get_Email()
        {
            return m_email;
        }
        public static bool Login_Acc(string user, string pass)
        {
            Account acc = new Account(user, pass);
            if (acc.m_crypto.Find_Account(acc.m_username, acc.m_password, acc.m_email))
            {
                curr_acc = acc;
                return true;
            }
            return false;
        }

        public static bool Acc_Add_Password(string website, string user, string password)
        {
            return curr_acc.m_crypto.Crypto_Add_Password(website, user, password, curr_acc);
        }

        public static List<string> Find_Pass_Acc(string txt)
        {
            return curr_acc.m_crypto.Crypto_Find_Pass(txt);
        }

        public static string Acc_FInd_Password(string url)
        {
            return curr_acc.m_crypto.Crypto_Find_Password(url);
        }

        private readonly string m_username;
        private readonly string m_password;
        private readonly string m_email;
        public bool done;
        private static List<Account> Acc_List = new List<Account>();
        private Crypto m_crypto;
        private static Account curr_acc;
    }

}
