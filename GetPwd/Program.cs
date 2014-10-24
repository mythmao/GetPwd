using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordPolicy;

namespace GetPwd
{
    class Program
    {
        static void Main(string[] args)
        {
            PasswordUtility passwordUtility = PasswordUtility.GetInstance();

            //string s = "c1";
            //Byte[] bytes = System.Text.Encoding.Default.GetBytes(s);

            string pwd1 = passwordUtility.HashPassword("kuangcf@21edu.com", "kuangcf1019");

            string pwd2 = passwordUtility.EncryptPasswordByDES("kuangcf@21edu.com", "kuangcf1019");
            //Console.WriteLine(pwd);
            //Console.WriteLine(pwd2);

            //decimal d0 = 1m;
            //decimal d1 = 1.2m;
            //decimal d2 = 1.5m;
            //decimal d3 = 1.7m;
            //Console.WriteLine("Round(1m) = " + Math.Round(d0));
            //Console.WriteLine("Round(1.2m) = "+Math.Round(d1));
            //Console.WriteLine("Round(1.5m) = " + Math.Round(d2));
            //Console.WriteLine("Round(1.7m) = " + Math.Round(d2));
            //Console.WriteLine("ToString(1m) = " + d0.ToString("0"));
            //Console.WriteLine("ToString(1.2m) = "+d1.ToString("0"));
            //Console.WriteLine("ToString(1.5m) = " + d2.ToString("0"));
            //Console.WriteLine("ToString(1.7m) = " + d3.ToString("0"));
            string pwd = passwordUtility.UnEncryptPasswordByDES("HwG3pQNemX0=", "WuShouXieChongZhiDingGou@xueda.com");
            Console.WriteLine(pwd);
            Console.Read();
        }
    }
}
