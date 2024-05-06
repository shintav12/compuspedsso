using System;

namespace CompuSPED.Common.Base
{
    public class ServiceResponse<T>
    {
        public T Result { get; set; }
        public bool HasError { get; set; }
        public int ErrorCode { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage { get; set; }
    }
}
