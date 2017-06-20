using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StockList.HubClient;
using StockLists.Models;
using ServiceDomain;

namespace StockLists.Controllers
{
	public partial class GridController : Controller
	{
	    private HubClient _hc;
		public ActionResult Orders_Read()
		{
		    _hc = new HubClient();
		    _hc.SetupHubProxy(Update);
		    var result = _hc.GetMessage();
			return Json(result);
		}

        private void Update(IEnumerable<Equity> e)
        {
        }
    }
}