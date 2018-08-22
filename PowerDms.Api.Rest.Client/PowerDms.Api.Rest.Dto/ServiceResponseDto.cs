namespace PowerDms.Api.Rest.Dto
{
    public class ServiceResponseDto<T>
    {
        public T Data { get; set; }

        public ErrorDto Error { get; set; }

        public bool IsSuccessful => Error == null;
    }

    /// <summary>
    /// Response for request that does not return data
    /// </summary>
    public class ServiceResponseDto
    {
        public ErrorDto Error { get; set; }

        public bool IsSuccessful => Error == null;
    }

}