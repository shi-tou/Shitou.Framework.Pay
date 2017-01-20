using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Shitou.Framework.Pay.Weixin
{
    public class WxPayUtils
    {
        /// <summary>
        /// 生成App支付订单参数信息
        /// </summary>
        /// <param name="outTradeNo">业务单号</param>
        /// <param name="subject">商品描述</param>
        /// <param name="body">商品详情</param>
        /// <param name="totalFee">支付金额</param>
        /// <returns></returns>
        public static SortedDictionary<string,object> GetAppPayString(string outTradeNo, string body, string detail, decimal totalFee, out string payParam)
        {
            #region 数据
            WxPayData payData = new WxPayData();
            payData.SetValue("out_trade_no", outTradeNo);
            payData.SetValue("body", body);
            //订单总金额，单位为分
            payData.SetValue("total_fee", totalFee.ToString("0"));
            //JSAPI，NATIVE，APP
            payData.SetValue("trade_type", "APP");
            #endregion

            WxPayData result = WxPayApi.UnifiedOrder(payData);
            payParam = JsonConvert.SerializeObject(payData);
            try
            {
                string prepay_id = result.GetValue("prepay_id").ToString();
                return WxPayApi.GeneratePayData(prepay_id);
            }
            catch (Exception ex)
            {
                LogHelper.SaveFileLog("WxPayUtils.GetAppPayString", ex.Message);
            }
            return null;
        }
    }
    /*
    统一下单接口返回结果
    字段名   |  变量名 | 必填 | 类型  |示例值  |   描述
    返回状态码   return_code 是   String(16)  SUCCESS SUCCESS/FAIL-此字段是通信标识，非交易标识，交易是否成功需要查看result_code来判断
    返回信息    return_msg 否   String(128)     签名失败 返回信息，如非空，为错误原因/签名失败/参数格式校验错误

    以下字段在return_code为SUCCESS的时候有返回
    字段名 | 变量名  |   必填 | 类型 | 示例值 | 描述
    应用APPID appid   是 String(32)  wx8888888888888888 调用接口提交的应用ID
    商户号 mch_id  是 String(32)  1900000109 	调用接口提交的商户号
    设备号     device_info 否   String(32)  013467007045764 	调用接口提交的终端设备号，
    随机字符串 nonce_str   是 String(32)  5K8264ILTKCH16CQ2502SI8ZNMTM67VS 微信返回的随机字符串
    签名 sign    是 String(32)  C380BEC2BFD727A4B6845133519F3AD6 微信返回的签名，详见签名算法
    业务结果    result_code 是   String(16)  SUCCESS SUCCESS/FAIL
    错误代码    err_code 否   String(32)  SYSTEMERROR 详细参见第6节错误列表
    错误代码描述 err_code_des    否 String(128)     系统错误 错误返回的信息描述

    以下字段在return_code 和result_code都为SUCCESS的时候有返回
    字段名 变量名     必填 类型  示例值 描述
    交易类型 trade_type  是 String(16)  JSAPI 调用接口提交的交易类型，取值如下：JSAPI，NATIVE，APP，详细说明见参数规定
    预支付交易会话标识   prepay_id 是   String(64)  wx201410272009395522657a690389285100 微信生成的预支付回话标识，用于后续接口调用中使用，该值有效期为2小时*/
}
