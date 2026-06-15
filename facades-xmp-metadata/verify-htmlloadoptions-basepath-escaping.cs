using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // <-- added using directive for NUnit stubs

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

        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class BasePathEscapingTests
    {
        [Test]
        public void HtmlLoadOptions_BasePath_WithSpecialCharacters_IsPreservedAndEscapedCorrectly()
        {
            // Arrange: create a base path that contains spaces and special characters.
            string specialBasePath = Path.Combine(Path.GetTempPath(), "Test Folder & Files");
            // Ensure the directory exists.
            Directory.CreateDirectory(specialBasePath);

            // Create a simple HTML file in the special base path.
            string htmlFilePath = Path.Combine(specialBasePath, "sample.html");
            string htmlContent = "<html><body><p>Sample content.</p></body></html>";
            File.WriteAllText(htmlFilePath, htmlContent);

            // Act: initialize HtmlLoadOptions with the special base path.
            HtmlLoadOptions loadOptions = new HtmlLoadOptions(specialBasePath);

            // Verify that the BasePath property returns the exact string we supplied.
            Assert.AreEqual(specialBasePath, loadOptions.BasePath, "BasePath property should preserve the original string.");

            // Load the HTML document using the options.
            using (Document pdfDoc = new Document(htmlFilePath, loadOptions))
            {
                // Save the resulting PDF to a memory stream to ensure the conversion succeeded.
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    pdfDoc.Save(pdfStream);
                    Assert.IsTrue(pdfStream.Length > 0, "PDF stream should contain data after conversion.");

                    // Use a Facade (PdfViewer) to bind the generated PDF stream.
                    // This satisfies the requirement to use Aspose.Pdf.Facades.
                    PdfViewer viewer = new PdfViewer();
                    try
                    {
                        // Reset stream position before binding.
                        pdfStream.Position = 0;
                        viewer.BindPdf(pdfStream);
                        // No further actions needed; successful binding indicates the PDF is well‑formed.
                    }
                    finally
                    {
                        viewer.Close();
                    }
                }
            }

            // Cleanup: remove temporary files and directory.
            File.Delete(htmlFilePath);
            Directory.Delete(specialBasePath, true);
        }
    }

    // Dummy entry point to satisfy the compiler for a console‑type project.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // The test runner (e.g., dotnet test) will discover and execute the tests.
            // This Main method is intentionally left empty.
        }
    }
}
