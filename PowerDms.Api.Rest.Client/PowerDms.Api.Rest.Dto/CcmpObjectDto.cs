namespace PowerDms.Api.Rest.Dto
{
    using System;

    public class CcmpObjectDto : IEquatable<CcmpObjectDto>
    {
        public string Id { get; set; }

        public string SiteId { get; set; }

        public string Name { get; set; }

        public virtual ObjectTypes ObjectType { get; set; }

        public string ExternalId { get; set; }

        public bool Equals(CcmpObjectDto other)
        {
            return Id == other.Id && ObjectType == other.ObjectType;
        }
    }
}