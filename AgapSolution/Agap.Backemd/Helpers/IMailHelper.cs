using Agap.Shared.Responses;

namespace Agap.Backemd.Helpers
{
    public interface IMailHelper
    {
        Response<string> SendMail(string toName, string toEmail, string subject, string body);
    }
}
