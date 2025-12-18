using projectApiAngular.Services;

namespace projectApiAngular.Controllers
{
    public class DonnerController
    {
       private readonly IDonnerService _donnerService;

         public DonnerController(IDonnerService donnerService)
         {
              _donnerService = donnerService;
         }


    }
}
