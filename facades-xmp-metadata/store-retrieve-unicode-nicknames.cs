using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

namespace AsposePdfFacadesTests
{
    [TestFixture]
    public class NicknameUnicodeTests
    {
        private const string TestFolder = "TestOutput";

        // Helper to create a PDF with a TextAnnotation containing the supplied nickname
        private static string CreatePdfWithNickname(string nickname)
        {
            Directory.CreateDirectory(TestFolder);
            string pdfPath = Path.Combine(TestFolder, $"Nickname_{Guid.NewGuid()}.pdf");

            // Create a simple PDF document
            using (Document doc = new Document())
            {
                // Add a blank page
                Page page = doc.Pages.Add();

                // Define the rectangle where the annotation will be placed
                // Fully qualified to avoid ambiguity
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 400, 550);

                // Create a TextAnnotation that holds the nickname as its contents
                TextAnnotation annotation = new TextAnnotation(page, rect)
                {
                    Title = "Nickname",
                    Contents = nickname,
                    // Use a visible color so the annotation is rendered
                    Color = Aspose.Pdf.Color.Yellow,
                    // Open the annotation by default
                    Open = true
                };

                // Add the annotation to the page
                page.Annotations.Add(annotation);

                // Save the PDF
                doc.Save(pdfPath);
            }

            return pdfPath;
        }

        // Helper to extract all text from a PDF using PdfExtractor (Facades API)
        private static string ExtractAllText(string pdfPath)
        {
            // Bind the PDF to the extractor
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(pdfPath);

            // Extract text using Unicode encoding
            extractor.ExtractText(Encoding.Unicode);

            // Retrieve the extracted text into a memory stream
            using (MemoryStream ms = new MemoryStream())
            {
                extractor.GetText(ms);
                ms.Position = 0;
                using (StreamReader reader = new StreamReader(ms))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        [Test]
        public void Nickname_WithChineseCharacters_IsStoredCorrectly()
        {
            // Unicode nickname containing Chinese characters
            string nickname = "测试昵称";

            // Create PDF with the nickname
            string pdfPath = CreatePdfWithNickname(nickname);

            // Extract text from the created PDF
            string extractedText = ExtractAllText(pdfPath);

            // The extracted text should contain the nickname
            Assert.IsTrue(extractedText.Contains(nickname),
                $"Extracted text does not contain the expected nickname. Extracted: \"{extractedText}\"");
        }

        [Test]
        public void Nickname_WithEmoji_IsStoredCorrectly()
        {
            // Unicode nickname containing an emoji
            string nickname = "User🚀";

            // Create PDF with the nickname
            string pdfPath = CreatePdfWithNickname(nickname);

            // Extract text from the created PDF
            string extractedText = ExtractAllText(pdfPath);

            // The extracted text should contain the nickname
            Assert.IsTrue(extractedText.Contains(nickname),
                $"Extracted text does not contain the expected nickname. Extracted: \"{extractedText}\"");
        }
    }

    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No-op – tests are executed via the NUnit runner.
        }
    }
}

// -----------------------------------------------------------------------------
// Minimal NUnit stubs to allow compilation when the real NUnit package is not
// referenced. These provide the attributes and the Assert class used in the
// tests above.
// -----------------------------------------------------------------------------
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        // Made the message parameter nullable to avoid CS8625 warnings.
        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }
    }
}
