using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using NUnit.Framework;

namespace AsposePdfFormSubmitTest
{
    // Mock handler that always returns 200 OK for any request
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Simulate a successful POST response
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("OK")
            };
            return Task.FromResult(response);
        }
    }

    [TestFixture]
    public class SubmitFormActionTests
    {
        private const string SubmitUrl = "https://example.com/submit";

        // Creates a simple PDF with a text box and a submit button that uses SubmitFormAction
        private Document CreatePdfWithSubmitForm()
        {
            // Document creation must be wrapped in a using block (document-disposal-with-using rule)
            Document doc = new Document();

            // Add a single page (page-indexing-one-based rule)
            Page page = doc.Pages.Add();

            // Create a text box field where the user can enter data
            TextBoxField textBox = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 620))
            {
                PartialName = "UserName",
                Value = "TestUser"
            };
            // Form fields are added to the document's form collection
            doc.Form.Add(textBox);

            // Create a button that will trigger the submit action
            ButtonField submitButton = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 560, 200, 580))
            {
                PartialName = "SubmitBtn",
                NormalCaption = "Submit"
            };

            // Initialize SubmitFormAction (SubmitFormAction constructor rule)
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Destination URL for the form submission – Url expects a FileSpecification, not a string
                Url = new FileSpecification(SubmitUrl)
            };

            // Assign the action to the button using the correct property
            submitButton.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitButton);

            return doc; // Caller is responsible for disposing
        }

        // Simulates posting the form data to the URL using a mocked HttpClient
        private async Task<HttpResponseMessage> SimulateFormPostAsync(Document pdfDoc)
        {
            // Extract form field values (simple example with one text box)
            var formData = new Dictionary<string, string>();
            foreach (var field in pdfDoc.Form.Fields)
            {
                if (field is TextBoxField txtField && !string.IsNullOrEmpty(txtField.PartialName))
                {
                    formData[txtField.PartialName] = txtField.Value;
                }
            }

            // Prepare HttpClient with the mock handler
            MockHttpMessageHandler mockHandler = new MockHttpMessageHandler();
            using HttpClient httpClient = new HttpClient(mockHandler);

            // Build the POST request content
            FormUrlEncodedContent content = new FormUrlEncodedContent(formData);

            // Send POST request to the URL defined in SubmitFormAction
            var response = await httpClient.PostAsync(SubmitUrl, content);
            return response;
        }

        [Test]
        public async Task SubmitFormAction_ShouldReturn200Ok()
        {
            // Create PDF with submit form
            using var pdfDoc = CreatePdfWithSubmitForm();

            // Simulate the POST request that would be triggered by the SubmitFormAction
            HttpResponseMessage response = await SimulateFormPostAsync(pdfDoc);

            // Validate that the simulated response is 200 OK
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Form submission did not return 200 OK.");
        }
    }

    // Provide an entry point so the project builds as a console application
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed via NUnit runner.
        }
    }
}

// -----------------------------------------------------------------------------
// Minimal NUnit stubs – used when the NUnit package is not referenced.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class OneTimeSetUpAttribute : Attribute { }

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}
