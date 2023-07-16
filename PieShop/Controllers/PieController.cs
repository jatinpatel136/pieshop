using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRespository;
        private readonly ICategoryRepository _categoryRespository;

       public PieController(IPieRepository pieRepository, ICategoryRepository categoryRespository)
        {
            _pieRespository= pieRepository;
            _categoryRespository= categoryRespository;
        }

        //public ViewResult List()
        //{
        //    PieListViewModel pieListViewModel = new PieListViewModel(_pieRespository.AllPies, "All Pies");
        //    return View(pieListViewModel);
        //}

        public ViewResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;
            
            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRespository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All Pies";
            } 
            else
            {
                pies = _pieRespository.AllPies.Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
                currentCategory = _categoryRespository.AllCategories.FirstOrDefault(c=>c.CategoryName== category)?.CategoryName;
            }

            return View(new PieListViewModel(pies, currentCategory));
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRespository.GetPieById(id);

            if (pie == null)
                return NotFound();

            return View(pie);

        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
