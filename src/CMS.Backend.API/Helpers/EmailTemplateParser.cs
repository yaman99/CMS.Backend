using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CMS.Backend.API.Helpers
{
    public class EmailTemplateParser
    {
        private static HttpContext _httpContext => new HttpContextAccessor().HttpContext;
        private static IWebHostEnvironment _hostingEnvironment => (IWebHostEnvironment)_httpContext.RequestServices.GetService(typeof(IWebHostEnvironment));

        static string emailVerificationTemplate;
        static string recoverPassowrdEmailTemplate;
        static string QuickAccountActivationTemplate;
        static string LackOfWalletBalanceWarningTemplate;
        static string LackOfWalletBalanceAccountSuspendedTemplate;
        static string ResetPasswordTemplate;



        public static string GetEmailVerificationMessage(string recepientName, string activationLink)
        {
            if (emailVerificationTemplate == null)
                emailVerificationTemplate = ReadFile("Templates/EmailVerificationTemplate.html");


            string emailMessage = emailVerificationTemplate
                .Replace("{user}", recepientName)
                .Replace("{activation-link}", activationLink);

            return emailMessage;
        }
        public static string GetResetPasswordLink(string link)
        {
            if (ResetPasswordTemplate == null)
                ResetPasswordTemplate = ReadFile("Templates/ResetPasswordTemplate.html");

            string emailMessage = ResetPasswordTemplate
                .Replace("{reset-password-link}", link);

            return emailMessage;
        }

        public static string GetTempPasswordMessage(string password, string signinLink)
        {
            if (emailVerificationTemplate == null)
                emailVerificationTemplate = ReadPhysicalFile("Templates/TempPasswordTemplate.html");


            string emailMessage = emailVerificationTemplate
                .Replace("{password}", password)
                .Replace("{signin-link}", signinLink);

            return emailMessage;
        }

        public static string GetQuickAccountActivationMessaage(string code, string setupUrl)
        {

            if (QuickAccountActivationTemplate == null)
                QuickAccountActivationTemplate = ReadFile("Templates/QuickAccountActivationTemplate.html");

            string emailMessage = QuickAccountActivationTemplate
                .Replace("{code}", code)
                .Replace("{setupUrl}", setupUrl);

            return emailMessage;
        }
        public static string GetLackOfWalletBalanceWarningMessage(int count)
        {

            if (LackOfWalletBalanceWarningTemplate == null)
                LackOfWalletBalanceWarningTemplate = ReadFile("Templates/LackOfWalletBalanceWarningTemplate.html");

            string emailMessage = LackOfWalletBalanceWarningTemplate
                .Replace("{count}", count.ToString());
            return emailMessage;
        }
        public static string GetLackOfWalletAccountsuspendMessage()
        {

            if (LackOfWalletBalanceAccountSuspendedTemplate == null)
                LackOfWalletBalanceAccountSuspendedTemplate = ReadFile("Templates/LackOfWalletBalanceAccountSuspendedTemplate.html");

            return LackOfWalletBalanceAccountSuspendedTemplate;
        }
        private static string ReadPhysicalFile(string path)
        {
            if (_hostingEnvironment == null)
                throw new InvalidOperationException($"{nameof(EmailTemplateParser)} is not initialized");

            IFileInfo fileInfo = _hostingEnvironment.ContentRootFileProvider.GetFileInfo(path);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"Template file located at \"{path}\" was not found");

            using (var fs = fileInfo.CreateReadStream())
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private static string ReadFile(string path)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = buildDir + $"/{path}";
            return File.ReadAllText(filePath);
        }
    }
}
