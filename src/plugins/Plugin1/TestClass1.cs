using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Plugin1;
 
public class TestClass1
{
    public static object GetTestObject()
    {
        var optionsMonitorMock = Substitute.For<IOptionsMonitor<JwtBearerOptions>>();
        optionsMonitorMock.CurrentValue.Returns(new JwtBearerOptions
        {
        });
        
        var handler = new JwtBearerHandler(optionsMonitorMock, new LoggerFactory(), UrlEncoder.Default, new SystemClock());
        return handler;
    }
}