using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the real NUnit package
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
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class AttachmentTests
    {
        [NUnit.Framework.Test]
        public void AttachmentIsPresentAfterSave()
        {
            // Arrange: create a simple source PDF
            string tempDir = Path.GetTempPath();
            string sourcePdf = Path.Combine(tempDir, "source.pdf");
            string outputPdf = Path.Combine(tempDir, "output.pdf");
            string attachmentFile = Path.Combine(tempDir, "attachment.txt");

            // Create a dummy attachment file
            File.WriteAllText(attachmentFile, "Sample attachment content");

            // Create a one‑page PDF and save it (using the documented lifecycle rule)
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(sourcePdf);
            }

            // Act: add the attachment using PdfContentEditor (facade) and save
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(sourcePdf);
            editor.AddDocumentAttachment(attachmentFile, "Sample attachment");
            editor.Save(outputPdf);
            editor.Close();

            // Assert: extract attachment names and verify the added file is present
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(outputPdf);
            extractor.ExtractAttachment();
            IList<string> names = extractor.GetAttachNames(); // Fixed CS0266

            bool attachmentFound = false;
            foreach (string name in names)
            {
                if (string.Equals(name, Path.GetFileName(attachmentFile), StringComparison.OrdinalIgnoreCase))
                {
                    attachmentFound = true;
                    break;
                }
            }

            NUnit.Framework.Assert.IsTrue(attachmentFound, "The attachment was not found in the saved PDF.");

            // Cleanup temporary files (optional)
            try { File.Delete(sourcePdf); } catch { }
            try { File.Delete(outputPdf); } catch { }
            try { File.Delete(attachmentFile); } catch { }
        }
    }
}

// Adding a dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main(string[] args)
    {
        // No operation – the test runner will discover and execute the tests.
    }
}