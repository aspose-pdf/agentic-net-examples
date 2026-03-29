using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

namespace AsposePdfTests
{
    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    // In a real test project the output type would be a library, but adding this method
    // removes the CS5001 error without affecting the unit‑test execution.
    public static class Program
    {
        public static void Main() { }
    }

    [TestFixture]
    public class PdfAttachmentTests
    {
        private const string OriginalPdf = "original.pdf";
        private const string AttachmentFile = "attachment.txt";
        private const string OutputPdf = "output.pdf";

        [SetUp]
        public void SetUp()
        {
            // Create a simple attachment file
            File.WriteAllText(AttachmentFile, "Sample attachment content");

            // Create a blank PDF document
            using (Document doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(OriginalPdf);
            }
        }

        [TearDown]
        public void TearDown()
        {
            // Clean up generated files
            if (File.Exists(OriginalPdf)) File.Delete(OriginalPdf);
            if (File.Exists(AttachmentFile)) File.Delete(AttachmentFile);
            if (File.Exists(OutputPdf)) File.Delete(OutputPdf);
        }

        [Test]
        public void AttachmentIsPresentAfterSave()
        {
            // Add the attachment to the PDF and save
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(OriginalPdf);
            editor.AddDocumentAttachment(AttachmentFile, "Sample attachment description");
            editor.Save(OutputPdf);

            // Extract attachment names from the saved PDF
            PdfExtractor extractor = new PdfExtractor();
            extractor.BindPdf(OutputPdf);
            extractor.ExtractAttachment();
            // GetAttachNames returns IList<string>; use the generic interface to avoid CS0266.
            IList<string> attachmentNames = extractor.GetAttachNames();

            // Verify that the expected attachment name is present
            bool found = false;
            foreach (string name in attachmentNames)
            {
                if (name != null && name.Equals(AttachmentFile, StringComparison.OrdinalIgnoreCase))
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found, "Attachment was not found in the saved PDF.");
        }
    }
}

// Minimal NUnit stubs to allow compilation when the real NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SetUpAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TearDownAttribute : Attribute { }

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