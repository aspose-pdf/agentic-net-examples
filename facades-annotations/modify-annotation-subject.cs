using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

// Minimal NUnit stubs for compilation
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

namespace PdfAnnotationTests
{
    [NUnit.Framework.TestFixture]
    public class ModifyAnnotationsTests
    {
        [NUnit.Framework.Test]
        public void ModifyAnnotationSubject_UpdatesSubjectProperty()
        {
            // Create a PDF with a single TextAnnotation having an initial subject
            using (var doc = new Document())
            {
                // Ensure at least one page exists
                doc.Pages.Add();

                // Define annotation rectangle
                var rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

                // Create and configure the original annotation
                var originalAnnot = new TextAnnotation(doc.Pages[1], rect)
                {
                    Subject = "Original Subject"
                };

                // Add annotation to the page
                doc.Pages[1].Annotations.Add(originalAnnot);

                // Save the document to a memory stream
                using (var originalStream = new MemoryStream())
                {
                    doc.Save(originalStream);
                    originalStream.Position = 0;

                    // Use PdfAnnotationEditor to modify the Subject property
                    using (var editor = new PdfAnnotationEditor())
                    {
                        editor.BindPdf(originalStream);

                        // Prepare an annotation instance with the new Subject value.
                        // The constructor must receive a Page and a Rectangle.
                        var updateAnnot = new TextAnnotation(doc.Pages[1], rect)
                        {
                            Subject = "Updated Subject"
                        };

                        // Apply modification to page 1 (start=1, end=1)
                        editor.ModifyAnnotations(1, 1, updateAnnot);

                        // Save the modified PDF to another memory stream
                        using (var modifiedStream = new MemoryStream())
                        {
                            editor.Save(modifiedStream);
                            modifiedStream.Position = 0;

                            // Load the modified document for verification
                            using (var modifiedDoc = new Document(modifiedStream))
                            {
                                // Retrieve the annotation after modification
                                var modifiedAnnot = modifiedDoc.Pages[1].Annotations[1] as TextAnnotation;

                                // Verify that the Subject property was updated
                                NUnit.Framework.Assert.AreEqual(
                                    "Updated Subject",
                                    modifiedAnnot?.Subject,
                                    "Subject was not updated correctly.");
                            }
                        }
                    }
                }
            }
        }
    }

    // Simple entry point to satisfy the compiler and allow manual execution.
    public static class Program
    {
        public static void Main()
        {
            // Run the test manually – in a real project a test runner would discover it.
            var test = new ModifyAnnotationsTests();
            test.ModifyAnnotationSubject_UpdatesSubjectProperty();
            Console.WriteLine("ModifyAnnotationsTests passed.");
        }
    }
}