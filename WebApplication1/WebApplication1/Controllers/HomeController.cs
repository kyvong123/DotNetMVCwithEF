using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly JsonSerializer _serializer = new JsonSerializer();
        static readonly HttpClient _client = new HttpClient();
        private readonly ItemContext _context;

        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }

        public HomeController(ILogger<HomeController> logger, ItemContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {


            //return View(deserialized);
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Cookie, "cookievalue");
            var json = webClient.DownloadString(@"https://localhost:44363/Item/GetItem");
            Item[] objJson = JsonConvert.DeserializeObject<Item[]>(json); //here we will map the Json to C# class
                                                                          //here we will return this model to view
            return View(objJson);  //you have to pass model to view
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
             return PartialView("Edit", _context.DbItem.Where(x => x.ItemID == id).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult Add(int? id)
        {
            if (id == 0)
                return PartialView("Add", new Item());
            else
                return PartialView("Add", _context.DbItem.Where(x => x.ItemID == id).FirstOrDefault());
        }

        [HttpGet]
        public IActionResult DeleteConfirm(int? id)
        {
            Item item = _context.DbItem.Where(x => x.ItemID == id).FirstOrDefault();
            return PartialView("DeleteConfirm", item);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Item item = _context.DbItem.Where(x => x.ItemID == id).FirstOrDefault();
             _context.DbItem.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //POST: Home/PostItem
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostItem([Bind("RenderingEngine,Browser,Platform,EngineVersion,CSSGrade")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ItemID == 0)
                {
                    Item item_new = new Item();

                    item_new.RenderingEngine = item.RenderingEngine;
                    item_new.Browser = item.Browser;
                    item_new.CSSGrade = item.CSSGrade;
                    item_new.EngineVersion = item.EngineVersion;
                    item_new.Platform = item.Platform;
                    _context.DbItem.Add(item);
                    await _context.SaveChangesAsync();
                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "Index", item) });

                }

                else
                {
                    _context.DbItem.Update(item);
                    _context.SaveChangesAsync();
                    return Json(new { isValid = false, html = RenderRazorViewToString(this, "Index", item) });
                }    
                    

            }
            return View(item);
        }


        //PUST: Home/PutItem/id
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PutItem(int? id,[Bind("RenderingEngine,Browser,Platform,EngineVersion,CSSGrade")] Item item)
        {
            if (ModelState.IsValid)
            {
                item.ItemID = (int)id;
                _context.DbItem.Update(item);
                await _context.SaveChangesAsync();
                return Json(new { isValid = false, html = RenderRazorViewToString(this, "Index", item) });
            }
            return View(item);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult DataTableLayout()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
