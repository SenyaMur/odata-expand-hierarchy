using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using OdataExample;
using Xunit;
// using SoftWell.Ctor.Api.Admin.IntegrationTests.Helpers;

namespace ODataExampleTests.IntegrationTests {

public static class JsonHelpers {
    public static async Task<List<T>> GetValues<T>(this HttpResponseMessage response) {
      var content = await response.Content.ReadAsStringAsync();
      var obj = JObject.Parse(content);
      var value = obj.Property("value");
      var result = value.Value.ToObject(typeof(List<T>)) as List<T>;
      return result;
    }
}
  public class BasicBooksTests
    : IClassFixture<WebApplicationFactory<Startup>> {
      private readonly WebApplicationFactory<Startup> _factory;

      public BasicBooksTests(WebApplicationFactory<Startup> factory) {
        _factory = factory;
      }

      [Theory]
      [InlineData("InstrumentTypes")]            
      [InlineData("Bullions")]      
      [InlineData("Precious")]      
      [InlineData("Payments")]      
      [InlineData("Precious?$expand=*")]      
      [InlineData("Payments?$expand=instrument($expand=OdataExample.Bullion/baseInstrument)")]      
      public async Task TestResultSuccess(string query) {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"odata/{query}");
        
        // Assert
        var result = response.EnsureSuccessStatusCode();
        // Assert.Equal(System.Net.HttpStatusCode.OK,response.StatusCode);
      }

      [Fact]
      public async Task TestResultExpandInstrumentInPayment() {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"odata/Payments?$expand=instrument");

        // Assert
        var result = response.EnsureSuccessStatusCode();        
        var lst =response.GetValues<PaymentApiModel>().Result;
        Assert.Equal("application/json; odata.metadata=minimal; odata.streaming=true; charset=utf-8", response.Content.Headers.ContentType.ToString());
        Assert.Equal(2,lst.Count);
        Assert.Equal(typeof(Bullion),lst.FirstOrDefault(x=>x.Id == 2).Instrument.GetType());
        Assert.Equal(1000,(lst.FirstOrDefault(x=>x.Id == 2).Instrument as Bullion).LigatureMass);
      }
    }
}