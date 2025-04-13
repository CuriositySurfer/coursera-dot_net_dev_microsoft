using System;
using Microsoft.AspNetCore.Mvc;
using ApiModels;
using ApiRepositories;

namespace ApiControllers
{
    [ApiController]
    [Route("api/user-profiles")]
    public class UserProfilesController : ControllerBase
    {
        [HttpGet]
        public IActionResult FetchAllProfiles()
        {
            var profiles = ProfileDataStore.GetAll();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        public IActionResult FetchProfileById(int id)
        {
            var user = ProfileDataStore.GetById(id);
            if (user == null)
                return NotFound(new { message = $"No user found with ID {id}." });

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateProfile([FromBody] UserProfile newUser)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(newUser.Name) || string.IsNullOrWhiteSpace(newUser.Email))
                return BadRequest(new { message = "Please provide valid Name and Email." });

            ProfileDataStore.Add(newUser);
            return CreatedAtAction(nameof(FetchProfileById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateExistingProfile(int id, [FromBody] UserProfile updatedUser)
        {
            var current = ProfileDataStore.GetById(id);
            if (current == null)
                return NotFound(new { message = $"User with ID {id} not found for update." });

            updatedUser.Id = id;
            ProfileDataStore.Update(updatedUser);
            return Ok(new { message = "Profile updated successfully." });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProfile(int id)
        {
            if (ProfileDataStore.GetById(id) == null)
                return NotFound(new { message = $"User with ID {id} does not exist." });

            ProfileDataStore.Delete(id);
            return Ok(new { message = $"User with ID {id} deleted successfully." });
        }
    }
}
