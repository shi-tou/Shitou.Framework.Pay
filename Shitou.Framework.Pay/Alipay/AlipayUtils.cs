using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using Shitou.Framework.Pay.Alipay.Pay;

namespace Shitou.Framework.Pay.Alipay
{
   /// <summary>
	/// 支付宝工具类
	/// </summary>
    public class AlipayUtils
    {
        /// <summary>
        /// 生成App支付订单参数信息
        /// 老版：https://doc.open.alipay.com/docs/doc.htm?spm=a219a.7629140.0.0.wJXHY0&treeId=59&articleId=103563&docType=1
        /// 新版：https://doc.open.alipay.com/docs/doc.htm?spm=a219a.7629140.0.0.HHgJg3&treeId=193&articleId=105297&docType=1
        /// </summary>
        /// <param name="outTradeNo">业务单号</param>
        /// <param name="subject">支付标题</param>
        /// <param name="body">支付内容</param>
        /// <param name="totalFee">支付金额</param>
        /// <returns></returns>
        public static string GetPayString(string outTradeNo, string subject, string body, decimal totalFee)
        {
            try
            {
                #region 数据
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("app_id", AlipayConfig.App_ID);
                dic.Add("method", "alipay.trade.app.pay");
                dic.Add("sign_type", AlipayConfig.Sign_type);
                dic.Add("charset", "utf-8");
                dic.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                dic.Add("version", "1.0");
                dic.Add("notify_url", AlipayConfig.Notify_Url);
                #region biz_content
                Dictionary<string, string> biz = new Dictionary<string, string>();
                biz.Add("subject", subject);
                biz.Add("body", body);
                biz.Add("out_trade_no", outTradeNo);
                biz.Add("timeout_express", "30m");//该笔订单允许的最晚付款时间，逾期将关闭交易。取值范围：1m～15d。m-分钟，h-小时，d-天，1c-当天（1c-当天的情况下，无论交易何时创建，都在0点关闭）。 该参数数值不接受小数点， 如 1.5h，可转换为 90m。
                biz.Add("total_amount", totalFee.ToString());//单元(元)
                biz.Add("seller_id", AlipayConfig.Seller);
                biz.Add("product_code", "QUICK_MSECURITY_PAY");
                #endregion
                dic.Add("biz_content", JsonConvert.SerializeObject(biz));

                //生成签名
                dic.Add("sign", AlipayCore.CreateSign(dic));
                #endregion
                return AlipayCore.CreateLinkStringUrlencode(dic, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                LogHelper.SaveFileLog("WxPayUtils.GetAppPayString", ex.Message);
            }
            return "";
        }
    }
}
