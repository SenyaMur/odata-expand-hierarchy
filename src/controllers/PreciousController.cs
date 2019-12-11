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
  [ODataRoutePrefix("Precious")]
  public class PreciousController : ODataController {    
    protected readonly Store _context;    
    public PreciousController(Store context) {
      _context = context;      
    }


    [EnableQuery(AllowedQueryOptions =
      AllowedQueryOptions.All, MaxExpansionDepth = 10, MaxAnyAllExpressionDepth = 10
    )]
    [HttpGet]
    public IQueryable<Precious> Get() => _context.Precious;

  }
}