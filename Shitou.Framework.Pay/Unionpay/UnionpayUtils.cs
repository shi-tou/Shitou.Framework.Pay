using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

using Shitou.Framework.Pay.Unionpay.Pay;

namespace Shitou.Framework.Pay.Unionpay
{
   /// <summary>
	/// 银联工具类
	/// </summary>
    public class UnionpayUtils
    {
        /// <summary>
        /// 生成App支付订单参数信息
        /// </summary>
        /// <param name="outTradeNo">业务单号</param>
        /// <param name="subject">支付标题</param>
        /// <param name="body">支付内容</param>
        /// <param name="totalFee">支付金额</param>
        /// <returns></returns>
        public static UniionpayResult GetPayString(string outTradeNo, string orderDesc, decimal totalAmount, bool isPrePay, out string payParam)
        {
            UniionpayResult result = new UniionpayResult();
            //构造待签名数据
            Dictionary<string, string> param = new Dictionary<string, string>();
            try
            {
                //填写参数
                param["version"] = "5.0.0";//版本号
                param["encoding"] = "UTF-8";//编码方式
                param["certId"] = CertUtil.GetSignCertId();//证书ID
                param["txnType"] = isPrePay ? TxnType.PrePay : TxnType.Consume;//交易类型
                param["txnSubType"] = "01";//交易子类
                param["bizType"] = "000000";//业务类型    
                param["backUrl"] = SDKConfig.BackTransUrl;  //后台异步通知地址	
                param["signMethod"] = "01";//签名方法,01：表示采用RSA
                param["channelType"] = "08";//渠道类型，07-PC，08-手机
                param["accessType"] = "0";//接入类型,0：商户直连接入 1：收单机构接入 2：平台商户接入 
                param["merId"] = SDKConfig.MerId;//商户号，请改成自己的商户号
                param["orderId"] = outTradeNo;//商户订单号
                param["txnTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间
                param["txnAmt"] = (totalAmount * 100).ToString("0");//交易金额，单位分
                param["currencyCode"] = "156";//交易币种
                param["orderDesc"] = orderDesc;//订单描述，可不上送，上送时控件中会显示该信息
                SDKUtil.Sign(param, Encoding.UTF8);  // 签名
                payParam = JsonConvert.SerializeObject(param);
                // 初始化通信处理类
                HttpClient hc = new HttpClient(SDKConfig.AppRequestUrl);
                // 发送请求获取通信应答
                int status = hc.Send(param, Encoding.UTF8);
                // 返回结果
                string results = hc.Result;
                Dictionary<string, string> resData = SDKUtil.CoverstringToDictionary(results);
                result.respcode = resData["respCode"];
                if (result.respcode != "00")
                {
                    result.respMsg = resData["respMsg"];
                    return result;
                }
                if (!SDKUtil.Validate(resData, Encoding.UTF8))
                {
                    LogHelper.SaveFileLog("获取银联支付信息失败！", "商户端验证返回报文签名失败:" + JsonConvert.SerializeObject(resData));
                    result.respMsg = resData["respMsg"];
                    return result;
                }
                result.tn = resData["tn"];
            }
            catch (Exception ex)
            {
                payParam = JsonConvert.SerializeObject(param);
                LogHelper.SaveFileLog("获取银联支付信息失败！", ex.Message);
                result.respcode = "";
                result.respMsg = "";
            }
            return result;
        }
    }
}