using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;

namespace Shitou.Framework.Pay.Alipay
{
   /// <summary>
	/// 支付宝配置信息类
	/// </summary>
    public class AlipayConfigInfo
    {
        public static AlipayConfigInfo Instance { get; set; }
        static AlipayConfigInfo()
        {
            try
            {
                Instance = JsonConvert.DeserializeObject<AlipayConfigInfo>(File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/AlipayConfig.json")));
            }
            catch (Exception ex)
            {
                LogHelper.SaveFileLog("解析支付宝配置文件[Config/AlipayConfig.json]出错", ex.Message);
                Instance = null;
            }
        }
        /// <summary>
        /// 合作商户ID。用签约支付宝账号登录ms.alipay.com后，在账户信息页面获取。  
        /// </summary>
        public string Partner { get; set; }

        /// <summary>
        /// 账户ID。用签约支付宝账号登录ms.alipay.com后，在账户信息页面获取。  
        /// </summary>
        public string Seller { get; set; }

        /// <summary>
        /// 32位安全交易码
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 商户（RSA）私钥 ,即rsa_private_key.pem中去掉首行，最后一行，空格和换行最后拼成一行的字符串  
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// 支付宝（RSA）公钥  用签约支付宝账号登录ms.alipay.com后，在密钥管理页面获取。  
        /// </summary>
        public string PublicKey { get; set; }

        /// <summary>
        /// 支付通知地址
        /// </summary>
        public string PayNotifyUrl { get; set; }

        /// <summary>
        /// 退款通知地址
        /// </summary>
        public string RefundNotifyUrl { get; set; }

        /// <summary>
        /// App支付应用id
        /// </summary>
        public string AppID { get; set; }
    }
}
