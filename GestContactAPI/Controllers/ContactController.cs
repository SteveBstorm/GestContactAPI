﻿using BusinessLogicLayer.Interfaces;
using GestContactAPI.Models;
using GestContactAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestContactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private IContactClientRepo _contactServices;

        public ContactController(IContactClientRepo contactServices)
        {
            _contactServices = contactServices;
        }

        [Authorize("isConnected")]
        [HttpGet("UserContact")]
        public IActionResult GetByUserId()
        {
            ClaimsPrincipal cp = HttpContext.User;
            string Id = cp.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Sid).Value;

            return Ok(_contactServices.Get(int.Parse(Id)));
        }

        [HttpGet("ContactDetail/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_contactServices.GetById(id));
        }

        [Authorize("isConnected")]
        [HttpPost]
        public IActionResult Register(ContactForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _contactServices.Insert(form.toBll());
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contactServices.Delete(2, id);
            return Ok();
        }


        [HttpPut]
        public IActionResult Update(UpdateContactForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            _contactServices.Update(form.Id, form.toBll());
            return Ok();
        }
    }
}
