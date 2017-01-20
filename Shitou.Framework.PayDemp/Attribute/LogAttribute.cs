using Newtonsoft.Json;
using Shitou.Framework.Pay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Shitou.Framework.PayDemp
{
    /// <summary>
    /// WebApi 服务监控
    /// <remarks>监控Action传递的参数值</remarks>
    /// </summary>
    public class LogAttribute : ActionFilterAttribute
    {
       
        /// <summary>
        /// 执行前调用
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments != null && actionContext.ActionArguments.Any())
            {
                var requestData = actionContext.ActionArguments.FirstOrDefault().Value;
                if (requestData != null)
                {
                    try
                    {
                        var actionName = actionContext.ActionDescriptor.ActionName;
                        var memberID = GetPropertyValue(requestData, "MemberID") ?? "";
                        var appToken = GetPropertyValue(requestData, "AppToken") ?? "";
                        LogHelper.SaveFileLog(actionName, JsonConvert.SerializeObject(requestData));
                    }
                    catch (Exception ex)
                    {
                        LogHelper.SaveFileLog("OnActionExecuting", ex.Message + "\r\n Request:" + JsonConvert.SerializeObject(requestData));
                    }
                }
            }
        }

        /// <summary>
        /// 执行后调用
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var requestData = actionExecutedContext.ActionContext.ActionArguments.FirstOrDefault().Value;
            if (requestData != null)
            {
                try
                {
                    var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;                    
                    var responseData = string.Empty;

                    if (actionExecutedContext.Response != null)
                        responseData = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
                    LogHelper.SaveFileLog(actionName, JsonConvert.SerializeObject(responseData));
                }
                catch (Exception ex)
                {
                    LogHelper.SaveFileLog("OnActionExecuted", ex.Message + "\r\n Request:" + JsonConvert.SerializeObject(requestData));
                }
            }
        }

        private static string GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            try
            {
                Type t = info.GetType();
                IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
                return (property != null && property.First() != null) ? property.First().GetValue(info, null) as string : string.Empty;
            }
            catch
            {                
                return "";
            }
        }
    }
}