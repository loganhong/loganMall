using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Logan.Mall.UI.Models;
using Logan.Mall.Contract.DataContracts;
using Logan.Mall.Contract.ServiceContracts;
using Logan.Mall.ServiceImplement.Business;

namespace Logan.Mall.UI.Controllers
{
    public class ItemInfoController : Controller
    {
        //
        // GET: /ItemInfo/

        public ActionResult ItemInfo()
        {
            var model = new ItemInfoModels();
            IItemService service = new ItemInfoBL();
            model.ItemInfoList = service.GetAll();
            return View(model);
        }

    }
}
