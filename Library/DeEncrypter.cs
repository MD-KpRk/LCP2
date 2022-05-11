using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public static class DeEncrypter 
    {
        public static string Decrypt(LCPP info)
        {
            return info.SourcePort + ";" + info.DestPort + ";" + info.SourceIP + ";" + info.Message;
        }

        public static LCPP Encrypt(string Message)
        {
            string[] parms = Message.Split(';');
            try
            {
                LCPP protocol = new LCPP(Convert.ToInt32(parms[0]), Convert.ToInt32(parms[1]), IPAddress.Parse(parms[2]),parms[3]);
                return protocol;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            throw new Exception("F");
        }
    }

}
