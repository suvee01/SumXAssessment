namespace SumXAssginment.Application.Helper
{
    public class ResponseStatus<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
