using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Pay
{
    /// <summary>
    /// 支付请求
    /// </summary>
    public class GetPayStringRequest
    {
        /// <summary>
        /// 业务订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付类型(1-购物 2-充值 )
        /// </summary>
        public int PayType { get; set; }
    }
}
