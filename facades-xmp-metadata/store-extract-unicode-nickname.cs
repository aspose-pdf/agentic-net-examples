using System;
using System.IO;
using Aspose.Pdf;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced
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

namespace AsposePdfFacadesTests
{
    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // The test runner will ignore this method.
    public static class Program
    {
        public static void Main() { /* No‑op */ }
    }

    [TestFixture]
    public class UnicodeNicknameTests
    {
        private const string CustomPropertyName = "Nickname";
        private const string UnicodeNickname = "Jürgen 🚀";

        private string CreateSamplePdf()
        {
            // Create a temporary PDF file with a single blank page
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            using (Document doc = new Document())
            {
                // Add an empty page
                doc.Pages.Add();
                // Save the PDF
                doc.Save(tempPath);
            }
            return tempPath;
        }

        [Test]
        public void NicknameUnicode_IsStoredAndRetrievedCorrectly()
        {
            // Arrange: create a PDF and set the custom property using Document.Info indexer
            string pdfPath = CreateSamplePdf();

            try
            {
                // Set the Unicode nickname
                using (Document doc = new Document(pdfPath))
                {
                    doc.Info[CustomPropertyName] = UnicodeNickname;
                    doc.Save(pdfPath);
                }

                // Act: read the custom property back
                string retrievedNickname;
                using (Document readDoc = new Document(pdfPath))
                {
                    retrievedNickname = readDoc.Info[CustomPropertyName];
                }

                // Assert: the retrieved value matches the original Unicode string
                Assert.AreEqual(UnicodeNickname, retrievedNickname,
                    $"The custom property '{CustomPropertyName}' should retain its Unicode value.");
            }
            finally
            {
                // Cleanup temporary file
                if (File.Exists(pdfPath))
                {
                    File.Delete(pdfPath);
                }
            }
        }
    }
}
