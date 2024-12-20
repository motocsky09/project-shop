using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Server.Entities;

namespace Server.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ServerDbContext _serverDbContext;
        public ProfileRepository(ServerDbContext serverDbContext)
        {
            _serverDbContext = serverDbContext;
        }
        
        public List<Profile> GetProfiles() 
        {
            return _serverDbContext.Profile.ToList();
        }
        
        public Profile GetProfileByUserName(string userName)
        {
            return _serverDbContext.Profile.FirstOrDefault(x => x.UserName == userName);
        }
        
        public void CreateProfile(Profile model)
        {
            var profile = new Profile
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                City = model.City,
                Postal = model.Postal
            };
            _serverDbContext.Profile.Add(profile);
            _serverDbContext.SaveChanges();
        }

        public void UpdateProfile(Profile model)
        {
            var existingProfile = _serverDbContext.Profile.FirstOrDefault(p => p.Id == model.Id);
            if (existingProfile != null)
            {
                existingProfile.UserName = model.UserName;
                existingProfile.FirstName = model.FirstName;
                existingProfile.LastName = model.LastName;
                existingProfile.Address = model.Address;
                existingProfile.Email = model.Email;
                existingProfile.PhoneNumber = model.PhoneNumber;
                existingProfile.City = model.City;
                existingProfile.Postal = model.Postal;
                
                _serverDbContext.SaveChanges();
            }
        }

        public void DeleteProfile(int profileId)
        {
            var profileToDelete = _serverDbContext.Profile.FirstOrDefault(p => p.Id == profileId);
            if (profileToDelete != null)
            {
                _serverDbContext.Profile.Remove(profileToDelete);
                _serverDbContext.SaveChanges();
            }
        }

        public Profile CreateDefaultProfile(Profile model)
        {
            if (model.UserName != null && model.Email != null)
            {
                var profile = new Profile
                {
                    UserName = model.UserName,
                    FirstName = "",
                    LastName = "",
                    Address = "",
                    Email = model.Email,
                    PhoneNumber = "",
                    City = "",
                    Postal = ""
                };
                _serverDbContext.Profile.Add(profile);
                _serverDbContext.SaveChanges();
                return profile;
            }

            return null;
        }
    }
}
