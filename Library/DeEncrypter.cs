using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library
{
    public static class DeEncrypter 
    {
        public static string Decrypt(LCPP info)
        {
            return info.SourcePort + ";" + info.DestPort + ";" + info.SourceIP + ";" + (int)info.Command + ";" + info.Args;
        }

        public static LCPP Encrypt(string Message)
        {
            string[] parms = Message.Split(';');
            MessageBox.Show( parms[3]);
            try
            {
                LCPP protocol = new LCPP(Convert.ToInt32(parms[0]), Convert.ToInt32(parms[1]), IPAddress.Parse(parms[2]), (CommandEnum)Convert.ToInt32(parms[3]),parms[4]);
                return protocol;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            throw new Exception();
        }
    }

}
