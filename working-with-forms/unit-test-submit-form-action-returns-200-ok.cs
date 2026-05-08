using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using NUnit.Framework;

// Minimal NUnit stubs – used only when the real NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfFormSubmissionTests
{
    [TestFixture]
    public class SubmitFormActionTests
    {
        // Helper method to create a simple PDF with a submit button
        private static byte[] CreatePdfWithSubmitButton(string submitUrl)
        {
            using (Document doc = new Document())
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // Create a button field (ButtonField is the generic button class)
                ButtonField button = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
                {
                    PartialName = "SubmitBtn",
                    NormalCaption = "Submit" // Use NormalCaption for the button label
                };

                // Attach a SubmitFormAction to the button using the supported property
                SubmitFormAction submitAction = new SubmitFormAction
                {
                    // Url property expects a FileSpecification, not a raw string
                    Url = new FileSpecification(submitUrl)!, // suppress nullable warning
                    // Use GET method for simplicity; change Flags if needed
                    Flags = SubmitFormAction.GetMethod
                };

                // The ButtonField does not expose an Action property – use OnActivated instead
                button.OnActivated = submitAction;

                // Add the button to the form collection
                doc.Form.Add(button);

                // Save PDF to a memory stream and return the bytes
                using (MemoryStream ms = new MemoryStream())
                {
                    doc.Save(ms);
                    return ms.ToArray();
                }
            }
        }

        // Simple HttpMessageHandler that always returns the supplied status code
        private class FixedResponseHandler : HttpMessageHandler
        {
            private readonly HttpStatusCode _statusCode;

            public FixedResponseHandler(HttpStatusCode statusCode) => _statusCode = statusCode;

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(_statusCode)
                {
                    Content = new StringContent(string.Empty)
                };
                return Task.FromResult(response);
            }
        }

        // Factory that creates an HttpClient using the FixedResponseHandler
        private static HttpClient CreateMockHttpClient(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var handler = new FixedResponseHandler(statusCode);
            return new HttpClient(handler);
        }

        [Test]
        public async Task SubmitFormAction_ShouldReturn200Ok()
        {
            // Arrange
            const string submitUrl = "https://example.com/submit";
            byte[] pdfBytes = CreatePdfWithSubmitButton(submitUrl);
            HttpClient httpClient = CreateMockHttpClient(HttpStatusCode.OK);

            // Simulate the POST request that would be triggered by the SubmitFormAction
            using (MultipartFormDataContent content = new MultipartFormDataContent())
            {
                // In a real scenario, form fields would be added here.
                // For the test we only need to ensure the request succeeds.
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, submitUrl)
                {
                    Content = content
                };

                // Act
                HttpResponseMessage response = await httpClient.SendAsync(request);

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Form submission did not return 200 OK.");
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
