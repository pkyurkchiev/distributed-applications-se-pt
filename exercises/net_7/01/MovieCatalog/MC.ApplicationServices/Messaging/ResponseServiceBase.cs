namespace MC.ApplicationServices.Messaging
{
    public abstract class ResponseServiceBase
    {
        public BusinessStatusCodeEnum StatusCode { get; set; }

        public ResponseServiceBase()
        {
            StatusCode = BusinessStatusCodeEnum.None;
        }
    }
}
