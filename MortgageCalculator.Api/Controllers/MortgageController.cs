using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MortgageCalculator.Api.Services;
using MortgageCalculator.Api.Helpers;

namespace MortgageCalculator.Api.Controllers
{
    public class MortgageController : ApiController
    {
        //GET: api/Mortgage
        [CacheWebApi(Duration = 1440)] //Cache the response to 24 hours
        public IEnumerable<Dto.Mortgage> Get()
        {
            var mortgageService = new MortgageService();
            return mortgageService.GetAllMortgages().OrderBy(m => m.MortgageType).ThenBy(n => n.InterestRate);
        }

        //GET: api/Mortgage/5
        public Dto.Mortgage Get(int id)
        {
            var mortgageService = new MortgageService();
            return mortgageService.GetAllMortgages().FirstOrDefault(x => x.MortgageId == id);
        }
    }
}
