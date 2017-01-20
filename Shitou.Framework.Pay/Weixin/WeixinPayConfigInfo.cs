using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace Shitou.Framework.Pay.Weixin
{
    /// <summary>
    /// 微信JsPay支付配置信息类
    /// </summary>
    public class WeixinPayConfigInfo
    {
        public static WeixinPayConfigInfo Instance { get; set; }
        static WeixinPayConfigInfo()
        {
            try
            {
                Instance = JsonConvert.DeserializeObject<WeixinPayConfigInfo>(File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/WeixinpayConfig.json")));
            }
            catch (Exception ex)
            {
                LogHelper.SaveFileLog("解析微信支付配置文件[Config/WeixinpayConfig.json]出错", ex.Message);
                Instance = null;
            }
        }
        /// <summary>
        /// APP公众账号ID -商户注册具有支付权限的公众号成功后即可获得
        /// </summary>
        public string AppID { get; set; }
        
        /// <summary>
        /// 公众账号密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 公众号支付商户号
        /// </summary>
        public string Mch_ID { get; set; }

        /// <summary>
        /// 商户支付密钥
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 证书路径
        /// </summary>
        public string Sslcert_Path { get; set; }

        /// <summary>
        /// 证书路径
        /// </summary>
        public string Sslcert_Password { get; set; }

        /// <summary>
        /// 支付通知地址
        /// </summary>
        public string PayNotifyUrl { get; set; }

        /// <summary>
        /// 退款通知地址
        /// </summary>
        public string RefundNotifyUrl { get; set; }
    }
}
