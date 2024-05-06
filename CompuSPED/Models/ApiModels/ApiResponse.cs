namespace CompuSPED.Models
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }

        public ApiResponse()
        {
            HasError = false;
        }
    }
}