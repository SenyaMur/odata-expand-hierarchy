using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace OdataExample.controllers
{
      
  [Produces("application/json")]
  [ODataRoutePrefix("InstrumentTypes")]
  public class InstrumentTypesController : ODataController {    
    protected readonly Store _context;    
    public InstrumentTypesController(Store context) {
      _context = context;      
    }


    [EnableQuery(AllowedQueryOptions =
      AllowedQueryOptions.All, MaxExpansionDepth = 10, MaxAnyAllExpressionDepth = 10
    )]
    [HttpGet]
    public IQueryable<InstrumentType> Get() => _context.InstrumentTypes;

  }
}