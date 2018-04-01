using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MortgageCalculator.UnitTests
{
    [TestFixture]
    public class HttpResponseTests
    {
        private HttpClient client;

        private HttpResponseMessage response;

        [SetUp]
        public void SetUP()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:49608/");
            response = client.GetAsync("api/Mortgage").Result;
        }

        //Test HTTP response
        [Test]
        public void GetResponseIsSuccess()
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        //Test HTTP response not null
        [Test]
        public void GetResponseContentNotNullCheck()
        {
            Assert.NotNull(response.Content);
        }

        //Test response content should be not json
        [Test]
        public void GetResponseIsJson()
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Assert.AreEqual("application/json", response.Content.Headers.ContentType.MediaType);
        }

        //Test response cache
        [Test]
        public void ValidateCaching()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(false, response.Headers.CacheControl.NoCache);
                Assert.AreEqual(1440, response.Headers.CacheControl.MaxAge.Value.TotalMinutes);
            });
        }
    }
}
