using System.Configuration;
using System.Web;

namespace Shitou.Framework.Pay.Unionpay.Pay
{

    public class SDKConfig
    {
        #region 字段
        private static string signcertpath = "";
        private static string signcertpwd = "";
        private static string validatecertdir = "";
        private static string encryptcert = "";
        private static string cardrequesturl = "";
        private static string apprequesturl = "";
        private static string merid = "";
        private static string singlequeryurl = "";
        private static string filetransurl = "";
        private static string fronttransurl = "";
        private static string backtransurl = "";
        private static string backrefundtransurl = "";
        private static string battransurl = "";
        #endregion

        static SDKConfig()
        {
            //=======【基本信息设置】=====================================
            /* 银联信息配置
            * SignCertPath：签名证书路径
            * SignCertPwd：签名证书密码
            * CardRequestUrl：有卡交易路径
            * AppRequestUrl：app交易路径
            * FrontTransUrl：前台同步通知地址
            * EncryptCert：加密公钥证书路径
            * BackTransUrl：后台异步通知地址
            * SingleQueryUrl：交易查询地址
            * FileTransUrl：文件传输类交易地址
            * ValidateCertDir：验签目录
            * BatTransUrl：批量交易地址
            * MerId：商户代码
            */
            UnionpayConfigInfo unionpayConfigInfo = UnionpayConfigInfo.Instance;
            //签名证书路径
            signcertpath = HttpContext.Current.Server.MapPath(unionpayConfigInfo.SignCertPath);
            //签名证书密码
            signcertpwd = unionpayConfigInfo.SignCertPwd;
            //验签目录
            validatecertdir = HttpContext.Current.Server.MapPath(unionpayConfigInfo.ValidateCertDir);
            //加密公钥证书路径
            encryptcert = "";
            //有卡交易路径
            cardrequesturl = unionpayConfigInfo.CardRequestUrl;
            //app交易地址 手机控件支付使用该地址
            apprequesturl = unionpayConfigInfo.AppRequestUrl;
            //交易查询地址
            singlequeryurl = "https://101.231.204.80:5000/gateway/api/queryTrans.do";
            //文件传输类交易地址
            filetransurl = "https://101.231.204.80:9080/";
            //前台同步通知地址
            fronttransurl = "https://101.231.204.80:5000/gateway/api/frontTransReq.do";
            //后台异步通知地址(https: //101.231.204.80:5000/gateway/api/backTransReq.do)
            backtransurl = unionpayConfigInfo.BackTransUrl;
            //退款通知地址
            backrefundtransurl = unionpayConfigInfo.RefundNotifyUrl;
            //批量交易地址
            battransurl = "https://101.231.204.80:5000/gateway/api/batchTrans.do";
            //商户代码
            merid = unionpayConfigInfo.MerId;
        }

        #region 属性
        /// <summary>
        /// 签名证书路径
        /// </summary>
        public static string SignCertPath
        {
            get { return signcertpath; }
            set { signcertpath = value; }
        }
        /// <summary>
        /// 签名证书密码
        /// </summary>
        public static string SignCertPwd
        {
            get { return signcertpwd; }
            set { signcertpwd = value; }
        }
        /// <summary>
        /// 有卡交易路径
        /// </summary>
        public static string CardRequestUrl
        {
            get { return cardrequesturl; }
            set { cardrequesturl = value; }
        }
        /// <summary>
        /// App交易路径
        /// </summary>
        public static string AppRequestUrl
        {
            get { return apprequesturl; }
            set { apprequesturl = value; }
        }
        /// <summary>
        /// 前台同步通知地址
        /// </summary>
        public static string FrontTransUrl
        {
            get { return fronttransurl; }
            set { fronttransurl = value; }
        }
        /// <summary>
        /// 加密公钥证书路径
        /// </summary>
        public static string EncryptCert
        {
            get { return encryptcert; }
            set { encryptcert = value; }
        }
        /// <summary>
        /// 支付异步通知地址
        /// </summary>
        public static string BackTransUrl
        {
            get { return backtransurl; }
            set { backtransurl = value; }
        }
        /// <summary>
        /// 退款异步通知地址
        /// </summary>
        public static string BackRefundTransUrl
        {
            get { return backrefundtransurl; }
            set { backrefundtransurl = value; }
        }
        /// <summary>
        /// 交易查询地址
        /// </summary>
        public static string SingleQueryUrl
        {
            get { return singlequeryurl; }
            set { singlequeryurl = value; }
        }
        /// <summary>
        /// 文件传输类交易地址
        /// </summary>
        public static string FileTransUrl
        {
            get { return filetransurl; }
            set { filetransurl = value; }
        }
        /// <summary>
        /// 验签目录
        /// </summary>
        public static string ValidateCertDir
        {
            get { return validatecertdir; }
            set { validatecertdir = value; }
        }
        /// <summary>
        /// 批量交易地址
        /// </summary>
        public static string BatTransUrl
        {
            get { return battransurl; }
            set { battransurl = value; }
        }
        /// <summary>
        /// 商户代码
        /// </summary>
        public static string MerId
        {
            get { return merid; }
            set { merid = value; }
        }
        #endregion

    }
}