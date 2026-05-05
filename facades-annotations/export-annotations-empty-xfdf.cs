using System;
using System.IO;
using System.Xml.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using NUnit.Framework; // Added to bring stub attributes into scope

// Minimal NUnit stubs – used when the real NUnit package is not referenced.
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
    }
}

namespace AsposePdfTests
{
    [TestFixture]
    public class PdfAnnotationEditorTests
    {
        [Test]
        public void ExportAnnotations_NoAnnotations_ProducesValidEmptyXfdf()
        {
            // Create a minimal PDF document with a single blank page.
            using (Document doc = new Document())
            {
                // Add an empty page – the document now has no annotations.
                doc.Pages.Add();

                // Initialize the annotation editor and bind it to the document.
                using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
                {
                    editor.BindPdf(doc);

                    // Export annotations to an in‑memory stream.
                    using (MemoryStream xfdfStream = new MemoryStream())
                    {
                        editor.ExportAnnotationsToXfdf(xfdfStream);
                        xfdfStream.Position = 0; // Reset for reading.

                        // Load the XFDF XML and verify its structure.
                        XDocument xfdfXml = XDocument.Load(xfdfStream);
                        Assert.AreEqual("xfdf", xfdfXml.Root!.Name.LocalName,
                            "Root element should be <xfdf>.");

                        // When there are no annotations the <annots> element should be absent
                        // or empty. Both conditions are acceptable.
                        XElement? annotsElement = xfdfXml.Root.Element("annots");
                        bool hasNoAnnotations = annotsElement == null || !annotsElement.HasElements;
                        Assert.IsTrue(hasNoAnnotations,
                            "XFDF should contain no annotation entries when the source PDF has none.");
                    }
                }
            }
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public class Program
{
    public static void Main()
    {
        // No runtime logic required – tests are executed by the test runner.
    }
}
