using System;
using System.Collections.Generic;
using System.Web;

namespace Shitou.Framework.Pay.Weixin
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}