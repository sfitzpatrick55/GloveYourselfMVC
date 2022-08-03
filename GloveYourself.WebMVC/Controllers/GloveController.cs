﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloveYourself.Models.Glove;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GloveYourself.WebMVC.Controllers
{
    [Authorize]
    public class GloveController : Controller
    {
        //
        // GET: /<controller>/
        public IActionResult Index()
        {
            var model = new GloveListItem[0];

            return View();
        }

        //
        // GET: /glove/create
        public IActionResult Create()
        {
            return View();
        }
    }
}

