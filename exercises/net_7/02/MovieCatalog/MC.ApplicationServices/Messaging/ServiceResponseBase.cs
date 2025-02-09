namespace MC.ApplicationServices.Messaging
{
    public abstract class ServiceResponseBase
    {
        public BusinessStatusCodeEnum StatusCode { get; set; }

        public ServiceResponseBase()
        {
            StatusCode = BusinessStatusCodeEnum.None;
        }

        public ServiceResponseBase(BusinessStatusCodeEnum statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
