using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using AutoMapper.Internal;

using PTSL.Ovidhan.Common.Model.EntityViewModels.SystemUser;

namespace PTSL.Ovidhan.Service.Services.Interface.SystemUser
{
    public interface IMessageService
    {
        void SendMessage();
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
