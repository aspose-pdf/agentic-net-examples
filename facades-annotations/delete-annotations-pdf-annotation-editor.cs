using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework;

// Minimal NUnit stubs to allow compilation when the NUnit package is not referenced.
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
        public static void AreEqual<T>(T expected, T actual, string? message = null)
        {
            if (!object.Equals(expected, actual))
                throw new Exception(message ?? $"Assert.AreEqual failed. Expected:<{expected}>. Actual:<{actual}>.");
        }
    }
}

[TestFixture]
public class PdfAnnotationEditorTests
{
    private const string TempDir = "TestTemp";

    [SetUp]
    public void SetUp()
    {
        if (!Directory.Exists(TempDir))
            Directory.CreateDirectory(TempDir);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(TempDir))
            Directory.Delete(TempDir, true);
    }

    [Test]
    public void DeleteAnnotations_RemovesAllAnnotations()
    {
        // Arrange: create a PDF with two text annotations
        string sourcePath = Path.Combine(TempDir, "source.pdf");
        string resultPath = Path.Combine(TempDir, "result.pdf");

        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // First annotation
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            TextAnnotation ann1 = new TextAnnotation(page, rect1)
            {
                Title = "Note1",
                Contents = "First annotation",
                Color = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(ann1);

            // Second annotation
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(300, 500, 400, 550);
            TextAnnotation ann2 = new TextAnnotation(page, rect2)
            {
                Title = "Note2",
                Contents = "Second annotation",
                Color = Aspose.Pdf.Color.Green
            };
            page.Annotations.Add(ann2);

            // Verify that two annotations are present before deletion
            Assert.AreEqual(2, page.Annotations.Count, "Setup failed: expected 2 annotations.");

            // Save the PDF to disk
            doc.Save(sourcePath);
        }

        // Act: delete all annotations using PdfAnnotationEditor
        using (PdfAnnotationEditor editor = new PdfAnnotationEditor())
        {
            editor.BindPdf(sourcePath);
            editor.DeleteAnnotations(); // method under test
            editor.Save(resultPath);
        }

        // Assert: the resulting PDF contains zero annotations
        using (Document resultDoc = new Document(resultPath))
        {
            Page resultPage = resultDoc.Pages[1];
            Assert.AreEqual(0, resultPage.Annotations.Count, "DeleteAnnotations should remove all annotations.");
        }
    }
}

// Dummy entry point to satisfy the compiler when building as an executable.
public static class Program
{
    public static void Main() { /* No-op */ }
}
