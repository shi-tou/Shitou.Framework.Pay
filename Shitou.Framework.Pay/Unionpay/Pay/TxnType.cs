using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace Shitou.Framework.Pay.Unionpay.Pay
{
    #region 交易类型说明
    //00 查询交易
    //01：消费
    //02：预授权
    //03：预授权完成
    //04：退货
    //05: 圈存
    //11：代收
    //12：代付
    //13：账单支付
    //14： 转账（保留）
    //21：批量交易
    //22：批量查询
    //31：消费撤销
    //32：预授权撤销
    //33：预授权完成撤销
    //71：余额查询
    //72：实名认证-建立绑定关系
    //73： 账单查询
    //74：解除绑定关系
    //75：查询绑定关系
    //77：发送短信验证码交易
    //78：开通查询交易
    //79：开通交易
    //94：IC卡脚本通知
    #endregion

    /// <summary>
    /// 交易类型
    /// </summary>
    public class TxnType
    {
        /// <summary>
        /// 消费
        /// </summary>
        public const string Consume = "01";
        /// <summary>
        /// 预授权
        /// </summary>
        public const string PrePay = "02";
        /// <summary>
        /// 预授权完成
        /// </summary>
        public const string PrePaid = "03";
        /// <summary>
        /// 退款
        /// </summary>
        public const string Refund = "04";
        /// <summary>
        /// 预授权撤销
        /// </summary>
        public const string PrePayCancel = "32";
        /// <summary>
        /// 预授权完成撤销
        /// </summary>
        public const string PrePayCanceled = "33";
    }
}