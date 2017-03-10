using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnergetskiPregled.Contracts.Mappers;
using EnergetskiPregled.Contracts.Data;
using EnergetskiPregled.Models;
using EnergetskiPregled.Contracts.Service;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EnergetskiPregled.Controllers.api
{
    public class BaseApiController : Controller
    {
		protected void CreateUnauthorizedResponse(BaseApiResponse res)
		{
			Response.StatusCode = 401;
			res.Ack = 401;
			res.Description = "Unauthorized access!";
		}
	}
}
