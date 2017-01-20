using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Pay
{
    /// <summary>
    /// 付款记录
    /// </summary>    
    public class PaymentRecordInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 支付流水号(用于回调)
        /// </summary>
        public string PaymentNo { get; set; }

        /// <summary>
        /// 交易号(渠道商返回)
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 支付参数
        /// </summary>
        public string PayParameter { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        /// 支付渠道(1:支付宝，2:微信，3:银联)
        /// </summary>
        public int PayChannel { get; set; }

        /// <summary>
        /// 状态：1-未支付 2-已支付 3-待退款 4-退款中 5-退款成功 6-退款失败
        /// </summary>
        public int PayStatus { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime PayTime { get; set; }

        /// <summary>
        /// 是否支付
        /// </summary>
        public bool IsPaid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
