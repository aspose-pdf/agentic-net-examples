using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

// Minimal NUnit stubs to allow compilation without the NUnit package
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
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

namespace AsposePdfTests
{
    [NUnit.Framework.TestFixture]
    public class PdfAnnotationTests
    {
        [NUnit.Framework.Test]
        public void ModifyAnnotations_UpdatesSubject()
        {
            // Prepare temporary file paths
            string sourcePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
            string modifiedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

            try
            {
                // -------------------------------------------------
                // 1. Create a simple PDF with a TextAnnotation
                // -------------------------------------------------
                using (Document doc = new Document())
                {
                    // Add a blank page
                    Page page = doc.Pages.Add();

                    // Create a TextAnnotation with an initial Subject and a Name (used as identifier)
                    TextAnnotation annotation = new TextAnnotation(page, new Rectangle(100, 500, 300, 550))
                    {
                        Title = "Note",
                        Subject = "OldSubject",
                        Contents = "Original annotation",
                        Name = "MyAnnotation" // identifier for later lookup
                    };

                    // Add the annotation to the page
                    page.Annotations.Add(annotation);

                    // Save the source PDF
                    doc.Save(sourcePath);
                }

                // -------------------------------------------------
                // 2. Retrieve the annotation Name (used as identifier)
                // -------------------------------------------------
                string annotationName;
                using (Document doc = new Document(sourcePath))
                {
                    // Assuming only one annotation exists
                    Annotation firstAnnotation = doc.Pages[1].Annotations[1];
                    annotationName = firstAnnotation.Name;
                }

                // -------------------------------------------------
                // 3. Modify the Subject property (cast to MarkupAnnotation because Subject lives there)
                // -------------------------------------------------
                using (Document doc = new Document(sourcePath))
                {
                    // Find the annotation by its Name
                    foreach (Annotation ann in doc.Pages[1].Annotations)
                    {
                        if (ann.Name == annotationName && ann is MarkupAnnotation markupAnn)
                        {
                            // Subject is defined on MarkupAnnotation (base for TextAnnotation)
                            markupAnn.Subject = "NewSubject";
                            break;
                        }
                    }

                    // Save the modified PDF
                    doc.Save(modifiedPath);
                }

                // -------------------------------------------------
                // 4. Verify that the Subject has been updated
                // -------------------------------------------------
                using (Document modifiedDoc = new Document(modifiedPath))
                {
                    Annotation modifiedAnnotation = modifiedDoc.Pages[1].Annotations[1];
                    // Cast to MarkupAnnotation to read Subject
                    string subject = (modifiedAnnotation as MarkupAnnotation)?.Subject;
                    NUnit.Framework.Assert.AreEqual("NewSubject", subject, "The annotation Subject was not updated correctly.");
                }
            }
            finally
            {
                // Clean up temporary files
                if (File.Exists(sourcePath))
                    File.Delete(sourcePath);
                if (File.Exists(modifiedPath))
                    File.Delete(modifiedPath);
            }
        }
    }

    // Dummy entry point to satisfy the compiler when building an executable project.
    public class Program
    {
        public static void Main(string[] args)
        {
            // No runtime logic required – tests are executed by the test runner.
        }
    }
}
