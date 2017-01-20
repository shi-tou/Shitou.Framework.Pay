using Newtonsoft.Json;
using Shitou.Framework.Pay;
using Shitou.Framework.Pay.Alipay;
using Shitou.Framework.Pay.Alipay.Pay;
using Shitou.Framework.Pay.Unionpay;
using Shitou.Framework.Pay.Unionpay.Pay;
using Shitou.Framework.Pay.Weixin;
using Shitou.Framework.PayDemp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;
using System.Web.Http;


namespace Weiche.App.WebApi.Controllers
{
    /// <summary>
    /// 支付相关
    /// </summary>
    [RoutePrefix("api/Pay")]
    [Log]
    [ExceptionHanding]
    public class PayController : ApiController
    {
        #region 支付宝支付
        /// <summary>
        /// 获取支付宝支付参数
        /// </summary>
        /// <returns></returns>
        [Route("GetAlipayPayParameter")]
        [HttpPost]
        public AppResponse<string> GetAlipayPayParameter(GetPayStringRequest request)
        {
            AppResponse<string> response = new AppResponse<string>();
            //生成业务付款单号
            string paymentNo = DateTime.Now.ToString("yyyyMMddHHmmsss");
            //获取订单价格(业务可通过订单号获取)
            decimal payAmount = 0.01m;
            //支付参数串
            string payString = AlipayUtils.GetPayString(paymentNo, "支付订单", "支付订单-[" + request.OrderNo + "]", payAmount);
            if (string.IsNullOrEmpty(payString))
            {
                response.MsgCode = "10001";
                return response;
            }

            //添加待支付的付款记录
            PaymentRecordInfo payment = new PaymentRecordInfo()
            {
                ID = Guid.NewGuid().ToString(),
                PaymentNo = paymentNo,
                OrderNo = request.OrderNo,
                PayParameter = payString,
                PayAmount = payAmount,
                PayChannel = PayChannelConst.WeixinPay,
                PayStatus = PaymentStatusConst.WaitPay,
                IsPaid = false,
                CreateTime = DateTime.Now
            };
            //Insert<PaymentRecordInfo>(payment);
            if (true)
            {
                response.IsOk = true;
                response.MsgCode = "10000";
                response.ReturnData = payString;
                return response;
            }
            return response;
        }
        /// <summary>
        /// 支付宝支付成功回调通知
        /// </summary>
        /// <returns></returns>
        [Route("AlipayPayNotify")]
        [HttpPost]
        public string AlipayPayNotify()
        {
            LogHelper.SaveFileLog("AlipayPayNotify支付宝回调", "start");
            Dictionary<string, string> dicParam = GetRequestPost();
            //回调通知参数为空
            if (dicParam.Count == 0)
            {
                LogHelper.SaveFileLog("AlipayPayNotify支付宝回调", "无通知参数");
                return "fail";
            }
            AlipayNotify aliNotify = new AlipayNotify();
            //获取验签结果
            bool verifyResult = aliNotify.Verify(dicParam, dicParam["notify_id"], dicParam["sign"]);
            //验签失败
            if (!verifyResult)
            {
                LogHelper.SaveFileLog("AlipayPayNotify支付宝回调", "验签失败");
                return "fail";
            }

            //商户订单号
            string out_trade_no = dicParam["out_trade_no"];
            //支付宝交易号
            string trade_no = dicParam["trade_no"];
            //交易状态
            string trade_status = dicParam["trade_status"];

            if (trade_status == "TRADE_FINISHED")
            {
                LogHelper.SaveFileLog("AlipayPayNotify支付宝回调", "trade_status:" + trade_status);
                return "success";
                //判断该笔订单是否在商户网站中已经做过处理
                //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                //如果有做过处理，不执行商户的业务程序

                //注意：
                //该种交易状态只在两种情况下出现
                //1、开通了普通即时到账，买家付款成功后。
                //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后。
            }
            else if (trade_status == "TRADE_SUCCESS")
            {
                try
                {
                    //OrderService.PayOrderNotify(out_trade_no, trade_no);
                    return "success";
                }
                catch (Exception ex)
                {
                    LogHelper.SaveFileLog("AlipayPayNotify支付宝回调", "更新订单状态失败->Exception:" + ex.Message);
                    return "fail";
                }
            }
            else
            {
                LogHelper.SaveFileLog("AlipayPayNotify支付宝回调", "支付失败->trade_status:" + trade_status);
                return "fail";
            }
        }
        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetRequestPost()
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();
            HttpRequest request = HttpContext.Current.Request;
            NameValueCollection coll = request.Form;
            foreach (string key in coll.AllKeys)
            {
                sArray.Add(key, request.Form[key]);
            }
            return sArray;
        }
        #endregion

