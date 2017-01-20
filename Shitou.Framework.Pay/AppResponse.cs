using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shitou.Framework.Pay
{
    public class AppResponse
    {
        public AppResponse()
        {
            IsOk = false;
            MsgCode = "99999";
        }

        public bool IsOk { get; set; }
        private string msgCode = "";
        public string MsgCode
        {
            get { return msgCode; }
            set
            {
                msgCode = value;
                MsgContent = MessageCode.GetMsgContent(value);
            }
        }
        public string MsgContent { get; set; }
    }
    public class AppResponse<T> : AppResponse
    {
        public T ReturnData { get; set; }
    }
}
