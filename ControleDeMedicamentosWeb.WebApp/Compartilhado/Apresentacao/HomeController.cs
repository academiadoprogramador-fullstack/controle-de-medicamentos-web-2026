using Microsoft.AspNetCore.Mvc;

namespace ControleDeMedicamentosWeb.WebApp.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
