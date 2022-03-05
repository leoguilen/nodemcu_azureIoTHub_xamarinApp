using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.PollyPolices
{
    public class PolicyHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return PolicyUtil.PolicyStrategy
                .ExecuteAsync(ct => base.SendAsync(request, ct), cancellationToken);
        }
    }
}
