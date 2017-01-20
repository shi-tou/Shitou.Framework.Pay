using Shitou.Framework.Pay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Shitou.Framework.PayDemp
{
    /// <summary>
    /// 异常处理过滤器
    /// </summary>
    public class ExceptionHandingAttribute: ExceptionFilterAttribute
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(HttpActionExecutedContext context)
        {
            try
            {
                Exception ex = context.Exception;
                if (ex.GetType().Name.Equals("HttpException"))
                {
                    HttpException httpEx = (HttpException)ex;
                    int httpCode = httpEx.GetHttpCode();

                    if (httpCode == 404)
                    {
                        return;
                    }
                }
                string errorDesc = "错误消息：" + ex.Message +
                                  "\r\n发生时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                                  "\r\n错误源： " + ex.Source +
                                  "\r\n引发异常的方法： " + ex.TargetSite +
                                  "\r\n堆栈信息： " + ex.StackTrace;
                LogHelper.SaveFileLog("ExceptionHanding" + context.ActionContext.ActionDescriptor.ActionName, errorDesc);
            }
            catch (Exception ex)
            {
                LogHelper.SaveFileLog("ExceptionHanding" + context.ActionContext.ActionDescriptor.ActionName, ex.Message);
            }
        }
    }
}