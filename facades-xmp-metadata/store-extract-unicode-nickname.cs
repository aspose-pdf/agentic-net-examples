using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class UnicodeNicknameTests
    {
        // Unicode nickname to test
        private const string Nickname = "测试昵称🌟";

        // Helper to create a simple PDF containing the nickname text
        private static string CreatePdfWithNickname()
        {
            // Create a temporary file path with .pdf extension
            string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            // Use Aspose.Pdf core API to create the document
            using (Document doc = new Document())
            {
                // Add a page
                Page page = doc.Pages.Add();

                // Create a text fragment with the Unicode nickname
                TextFragment fragment = new TextFragment(Nickname);
                // Optional: set a readable font that supports Unicode
                fragment.TextState.Font = FontRepository.FindFont("Arial Unicode MS");
                fragment.TextState.FontSize = 14;

                // Add the fragment to the page
                page.Paragraphs.Add(fragment);

                // Save the document (lifecycle rule: use using and doc.Save)
                doc.Save(pdfPath);
            }

            return pdfPath;
        }

        // Helper to extract text from a PDF using Aspose.Pdf.Facades.PdfExtractor
        private static string ExtractTextFromPdf(string pdfPath)
        {
            // Create a temporary file for extracted text
            string txtPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".txt");

            // Use PdfExtractor (facade) to extract Unicode text
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfPath);
            extractor.ExtractText();               // extracts using Unicode encoding by default
            extractor.GetText(txtPath);            // writes extracted text to file

            // Read the extracted text back into a string
            string extracted = File.ReadAllText(txtPath);
            // Clean up the temporary text file
            File.Delete(txtPath);
            return extracted;
        }

        [Test]
        public void Nickname_WithUnicode_IsStoredAndExtractedCorrectly()
        {
            // Arrange: create a PDF containing the Unicode nickname
            string pdfPath = CreatePdfWithNickname();

            try
            {
                // Act: extract text from the created PDF
                string extractedText = ExtractTextFromPdf(pdfPath);

                // Assert: the extracted text must contain the exact Unicode nickname
                Assert.IsTrue(extractedText.Contains(Nickname),
                    $"Extracted text does not contain the expected Unicode nickname. Expected: '{Nickname}'. Extracted: '{extractedText}'.");
            }
            finally
            {
                // Clean up the temporary PDF file
                if (File.Exists(pdfPath))
                {
                    File.Delete(pdfPath);
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑type project.
public static class Program
{
    public static void Main() { /* No‑op – tests are executed by the test runner */ }
}