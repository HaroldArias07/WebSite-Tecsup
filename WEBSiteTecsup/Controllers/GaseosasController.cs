using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WEBSiteTecsup.Data;
using WEBSiteTecsup.Models;

namespace WEBSiteTecsup.Controllers
{
    public class GaseosasController : Controller
    {
        private WEBSiteTecsupContext db = new WEBSiteTecsupContext();

        // GET: Gaseosas
        public async Task<ActionResult> Index()
        {
            List<Gaseosas> model = new List<Gaseosas>();
            var client = new HttpClient();
            var urlBase = "https://localhost:44315/";
            client.BaseAddress = new Uri(urlBase);

            var url = string.Concat(urlBase, "/Api", "/Gaseosas", "/GetGaseosas");
            var response = client.GetAsync(url).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var result = await response.Content.ReadAsStringAsync();
                //Json to Response
                var gaseosas = JsonConvert.DeserializeObject<List<Gaseosas>>(result);
                model = gaseosas;
            }
            return View(model);
        }

        // GET: Gaseosas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gaseosas gaseosas = db.Gaseosas.Find(id);
            if (gaseosas == null)
            {
                return HttpNotFound();
            }
            return View(gaseosas);
        }

        // GET: Gaseosas/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Gaseosas model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");

                var client = new HttpClient();
                var urlBase = "https://localhost:44315";
                client.BaseAddress = new Uri(urlBase);
                var url = string.Concat(urlBase, "/Api", "/Gaseosas" , "/PostGaseosas");

                var response = client.PostAsync(url, content).Result;

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    var result = await response.Content.ReadAsStringAsync();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Gaseosas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gaseosas gaseosas = db.Gaseosas.Find(id);
            if (gaseosas == null)
            {
                return HttpNotFound();
            }
            return View(gaseosas);
        }

        // POST: Gaseosas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GaseosaID,GaseosaNombre,GaseosaMarca")] Gaseosas gaseosas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gaseosas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gaseosas);
        }

        // GET: Gaseosas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gaseosas gaseosas = db.Gaseosas.Find(id);
            if (gaseosas == null)
            {
                return HttpNotFound();
            }
            return View(gaseosas);
        }

        // POST: Gaseosas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gaseosas gaseosas = db.Gaseosas.Find(id);
            db.Gaseosas.Remove(gaseosas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
