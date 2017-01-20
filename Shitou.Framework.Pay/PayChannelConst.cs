using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Pay
{
    /// <summary>
    /// 支付渠道（1:支付宝，2:微信，3:银联)
    /// </summary>
    public static class PayChannelConst
    {
        /// <summary>
        /// 支付宝
        /// </summary>
        public const int Alipay = 1;
        /// <summary>
        /// 微信
        /// </summary>
        public const int WeixinPay = 2;
        /// <summary>
        /// 银联
        /// </summary>
        public const int Unionpay = 3;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetPayChannelName(int s)
        {
            if (s == 1)
            {
                return "支付宝";
            }
            if (s == 2)
            {
                return "微信";
            }
            if (s == 3)
            {
                return "银联";
            }
            return "";
        }
    }
}
