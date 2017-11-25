namespace PowerDms.Api.Rest.Dto
{
    public class GroupDto : PrincipalDto
    {
        public int MembersCount { get; set; }

        public string Location { get; set; }

        public string JobTitle { get; set; }

        public bool? AutoAddNewUsers { get; set; }

        public bool? IsExternallySynced { get; set; }

        public override ObjectTypes ObjectType => ObjectTypes.Group;

        public override string GroupId => Id;
    }
}