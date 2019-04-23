using System.Collections.Generic;
using WebPayNetCore.Attributes;

namespace WebPayNetCore.Models
{
    public class MacroRequestParams
    {
        [ParameterName("version")]
        public string Version { get; set; }

        [ParameterName("projectid")]
        public int ProjectId { get; set; }

        [ParameterName("repeat_request")]
        public bool? RepeatRequest { get; set; }

        [ParameterName("test")]
        public bool? Test { get; set; }

        [ParameterName("time_limit")]
        public string TimeLimit { get; set; }

        [ParameterName("personcode")]
        public string PersonCode { get; set; }

        [ParameterName("only_payments")]
        public List<string> AllowPayments { get; set; }

        [ParameterName("disalow_payments")]
        public List<string> DisallowPayments { get; set; }

        [ParameterName("callbackurl")]
        public string CallbackUrl { get; set; }

        [ParameterName("cancelurl")]
        public string CancelUrl { get; set; }

        [ParameterName("orderid")]
        public string OrderId { get; set; }

        [ParameterName("accepturl")]
        public string AcceptUrl { get; set; }

        [ParameterName("lang")]
        public string Language { get; set; }

        [ParameterName("amount")]
        public double? Amount { get; set; }

        [ParameterName("currency")]
        public string Currency { get; set; }

        [ParameterName("payment")]
        public string Payment { get; set; }

        [ParameterName("country")]
        public string Country { get; set; }

        [ParameterName("paytext")]
        public string PayText { get; set; }

        [ParameterName("p_firstname")]
        public string PaypalFirstName { get; set; }

        [ParameterName("p_lastname")]
        public string PaypalLastName { get; set; }

        [ParameterName("p_email")]
        public string Email { get; set; }

        [ParameterName("p_street")]
        public string PaypalStreet { get; set; }

        [ParameterName("p_city")]
        public string PaypalCity { get; set; }

        [ParameterName("p_state")]
        public string PaypalState { get; set; }

        [ParameterName("p_zip")]
        public string PaypalZip { get; set; }

        [ParameterName("p_countrycode")]
        public string PaypalCountryCode { get; set; }
    }
}