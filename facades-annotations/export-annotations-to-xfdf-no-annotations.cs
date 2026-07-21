using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs – add only when the real NUnit package is not referenced.
namespace NUnit.Framework
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TestFixtureAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TestAttribute : Attribute { }

    public static class Assert
    {
        public static void IsNotEmpty(string value, string? message = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception(message ?? "Assert.IsNotEmpty failed – string is null or empty.");
        }

        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed – condition is false.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class AnnotationExportTests
    {
        // Helper method to create a simple PDF with a single blank page.
        private static Document CreateBlankPdf()
        {
            // Document implements IDisposable; use a using block when consuming.
            Document doc = new Document();
            // Add an empty page (1‑based indexing).
            doc.Pages.Add();
            return doc;
        }

        [NUnit.Framework.Test]
        public void ExportAnnotations_ToXfdf_WhenNoAnnotations_ShouldProduceValidXfdf()
        {
            // Arrange: create a PDF that contains no annotations.
            using (Document pdf = CreateBlankPdf())
            {
                // Initialize the annotation editor and bind it to the PDF document.
                PdfAnnotationEditor editor = new PdfAnnotationEditor();
                editor.BindPdf(pdf);

                // Act: export annotations to an in‑memory stream.
                using (MemoryStream xfdfStream = new MemoryStream())
                {
                    editor.ExportAnnotationsToXfdf(xfdfStream);
                    // Ensure the editor releases resources.
                    editor.Close();

                    // Reset stream position for reading.
                    xfdfStream.Position = 0;
                    string xfdfContent = new StreamReader(xfdfStream, Encoding.UTF8).ReadToEnd();

                    // Assert: the XFDF output is not empty and contains the root <xfdf> element.
                    NUnit.Framework.Assert.IsNotEmpty(xfdfContent, "XFDF content should not be empty.");
                    NUnit.Framework.Assert.IsTrue(xfdfContent.Contains("<xfdf"), "XFDF should contain the root <xfdf> element.");
                    // When there are no annotations, the <annots> section should be empty or absent.
                    // The presence of the tag is sufficient for this test.
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main(string[] args)
    {
        // No operation – the project is intended for unit testing.
    }
}