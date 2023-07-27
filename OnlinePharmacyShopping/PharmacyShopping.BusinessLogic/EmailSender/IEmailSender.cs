using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyShopping.BusinessLogic.EmailSender
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
