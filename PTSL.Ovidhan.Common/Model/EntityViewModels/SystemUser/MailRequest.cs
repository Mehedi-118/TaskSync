using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser
{
    public class MailRequest
    {
        public string? ToEmail { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? OTP { get; set; }
    }
}
