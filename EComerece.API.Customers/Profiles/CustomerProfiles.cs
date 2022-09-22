namespace EComerece.API.Customers.Profiles
{
    public class CustomerProfiles: AutoMapper.Profile
    {
        public CustomerProfiles()
        {
            CreateMap<DB.Customer, Models.Customer>();
        }
    }
}
