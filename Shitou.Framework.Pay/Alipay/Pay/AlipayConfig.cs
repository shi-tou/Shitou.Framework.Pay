using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;


namespace Shitou.Framework.Pay.Alipay.Pay
{
    /// <summary>
    /// 类名：Config
    /// 功能：基础配置类
    /// 详细：设置帐户有关信息及返回路径
    /// 版本：3.3
    /// 日期：2012-07-05
    /// 说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究支付宝接口使用，只是提供一个参考。
    /// 
    /// 如何获取安全校验码和合作身份者ID
    /// 1.用您的签约支付宝账号登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class AlipayConfig
    {
        #region 字段
        private static string partner = "";
        private static string private_key = "";
        private static string public_key = "";
        private static string input_charset = "";
        private static string sign_type = "";
        private static string seller = "";
        private static string notify_url = "";
        //appid
        private static string app_id = "";
        #endregion

        static AlipayConfig()
        {
            //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓

            AlipayConfigInfo alipayConfigInfo = AlipayConfigInfo.Instance;
            //合作身份者ID，以2088开头由16位纯数字组成的字符串
            partner = alipayConfigInfo.Partner;

            //卖家支付宝帐号
            seller = alipayConfigInfo.Seller;

            //商户的私钥
            private_key = alipayConfigInfo.PrivateKey;

            //支付宝的公钥，无需修改该值
            public_key = alipayConfigInfo.PublicKey;

            //字符编码格式 目前支持 gbk 或 utf-8
            input_charset = "utf-8";

            //签名方式，选择项：RSA、DSA、MD5
            sign_type = "RSA";

            //异步通知地址
            notify_url = alipayConfigInfo.PayNotifyUrl;

            //app支付应用id
            app_id = alipayConfigInfo.AppID;
        }

        #region 属性
        /// <summary>
        /// 获取或设置合作者身份ID
        /// </summary>
        public static string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

         /// <summary>
        /// 卖家支付宝帐号
        /// </summary>
        public static string Seller
        {
            get { return seller; }
            set { seller = value; }
        }

        /// <summary>
        /// 获取或设置商户的私钥
        /// </summary>
        public static string Private_key
        {
            get { return private_key; }
            set { private_key = value; }
        }

        /// <summary>
        /// 获取或设置支付宝的公钥
        /// </summary>
        public static string Public_key
        {
            get { return public_key; }
            set { public_key = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string Input_charset
        {
            get { return input_charset; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string Sign_type
        {
            get { return sign_type; }
        }

        /// <summary>
        /// 异步通知地址
        /// </summary>
        public static string Notify_Url
        {
            get { return notify_url; }
        }

        /// <summary>
        /// app支付应用id
        /// </summary>
        public static string App_ID
        {
            get { return app_id; }
        }

        #endregion
    }
}