using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Contracts;
using Entities;
using Entities.Enumerations;
using Entities.Models;
using Entities.Extensions;

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repoWrapper;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _repoWrapper.Account.GetAllAccounts();
    
                _logger.LogInfo($"Returned all accounts from database.");
    
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody]Account account)
        {
            try
            {
                if (account.IsObjectNull())
                {
                    _logger.LogError("Object sent from client is null.");
                    return BadRequest("Object is null");
                }
    
                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client.");
                    return BadRequest("Invalid model object");
                }
    
                _repoWrapper.Account.CreateAccount(account);
    
                return CreatedAtRoute("AccountById", new { id = account.Id }, account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccount action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET api/values
        // [HttpGet]
        // public IActionResult Get()
        // {
        //     //var domesticAccounts = _repoWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
        //     var owners = _repoWrapper.Owner.GetAllOwners();

        //     _logger.LogInfo("Here is info message from our values controller.");
        //     _logger.LogDebug("Here is debug message from our values controller.");
        //     _logger.LogWarn("Here is warn message from our values controller.");
        //     _logger.LogError("Here is error message from our values controller.");

        //     return Ok(owners);
        // }
    }
}
