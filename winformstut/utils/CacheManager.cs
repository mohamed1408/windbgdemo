using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winformstut.utils
{
    internal class CacheManager
    {
        public delegate void Callback(string message);
        public static Callback CBack;
        // Create a method for a delegate.
        public static void SendCallBack(string message)
        {
            if (CBack != null)
            {
                CBack(message);
            }
        }
    }
}
