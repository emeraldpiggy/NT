using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using StockList.HubClient;
using ServiceDomain;

namespace StockLists.Controllers
{
	public partial class GridController : Controller
	{
	    private HubClient _hc;
		public ActionResult GetResource()
		{

		    var res = new List<Equity>
		    {
		        new Equity() {Symbol = "a", Price = 1},
		        new Equity() {Symbol = "b", Price = 2},
		        new Equity() {Symbol = "c", Price = 3}
		    };

		    return Json(res);

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
