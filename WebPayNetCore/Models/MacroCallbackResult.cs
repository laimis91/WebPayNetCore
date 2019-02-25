using WebPayNetCore.Attributes;

namespace WebPayNetCore.Models
{
    public class MacroCallbackResult
    {
        [ParameterName("projectid")]
        public int ProjectId { get; set; }

        [ParameterName("orderid")]
        public string OrderId { get; set; }

        [ParameterName("lang")]
        public string Language { get; set; }

        [ParameterName("amount")]
        public int Amount { get; set; }

        [ParameterName("currency")]
        public string Currency { get; set; }

        [ParameterName("payment")]
        public string Payment { get; set; }

        [ParameterName("country")]
        public string Country { get; set; }

        [ParameterName("paytext")]
        public string PayText { get; set; }

        [ParameterName("name")]
        public string FirstName { get; set; }

        [ParameterName("surename")]
        public string LastName { get; set; }

        [ParameterName("status")]
        public int Status { get; set; }

        [ParameterName("test")]
        public bool Test { get; set; }

        [ParameterName("p_email")]
        public string Email { get; set; }

        [ParameterName("requestid")]
        public int RequestId { get; set; }

        [ParameterName("payamount")]
        public int PayAmount { get; set; }

        [ParameterName("paycurrency")]
        public string PayCurrency { get; set; }

        [ParameterName("version")]
        public string Version { get; set; }

        [ParameterName("account")]
        public string Account { get; set; }

        [ParameterName("personcodestatus")]
        public int? PersonCodeStatus { get; set; }
    }
}