using System;
using System.IO;
using System.Text;
using System.Xml;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework;

namespace AsposePdfTests
{
    // Dummy entry point to satisfy the compiler when the project is built as an executable.
    public static class Program
    {
        public static void Main() { /* No‑op */ }
    }

    [TestFixture]
    public class ExportAnnotationsTests
    {
        [Test]
        public void ExportAnnotations_NoAnnotations_ReturnsValidXfdf()
        {
            // Create a simple PDF document with a single blank page.
            using (Document pdfDoc = new Document())
            {
                // Add an empty page (1‑based indexing).
                pdfDoc.Pages.Add();

                // Initialize the annotation editor with the in‑memory document.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor(pdfDoc))
                {
                    // Export annotations to a memory stream.
                    using (MemoryStream xfdfStream = new MemoryStream())
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);

                        // Ensure the stream contains data.
                        Assert.IsTrue(xfdfStream.Length > 0, "XFDF stream should not be empty.");

                        // Reset position for reading.
                        xfdfStream.Position = 0;

                        // Read the XFDF content as a string.
                        string xfdfContent;
                        using (StreamReader reader = new StreamReader(xfdfStream, Encoding.UTF8))
                        {
                            xfdfContent = reader.ReadToEnd();
                        }

                        // Load the content into an XmlDocument to verify well‑formed XML.
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(xfdfContent);

                        // The root element of a valid XFDF file must be <xfdf>.
                        Assert.AreEqual("xfdf", xmlDoc.DocumentElement!.Name, "Root element should be 'xfdf'.");

                        // When there are no annotations, the <annots> element should be absent or empty.
                        XmlNode annotsNode = xmlDoc.DocumentElement.SelectSingleNode("annots");
                        if (annotsNode != null)
                        {
                            // Ensure there are no child annotation nodes.
                            Assert.IsFalse(annotsNode.HasChildNodes, "XFDF should not contain any annotation entries.");
                        }
                    }
                }
            }
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

    public static class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }

        public static void IsTrue(bool condition, string? message = null)
        {
            if (!condition)
                throw new Exception(message ?? "Assert.IsTrue failed.");
        }

        public static void IsFalse(bool condition, string? message = null)
        {
            if (condition)
                throw new Exception(message ?? "Assert.IsFalse failed.");
        }
    }
}