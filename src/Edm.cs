using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace OdataExample {
  /// <summary>
  /// Класс настройки OData Entity Data Model
  /// </summary>
  public class AllConfigurations : IModelConfiguration {
    /// <summary>
    /// Метод настройки OData Entity Data Model
    /// </summary>    
    public void Apply (ODataModelBuilder builder, ApiVersion apiVersion) {

      builder.EntitySet<InstrumentType> ("InstrumentTypes");
      builder.EntitySet<Instrument> ("Instruments");
      builder.EntitySet<Precious> ("Precious");
      builder.EntitySet<Bullion> ("Bullions");
      builder.EntitySet<PaymentApiModel> ("Payments");

    }
  }
}