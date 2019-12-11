using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OdataExample.controllers
{
      
  [Produces("application/json")]
  [ODataRoutePrefix("Bullions")]
  public class BullionsController : ODataController {    
    protected readonly Store _context;    
    public BullionsController(Store context) {
      _context = context;      
    }


    [EnableQuery(AllowedQueryOptions =
      AllowedQueryOptions.All, MaxExpansionDepth = 10, MaxAnyAllExpressionDepth = 10
    )]
    [HttpGet]
    public IQueryable<Bullion> Get() => _context.Bullions;

  }
}