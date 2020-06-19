using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Xunit;

namespace XUnitTestProject1
{
    public class SampleTest: IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public SampleTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public void TestGetStockItemsAsync()
        {
            //AppContext.SetSwitch("System.Net.Http.UseSocketsHttpHandler", false);
            ////AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var channel = GrpcChannel.ForAddress("http://localhost:5000",
                 new GrpcChannelOptions { HttpClient = Client });
            var grpcClient = new CompanyGrpcService.CompanyGrpcServiceClient(channel);
            var reply = grpcClient.GetCompanyById(new CompanyRequest { Id = 1 });
            Assert.Equal(reply.Name, "");
        }

        [Fact]
        public async Task TestGetStockItems1Async()
        {
            var response = await Client.GetAsync("companies/1");
            var body = response.Content.ReadAsStringAsync();
        }
    }
}