        #region 微信支付
        /// <summary>
        /// 获取微信支付参数
        /// </summary>
        /// <returns></returns>
        [Route("GetWeixinPayParameter")]
        [HttpPost]
        public AppResponse<SortedDictionary<string, object>> GetWeixinPayParameter(GetPayStringRequest request)
        {
            AppResponse<SortedDictionary<string, object>> response = new AppResponse<SortedDictionary<string, object>>();
            string paymentNo = Guid.NewGuid().ToString();
            //获取订单价格(业务可通过订单号获取)
            decimal payAmount = 0.01m;
            string payParam = "";
            //支付参数串
            SortedDictionary<string, object> payString = WxPayUtils.GetAppPayString(paymentNo,"支付订单", "支付订单-[" + request.OrderNo + "]", payAmount * 100, out payParam);
            if (payString == null || payString.Count == 0)
            {
                response.MsgCode = "10001";
                return response;
            }
           
            //添加待支付的付款记录
            PaymentRecordInfo payment = new PaymentRecordInfo()
            {
                ID = Guid.NewGuid().ToString(),
                PaymentNo = paymentNo,
                OrderNo = request.OrderNo,
                PayParameter = payParam,
                PayAmount = payAmount,
                PayChannel = PayChannelConst.WeixinPay,
                PayStatus = PaymentStatusConst.WaitPay,
                IsPaid = false,
                CreateTime = DateTime.Now
            };
            //OrderService.Insert<PaymentRecordInfo>(payment)
            if (true)
            {
                response.IsOk = true;
                response.MsgCode = "10000";
                response.ReturnData = payString;
                return response;
            }
            else
            {
                return response;
            }
        }
        /// <summary>
        /// 微信支付成功回调通知
        /// </summary>
        /// <returns></returns>
        [Route("WeixinPayNotify")]
        [HttpPost]
        public string WeixinPayNotify()
        {
            Notify notify = new Notify();
            WxPayData result = null;
            if (HttpContext.Current.Request.InputStream == null)
            {
                result.SetValue("return_code", "FAIL");
                result.SetValue("return_msg", "更新订单状态失败");
                return result.ToXml();
            }
            WxPayData notifyData = notify.GetNotifyData(HttpContext.Current.Request.InputStream);
            LogHelper.SaveFileLog("WeixinPayNotify支付回调", JsonConvert.SerializeObject(notifyData));
            //检查支付结果中transaction_id是否存在
            if (!notifyData.IsSet("transaction_id"))
            {
                //若transaction_id不存在，则立即返回结果给微信支付后台
                result = new WxPayData();
                result.SetValue("return_code", "FAIL");
                result.SetValue("return_msg", "支付结果中微信订单号不存在");
                LogHelper.SaveFileLog("WeixinPayNotify支付回调", result.ToXml());
                return result.ToXml();
            }
            //微信支付交易号
            string transaction_id = notifyData.GetValue("transaction_id").ToString();
            //商户订单号
            string out_trade_no = notifyData.GetValue("out_trade_no").ToString();

            //查询订单，判断订单真实性
            result = WxPayApi.OrderQuery(notifyData);
            if (result.GetValue("return_code").ToString().ToUpper() != "SUCCESS" || result.GetValue("result_code").ToString().ToUpper() != "SUCCESS")
            {
                result = new WxPayData();
                result.SetValue("return_code", "FAIL");
                result.SetValue("return_msg", "订单查询失败");
                LogHelper.SaveFileLog("WeixinPayNotify支付回调", "订单查询失败" + result.ToXml());
                return result.ToXml();
            }
            //处理订单逻辑(状态，跟踪记录)
            try
            {
                var response = new AppResponse();
                //response = OrderService.PayOrderNotify(out_trade_no, transaction_id);
                result = new WxPayData();
                if (response.IsOk)
                {
                    result.SetValue("return_code", "SUCCESS");
                    result.SetValue("return_msg", "OK");
                }
                else
                {
                    result.SetValue("return_code", "FAIL");
                    result.SetValue("return_msg", "更新订单状态失败");
                }
                return result.ToXml();
            }
            catch (Exception ex)
            {
                result = new WxPayData();
                result.SetValue("return_code", "FAIL");
                result.SetValue("return_msg", "回调失败");
                LogHelper.SaveFileLog("WeixinPayNotify支付回调", "Exception:" + ex.Message);
                return result.ToXml();
            }
        }
        #endregion

