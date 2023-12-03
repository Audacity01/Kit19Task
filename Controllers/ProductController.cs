using kit19Task.DAL;
using kit19Task.Models;
using Microsoft.AspNetCore.Mvc;

public class ProductController : Controller
{
    private readonly ProductRepository productRepository;

    // Inject the ProductRepository using the constructor
    public ProductController(ProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public ActionResult Index()
    {
        // Return the view for the search page.
        return View();
    }

    [HttpPost]
    public ActionResult SearchProducts(ProductSearchModel productSearchModel)
    {
        // Call the data access layer to retrieve search results.
        List<ProductSearchModel> searchResults = productRepository.SearchProducts(productSearchModel);

        // Pass the results to the view.
        return View("SearchResults", searchResults);
    }
}
