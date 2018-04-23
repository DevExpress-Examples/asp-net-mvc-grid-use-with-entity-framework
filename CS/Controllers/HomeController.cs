using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoredProcedures.Models;
using DevExpress.Web.Mvc;

namespace StoredProcedures.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "The example demonstrates how to bind the grid to a Stored Procedure result.<br /> All changes that are cleared after 10 minutes.";

            /* Clean 10-min old records */
            StoredProcedureEntities context = new StoredProcedureEntities();

            DateTime oldDate = DateTime.Now.AddMinutes(-10.0);

            var supportTeamEntities = context.SupportTeams
                .OrderBy(i => i.Id)
                .Skip(4)
                .Where(i => i.ChangeDate < oldDate)                
                .ToList();

            foreach (SupportTeam item in supportTeamEntities)
                context.DeleteObject(item);
            context.SaveChanges();

            return View();
        }

        public ActionResult SupportGridPartial() {
            StoredProcedureEntities context = new StoredProcedureEntities();
            var model = context.SelectSupportTeam();

            return PartialView("SupportGridPartial", model);
        }

        [HttpPost]
        public ActionResult SupportGridInsert([ModelBinder(typeof(DevExpressEditorsBinder))] SelectSupportTeam_Result supportEngineer) {
            StoredProcedureEntities context = new StoredProcedureEntities();
            var result = context.InsertSupportEngineer(supportEngineer.Name).First(); // inserted key value

            if (result.HasValue)
                TempData["message"] = "Inserted row key: " + result.Value.ToString();

            var model = context.SelectSupportTeam();

            return PartialView("SupportGridPartial", model);
        }

        [HttpPost]
        public ActionResult SupportGridDelete(Int32 Id) {
            StoredProcedureEntities context = new StoredProcedureEntities();

            /* it is not allowed to delete some initial records */
            Int32 foundItem = context.SupportTeams
                .Take(4)
                .Where(i => i.Id == Id)
                .Count();

            if (foundItem != 0)
                TempData["message"] = "Error: It is not possible to delete test records";
            else {
                var result = context.DeleteSupportEngineer(Id).First(); // a number of deleted rows (usually 1)

                if (result.HasValue)
                    TempData["message"] = "A number of deleted rows: " + result.Value.ToString();
            }

            var model = context.SelectSupportTeam();

            return PartialView("SupportGridPartial", model);
        }
    }
}
