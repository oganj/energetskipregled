using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using EnergetskiPregled.Models;
using EnergetskiPregled.Contracts.Service;
using Microsoft.AspNetCore.Http;

namespace EnergetskiPregled.Controllers
{
	public class BaseMvcController : Controller
	{
		private readonly IUserService _userService;

		public BaseMvcController(IUserService userService)
		{
			if (userService == null) throw new ArgumentNullException("userService");
			_userService = userService;
		}

		protected async Task<ApplicationUser> GetUserAsync()
		{
			return await _userService.GetByUsernameAsync(User.Identity.Name);
		}

		protected ApplicationUser GetUser()
		{
			return _userService.GetByUsername(User.Identity.Name);
		}
	}
}
