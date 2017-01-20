using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;


namespace Shitou.Framework.Pay.Unionpay
{
    /// <summary>
    /// 银联支付配置信息类
    /// </summary>
    public class UnionpayConfigInfo
    {
        public static UnionpayConfigInfo Instance { get; set; }
        static UnionpayConfigInfo()
        {
            try
            {
                Instance = JsonConvert.DeserializeObject<UnionpayConfigInfo>(File.ReadAllText(HttpContext.Current.Server.MapPath("/Config/UnionpayConfig.json")));
            }
            catch (Exception ex)
            {
                LogHelper.SaveFileLog("解析银联支付配置文件[Config/UnionpayConfig.json]出错", ex.Message);
                Instance = null;
            }
        }

        /// <summary>
        /// 签名证书路径 
        /// </summary>
        public string SignCertPath { get; set; }

        /// <summary>
        /// 签名证书密码
        /// </summary>
        public string SignCertPwd { get; set; }

        /// <summary>
        /// 验签目录
        /// </summary>
        public string ValidateCertDir { get; set; }

        /// <summary>
        /// 加密公钥证书路径
        /// </summary>
        public string EncryptCert { get; set; }

        /// <summary>
        /// 有卡交易路径
        /// </summary>
        public string CardRequestUrl { get; set; }

        /// <summary>
        /// app交易路径
        /// </summary>
        public string AppRequestUrl { get; set; }

        /// <summary>
        /// 前台同步通知地址
        /// </summary>
        public string FrontTransUrl { get; set; }

        /// <summary>
        /// 后台异步通知地址
        /// </summary>
        public string BackTransUrl { get; set; }

        /// <summary>
        /// 退款通知地址
        /// </summary>
        public string RefundNotifyUrl { get; set; }

        /// <summary>
        /// 商户代码
        /// </summary>
        public string MerId { get; set; }

    }
}
