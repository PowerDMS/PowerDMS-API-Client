namespace PowerDms.Api.Rest.Dto
{
    public class PrincipalDto : CcmpObjectDto, IPrincipalDto 
    {
        public virtual string GroupId { get; set; }
    }
}
