using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Pay
{
    /// <summary>
    /// 支付记录状态(1-未支付 2-已支付 3-待退款 4-退款中 5-退款成功 6-退款失败)
    /// </summary>
    public static class PaymentStatusConst
    {
        /// <summary>
        /// 待支付
        /// </summary>
        public const int WaitPay = 1;
        /// <summary>
        /// 已支付
        /// </summary>
        public const int Paid = 2;
        /// <summary>
        /// 待退款
        /// </summary>
        public const int WaitRefund = 3;
        /// <summary>
        /// 退款中
        /// </summary>
        public const int Refunding = 4;
        /// <summary>
        /// 退款成功
        /// </summary>
        public const int RefundSuccess = 5;
        /// <summary>
        /// 退款失败
        /// </summary>
        public const int RefundFail = 5;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetName(int s)
        {
            if (s == 1)
            {
                return "待支付";
            }
            if (s == 2)
            {
                return "已支付";
            }
            if (s == 3)
            {
                return "待退款";
            }
            if (s == 4)
            {
                return "退款中";
            }
            if (s == 5)
            {
                return "退款成功";
            }
            if (s == 6)
            {
                return "退款失败";
            }
            return "";
        }
    }
}
