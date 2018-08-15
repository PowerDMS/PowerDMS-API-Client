namespace PowerDms.Api.Rest.Dto
{
    public class ServiceResponseDto<T>
    {
        public T Data { get; set; }

        public string Error { get; set; }
    }
}