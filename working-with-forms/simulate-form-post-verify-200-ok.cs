using System;
using System.Net.Http;
using System.Threading.Tasks;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Made the message parameter nullable to satisfy non‑nullable reference type analysis
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace FormSubmissionTests
{
    [NUnit.Framework.TestFixture]
    public class PostRequestTests
    {
        private static readonly string FormUrl = "https://example.com/submit"; // replace with actual endpoint

        [NUnit.Framework.Test]
        public async Task SubmitForm_Returns200Ok()
        {
            // Arrange: create HttpClient and form content
            using HttpClient httpClient = new HttpClient();

            MultipartFormDataContent formData = new MultipartFormDataContent {
                { new StringContent("value1"), "field1" },
                { new StringContent("value2"), "field2" }
                // add additional fields as needed
            };

            // Act: send POST request
            HttpResponseMessage response = await httpClient.PostAsync(FormUrl, formData);

            // Assert: verify that the response status code is 200 OK
            NUnit.Framework.Assert.AreEqual(System.Net.HttpStatusCode.OK, response.StatusCode, "Form submission did not return 200 OK.");
        }
    }
}

// Minimal entry point required for a console‑type project.
public static class Program
{
    public static void Main()
    {
        // No operation – the presence of Main satisfies the compiler.
    }
}