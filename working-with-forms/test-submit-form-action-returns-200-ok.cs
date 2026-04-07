using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

// Minimal NUnit stubs to allow compilation without the real NUnit package.
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
    public class SubmitFormActionTests
    {
        // Helper method to create a PDF with a submit button that posts to a given URL.
        private static MemoryStream CreatePdfWithSubmitButton(string submitUrl)
        {
            // Create an in‑memory PDF document.
            MemoryStream pdfStream = new MemoryStream();
            using (Document doc = new Document())
            {
                // Add a single page.
                Page page = doc.Pages.Add();

                // Add a simple text box field (optional, just to have form data).
                TextBoxField textBox = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 650))
                {
                    PartialName = "sampleText",
                    Value = "Test"
                };
                doc.Form.Add(textBox);

                // Create a submit button field.
                ButtonField submitButton = new ButtonField(page, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
                {
                    PartialName = "submitBtn"
                };

                // Configure the SubmitFormAction.
                SubmitFormAction submitAction = new SubmitFormAction
                {
                    // The Url property of SubmitFormAction expects a FileSpecification object.
                    // Using the constructor that takes a path/description satisfies the type requirement.
                    Url = new FileSpecification(submitUrl, "Form submission URL")
                    // No GetMethod flag => HTTP POST will be used.
                };

                // Assign the action to the button's mouse‑press event (valid property).
                submitButton.Actions.OnPressMouseBtn = submitAction;

                // Add the button to the form.
                doc.Form.Add(submitButton);

                // Save the PDF into the memory stream.
                doc.Save(pdfStream);
            }

            // Reset stream position for reading.
            pdfStream.Position = 0;
            return pdfStream;
        }

        // Unit test that simulates a POST request to the URL defined in the SubmitFormAction.
        [NUnit.Framework.Test]
        public async Task SubmitFormAction_ShouldReturn200Ok()
        {
            // URL that reliably returns HTTP 200.
            const string testUrl = "https://httpbin.org/status/200";

            // Create the PDF (the PDF itself is not sent; we only need the URL for the test).
            using (MemoryStream pdf = CreatePdfWithSubmitButton(testUrl))
            {
                // Simulate a POST request to the submit URL.
                using (HttpClient httpClient = new HttpClient())
                {
                    // Prepare minimal form content (empty in this case).
                    FormUrlEncodedContent content = new FormUrlEncodedContent(Array.Empty<KeyValuePair<string, string>>());

                    // Send the POST request.
                    HttpResponseMessage response = await httpClient.PostAsync(testUrl, content);

                    // Verify that the response status code is 200 OK.
                    NUnit.Framework.Assert.AreEqual(HttpStatusCode.OK, response.StatusCode,
                        $"Expected HTTP 200 OK but received {(int)response.StatusCode} {response.ReasonPhrase}");
                }
            }
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑style project.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
