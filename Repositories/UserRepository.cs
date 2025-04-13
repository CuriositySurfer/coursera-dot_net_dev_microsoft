using System.Collections.Generic;
using System.Linq;
using ApiModels;

namespace ApiRepositories
{
    public static class ProfileDataStore
    {
        private static List<UserProfile> _userProfiles = new List<UserProfile>();
        
        public static UserProfile GetById(int id) => _userProfiles.FirstOrDefault(profile => profile.Id == id);
        
        public static List<UserProfile> GetAll() => _userProfiles;
        
        public static void Add(UserProfile profile) => _userProfiles.Add(profile);
        
        public static void Delete(int id) => _userProfiles.RemoveAll(profile => profile.Id == id);
        
        public static void Update(UserProfile profile)
        {
            var index = _userProfiles.FindIndex(existingProfile => existingProfile.Id == profile.Id);
            if (index >= 0) _userProfiles[index] = profile;
        }
    }
}