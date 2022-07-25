using System;
using System.Collections.Generic;
using System.IO;


namespace SafeWord
{
    class Crypto
    {
        public Crypto()
        {
            passfile = null;

        }
        public Crypto(Account acc, string email)
        {
            dfilepath = $@"..\..\..\..\Safeword 1.0\Dependencies\" + email + ".txt";
            passfile = File.OpenWrite(dfilepath);
            passfile.Close();

        }


        public static void Add_Acc_Entry(string email, string password, string user)
        {
            accfile = File.Open($@"..\..\..\..\Safeword 1.0\Dependencies\Accounts.txt", FileMode.Append);
            string temp_string = user + '/' + password + '/' + email + '/';
            char[] temp_arr = temp_string.ToCharArray();
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < temp_arr.Length; i++)
            {
                byte temp_byte = Encrypt(temp_arr[i]);
                bytes.Add(temp_byte);
            }

            byte[] true_bytes = bytes.ToArray();
            accfile.Write(true_bytes, 0, true_bytes.Length);
            accfile.WriteByte(0x0A);
            //accfile.Write(Decrypt(true_bytes), 0, true_bytes.Length);
            accfile.Close();

        }

        public bool Find_Account(string user, string password, string email)
        {

            accfile = File.OpenRead($@"..\..\..\..\..\SafeWord\SafeWord\Dependencies\Accounts.txt");
            int[] read = new int[200];
            bool hasreached = false;
            bool hasended = false;
            string account = " ";
            int dash1 = 0;
            int dash2 = 0;
            int dash3 = 0;
            bool acc_found = false;
            int pos = 0;
            int w;
            Account acc = new Account(" ", " ", " ");
            for (int i = 0; !hasended; i++)
            {
                w = 0;
                int count = 0;
                accfile.Position = pos;
                hasreached = false;
                while (!hasreached)
                {

                    read[w] = accfile.ReadByte();
                    if (read[w] == -1)
                    {
                        hasreached = true;
                        hasended = true;
                        break;
                    }
                    if (read[w] == 10)
                    {
                        hasreached = true;
                        w++;
                        break;
                    }
                    else
                    {
                        read[w] -= 1;
                    }

                    if (read[w] == 47)
                    {
                        count++;
                    }

                    if (read[w] == 47 && count == 3)
                    {
                        char[] result = new char[w + 1];

                        for (int x = 0; x < result.Length; x++)
                        {
                            result[x] = Convert.ToChar(read[x]);
                        }
                        account = new String(result);

                    }
                    if (read[w] == 47)
                    {
                        switch (count)
                        {
                            case 1:
                                dash1 = w;
                                break;
                            case 2:
                                dash2 = w;
                                break;
                            case 3:
                                dash3 = w;
                                break;

                        }
                    }

                    w++;

                }
                pos += w;

                string l_user = account.Substring(0, dash1);
                string l_pass = account.Substring(dash1 + 1, (dash2 - (dash1 + 1)));
                string l_email = account.Substring(dash2 + 1, (dash3 - (dash2 + 1)));

                if (l_user == user && l_pass == password)
                {
                    //acc = new Account(l_user, l_pass, l_email);
                    hasended = true;
                    acc_found = true;
                    dfilepath = @"C:\Users\zanet\source\repos\SafeWord\SafeWord\Dependencies\" + l_email + ".txt";
                }

            }



            return acc_found;
        }


        public static byte Encrypt(char x)
        {
            byte b = Convert.ToByte(x);
            b += 0x001;
            return b;
        }

        public static byte[] Decrypt(byte[] x)
        {
            byte[] w = new byte[x.Length];
            for (int i = 0; i < w.Length; i++)
            {

                w[i] = (byte)(x[i] - 0x001);
            }

            return w;
        }

        private static int Decrypt_int(int x)
        {
            return x - 0x001;
        }




        public bool Crypto_Add_Password(string website, string user, string password, Account acc)
        {
            passfile = File.Open(dfilepath, FileMode.Append);
            string temp_string = website + '/' + user + '/' + password + '/';
            char[] temp_arr = temp_string.ToCharArray();
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < temp_arr.Length; i++)
            {
                byte temp_byte = Encrypt(temp_arr[i]);
                bytes.Add(temp_byte);
            }

            byte[] true_bytes = bytes.ToArray();
            passfile.Write(true_bytes, 0, true_bytes.Length);
            passfile.WriteByte(0x0A);
            passfile.Close();

            return true;
        }

        public List<string> Crypto_Find_Pass(string txt)
        {
            passfile = File.OpenRead(dfilepath);
            bool hasreached = false;
            bool endreached = false;
            bool beginread = true;
            List<int> bytes = new List<int>();
            List<int> email = new List<int>();
            List<string> final = new List<string>();
            for (int i = 0; !hasreached && !endreached; i++)
            {
                bytes.Add(passfile.ReadByte());
                if (bytes[i] == -1)
                {
                    break;
                }
                else
                {
                    if (bytes[i] == 0x0A)
                    {
                        beginread = true;
                    }
                    else if (bytes[i] == 48 && beginread == true)
                    {
                        beginread = false;
                        string email_string = "";
                        for (int x = 0; x < email.Count; x++)
                        {
                            email_string += Convert.ToChar(email[x] - 1);

                        }
                        final.Add(email_string);
                        email.Clear();
                    }
                }
                if (beginread && bytes[i] != 10)
                {
                    email.Add(bytes[i]);
                }
            }

            final = new List<string>(Search(final.ToArray(), txt));
            passfile.Close();
            return final;
        }

        public static string[] Search(string[] passwrds, string srch)
        {
            int srch_len = srch.Length;
            List<string> result = new List<string>();
            for (int i = 0; i < passwrds.Length; i++)
            {
                int pass_len = passwrds[i].Length;
                int j;
                string word = passwrds[i];
                for (j = 0; j <= pass_len - srch_len; j++)
                {
                    int w;
                    for (w = 0; w < srch_len; w++)
                        if (word[j + w] != srch[w])
                            break;

                    if (w == srch_len)
                    {
                        result.Add(word);
                        break;
                    }




                }
            }
            return result.ToArray();
        }

        public string Crypto_Find_Password(string url)
        {
            passfile = File.OpenRead(dfilepath);
            bool hasreached = false;
            bool endreached = false;
            bool beginread = true;
            List<int> bytes = new List<int>();
            string temp = "";
            string email;
            string pass = "";
            int space_counter = 0;
            bool is_url = false;
            bool beginread2 = false;
            for (int i = 0; !hasreached && !endreached; i++)
            {
                bytes.Add(passfile.ReadByte());
                if (bytes[i] == -1)
                {
                    break;
                }
                if (bytes[i] == 0x0A)
                {
                    beginread = true;
                    space_counter = 0;
                    if (beginread2)
                    {
                        break;
                    }
                }
                if (bytes[i] == 48)
                {
                    space_counter++;
                    if (beginread)
                    {
                        beginread = false;
                        email = temp;
                        temp = "";
                        if (email == url)
                        {
                            is_url = true;
                        }
                        else
                        {
                            is_url = false;
                        }
                    }
                }

                if (bytes[i] != 48 && space_counter == 2 && is_url)
                {
                    pass += Convert.ToChar(bytes[i] - 1);
                }

                if (beginread && bytes[i] != 10)
                {
                    temp += Convert.ToChar(bytes[i] - 1);
                }
            }
            return pass;
        }

        private FileStream passfile;
        private static FileStream accfile;
        private string dfilepath;
    }
}
