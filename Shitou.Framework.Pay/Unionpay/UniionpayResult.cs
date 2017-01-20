using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Pay.Unionpay
{
    public class UniionpayResult
    {
        /// <summary>
        /// 00表示成功 其他-异常
        /// </summary>
        public string respcode { get; set; }

        public string respMsg { get; set; }

        public string tn { get; set; }
    }
}