        #region 银联支付
        /// <summary>
        /// 获取银联预授权支付参数
        /// </summary>
        /// <returns></returns>
        [Route("GetUnionpayPrePayParameter")]
        [HttpPost]
        public AppResponse<string> GetUnionpayPrePayParameter(GetPayStringRequest request)
        {
            return GetUnionpayPayParameterCore(request, true);
        }
        /// <summary>
        /// 获取银联支付信息
        /// </summary>
        /// <returns></returns>
        [Route("GetUnionpayPayParameter")]
        [HttpPost]
        public AppResponse<string> GetUnionpayPayParameter(GetPayStringRequest request)
        {
            return GetUnionpayPayParameterCore(request, false);
        }
        /// <summary>
        /// 获取银联支付信息
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isPrePay">是否预授权支付</param>
        /// <returns></returns>
        private AppResponse<string> GetUnionpayPayParameterCore(GetPayStringRequest request, bool isPrePay)
        {
            AppResponse<string> response = new AppResponse<string>();
            string paymentNo = Guid.NewGuid().ToString();
            //获取订单价格(业务可通过订单号获取)
            decimal payAmount = 0.01m;
            string payParam = "";
            //支付参数串
            UniionpayResult payResult = UnionpayUtils.GetPayString(paymentNo,"支付订单-[" + request.OrderNo + "]", payAmount, isPrePay, out payParam);
            if (payResult.respcode != "00")
            {
                response.MsgCode = "10012";
                if (!string.IsNullOrEmpty(payResult.respMsg))
                    response.MsgContent = payResult.respMsg;
                return response;
            }

            //添加待支付的付款记录
            PaymentRecordInfo payment = new PaymentRecordInfo()
            {
                ID = Guid.NewGuid().ToString(),
                PaymentNo = paymentNo,
                OrderNo = request.OrderNo,
                PayParameter = payParam,
                PayAmount = payAmount,
                PayChannel = PayChannelConst.Unionpay,
                PayStatus = PaymentStatusConst.WaitPay,
                IsPaid = false,
                CreateTime = DateTime.Now
            };
            //OrderService.Insert<PaymentRecordInfo>(payment)
            if (true)
            {
                response.IsOk = true;
                response.MsgCode = "10000";
                response.ReturnData = payResult.tn;
                return response;
            }
            else
            {
                return response;
            }
        }
        /// <summary>
        /// 银联支付成功回调通知
        /// </summary>
        /// <returns></returns>
        [Route("UnionpayPayNotify")]
        [HttpPost]
        public string UnionpayPayNotify()
        {
            // 使用Dictionary保存参数
            Dictionary<string, string> resData = new Dictionary<string, string>();
            resData = GetRequestPost();
            LogHelper.SaveFileLog("UnionpayPayNotify支付回调", "回调参数：" + JsonConvert.SerializeObject(resData));
            // 返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
            if (SDKUtil.Validate(resData, Encoding.UTF8))
            {
                //Response.Write("商户端验证返回报文签名成功\n");

                string out_trade_no = resData["orderId"];
                string trade_no = resData["queryId"];
                //商户端根据返回报文内容处理自己的业务逻辑 ,DEMO此处只输出报文结果
                try
                {
                    //OrderService.PayOrderNotify(out_trade_no, trade_no);
                    LogHelper.SaveFileLog("UnionpayPayNotify支付回调", "支付成功->TRADE_SUCCESS");
                    return "success";
                }
                catch (Exception ex)
                {
                    LogHelper.SaveFileLog("UnionpayPayNotify支付回调", "Exception:" + ex.Message);
                    return "fail";
                }
            }
            else
            {
                LogHelper.SaveFileLog("UnionpayPayNotify支付回调", "验签失败");
                return "fail";
            }
        }
        #endregion
    }
}