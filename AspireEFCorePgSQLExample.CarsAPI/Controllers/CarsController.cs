using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspireEFCorePgSQLExample.CarsAPI.Controllers;
public class CarsController : Controller
{
    // GET: CarsController
    public ActionResult Index()
    {
        return View();
    }

    // GET: CarsController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: CarsController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: CarsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CarsController/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: CarsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: CarsController/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: CarsController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}
