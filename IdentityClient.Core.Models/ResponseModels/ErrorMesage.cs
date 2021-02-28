namespace IdentityClient.Core.Models.ResponseModels
{
    public class ErrorMesage : IErrorMesage
    {
        public ErrorMesage() { }
        public ErrorMesage(string value)
        {
            Key = "unknown";
            Value = value;
        }
        public ErrorMesage(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
