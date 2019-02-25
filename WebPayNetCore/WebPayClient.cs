using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using WebPayNetCore.Extensions;
using WebPayNetCore.Helpers;
using WebPayNetCore.Models;

namespace WebPayNetCore
{
    public class WebPayClient
    {
        private readonly int _projectId;
        private readonly string _signPassword;
        public const string Version = "1.6";
        public const string PayUrl = "https://bank.paysera.com/pay/";
        public const string PublicKeyUrl = "https://www.paysera.com/download/public.key";

        public WebPayClient(int projectId, string signPassword)
        {
            if(projectId < 0)
                throw new ArgumentException("Incorrect project id.");

            if(string.IsNullOrEmpty(signPassword))
                throw new ArgumentNullException(nameof(signPassword), "Password can't be empty.");

            _projectId = projectId;
            _signPassword = signPassword;
        }

        public string GenerateMacroRequest(MacroRequestParams macroRequestParams)
        {
            if (macroRequestParams.ProjectId == 0)
                macroRequestParams.ProjectId = _projectId;

            if (string.IsNullOrEmpty(macroRequestParams.Version))
                macroRequestParams.Version = Version;

            if(macroRequestParams.ProjectId != _projectId)
                throw new ArgumentException("Wrong project id.");

            if(macroRequestParams.Version != Version)
                throw new ArgumentException("Wrong version.");

            var data = macroRequestParams.ToBase64String();
            var sign = data.CalculateMd5(_signPassword);

            var requestQueryParams = new Dictionary<string, string>
            {
                ["data"] = data, ["sign"] = sign
            };

            var query = requestQueryParams.ToQueryString();

            return $"{PayUrl}?{query}";
        }

        public MacroCallbackResult ParseMacroCallback(string data, string ss2)
        {
            if(string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            if (string.IsNullOrEmpty(ss2))
                throw new ArgumentNullException(nameof(ss2));

            var keyFile = CryptoHelper.DownloadPublicKey(PublicKeyUrl);
            var keyRawData = CryptoHelper.GetRawDataFromKeyData(keyFile);

            if (!CryptoHelper.IsValidSs2(data, ss2.DecodeBase64(), keyRawData))
            {
                throw new InvalidOperationException("Signed data validation failed (SS2).");
            }

            var dataQuery = Encoding.UTF8.GetString(data.DecodeBase64());
            var queryDictionary = HttpUtility.ParseQueryString(dataQuery).ToDictionary();
            var callbackResult = queryDictionary.ToModel<MacroCallbackResult>();
            
            if(callbackResult.ProjectId != _projectId)
                throw new Exception($"Wrong project id. Project id is {callbackResult.ProjectId} should be {_projectId}.");

            return callbackResult;
        }

        public string GetCallbackSuccessResponse()
        {
            return "OK";
        }
    }
}