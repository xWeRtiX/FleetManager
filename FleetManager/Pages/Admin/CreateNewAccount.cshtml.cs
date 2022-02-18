using FleetManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FleetManager.Pages.Admin
{
    public class CreateNewAccountModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Role { get; set; }
        }

        public CreateNewAccountModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid) return StatusCode(500); // TODO: vrátit chyby

            var user = new AppUser
            {
                UserName = Input.Username,
                Email = Input.Email,
                FirstName = Input.FirstName,
                LastName = Input.LastName
            };

            var res = await _userManager.CreateAsync(user, Input.Password);

            if (!res.Succeeded) return StatusCode(500); // TODO: vrátit chyby

            res = await _userManager.AddToRoleAsync(user, Input.Role);

            if (!res.Succeeded) return StatusCode(500); // TODO: vrátit chyby

            return Page();
        }

    }
}
