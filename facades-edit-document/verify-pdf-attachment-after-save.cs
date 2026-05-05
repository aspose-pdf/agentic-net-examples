using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the NUnit package
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void IsTrue(bool condition, string message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfAttachmentTests
{
    [NUnit.Framework.TestFixture]
    public class AttachmentTests
    {
        private const string AttachmentFileName = "sample.txt";
        private const string AttachmentContent = "This is a test attachment.";

        // Helper to create a simple PDF with one blank page
        private static string CreateBlankPdf()
        {
            string pdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            using (Document doc = new Document())
            {
                // Add a blank page (Aspose.Pdf adds a default page automatically)
                doc.Save(pdfPath);
            }
            return pdfPath;
        }

        // Helper to create a temporary file that will be attached
        private static string CreateAttachmentFile()
        {
            string filePath = Path.Combine(Path.GetTempPath(), AttachmentFileName);
            File.WriteAllText(filePath, AttachmentContent);
            return filePath;
        }

        [NUnit.Framework.Test]
        public void Attachment_Should_Appear_After_Save()
        {
            // Arrange: create a blank PDF and an attachment file
            string inputPdf = CreateBlankPdf();
            string attachmentFile = CreateAttachmentFile();
            string outputPdf = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // Act: add the attachment using PdfContentEditor and save the result
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(inputPdf);
                    editor.AddDocumentAttachment(attachmentFile, "Test attachment description");
                    editor.Save(outputPdf);
                }

                // Assert: verify that the attachment exists in the saved PDF
                using (PdfExtractor extractor = new PdfExtractor())
                {
                    extractor.BindPdf(outputPdf);
                    extractor.ExtractAttachment(); // Populate attachment information
                    IList<string> attachmentNames = extractor.GetAttachNames(); // Generic IList<string>
                    bool found = false;
                    foreach (string name in attachmentNames)
                    {
                        if (name.Equals(AttachmentFileName, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true;
                            break;
                        }
                    }
                    NUnit.Framework.Assert.IsTrue(found, $"Attachment '{AttachmentFileName}' was not found in the PDF.");
                }
            }
            finally
            {
                // Cleanup temporary files
                if (File.Exists(inputPdf)) File.Delete(inputPdf);
                if (File.Exists(attachmentFile)) File.Delete(attachmentFile);
                if (File.Exists(outputPdf)) File.Delete(outputPdf);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building as an executable
    public static class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required for the unit test library.
        }
    }
}
