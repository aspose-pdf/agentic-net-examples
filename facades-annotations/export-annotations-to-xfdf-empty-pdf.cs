// Program.cs
using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs to allow compilation without the real NUnit package.
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

        public static void IsFalse(bool condition, string message = null)
        {
            if (condition)
                throw new Exception(message ?? "Assert.IsFalse failed.");
        }

        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfApi
{
    class Program
    {
        static void Main()
        {
            // Example: create an empty PDF and export annotations to XFDF (the PDF has no annotations).
            using (Document doc = new Document())
            {
                // Add a single blank page.
                doc.Pages.Add();

                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(doc);

                    // Export annotations to an in‑memory stream. The stream will contain a minimal XFDF document.
                    using (MemoryStream xfdfStream = new MemoryStream())
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                        // The stream is not used further in this demo; it is just to illustrate the API call.
                    }
                }
            }

            Console.WriteLine("Done.");
        }
    }
}

namespace AsposePdfTests
{
    using NUnit.Framework;

    [TestFixture]
    public class PdfAnnotationExportTests
    {
        // Helper method to create a minimal PDF with a single blank page.
        private Document CreateEmptyPdf()
        {
            // Document disposal is handled by the caller via a using block.
            Document doc = new Document();
            // Add one empty page (1‑based indexing).
            doc.Pages.Add();
            return doc;
        }

        [Test]
        public void ExportAnnotations_ToXfdf_WhenNoAnnotations_ShouldProduceValidEmptyXfdf()
        {
            // Arrange: create a PDF that contains no annotations.
            using (Document pdf = CreateEmptyPdf())
            {
                // Initialize the annotation editor and bind it to the PDF.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(pdf);

                    // Act: export annotations to an in‑memory stream.
                    using (MemoryStream xfdfStream = new MemoryStream())
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                        xfdfStream.Position = 0; // rewind for reading

                        // Read the XFDF content as a UTF‑8 string.
                        string xfdfContent = Encoding.UTF8.GetString(xfdfStream.ToArray());

                        // Assert: the XFDF is well‑formed and contains no annotation entries.
                        // A minimal XFDF file always starts with the <xfdf> root element.
                        Assert.IsTrue(xfdfContent.Contains("<xfdf"), "XFDF output should contain the root <xfdf> element.");

                        // When there are no annotations, there should be no <annotation> tags.
                        Assert.IsFalse(xfdfContent.Contains("<annotation"), "XFDF output should not contain any <annotation> elements for an empty PDF.");
                    }
                }
            }
        }
    }
}
