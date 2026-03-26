using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using NUnit.Framework; // <-- added using directive for the stub namespace

// Minimal NUnit stub to allow compilation without the real NUnit package
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

        public static void IsInstanceOf<T>(object obj, string message = null)
        {
            if (!(obj is T))
                throw new Exception(message ?? $"Assert.IsInstanceOf failed. Object is not of type {typeof(T).FullName}.");
        }
    }
}

[TestFixture]
public class PdfAnnotationEditorTests
{
    private const string OriginalFile = "original.pdf";
    private const string ModifiedFile = "modified.pdf";

    [Test]
    public void ModifyAnnotations_UpdatesSubjectProperty()
    {
        // Clean up any previous files
        if (File.Exists(OriginalFile))
            File.Delete(OriginalFile);
        if (File.Exists(ModifiedFile))
            File.Delete(ModifiedFile);

        // Create a PDF with a single text annotation whose Subject is "Old Subject"
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect);
            annotation.Subject = "Old Subject";
            annotation.Contents = "Sample annotation";
            page.Annotations.Add(annotation);
            doc.Save(OriginalFile);
        }

        // Use PdfAnnotationEditor to modify the Subject property
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(OriginalFile);

        // Create a temporary TextAnnotation instance (required constructor) to hold new values
        Document tempDoc = new Document();
        Page tempPage = tempDoc.Pages.Add();
        Aspose.Pdf.Rectangle tempRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
        TextAnnotation newValues = new TextAnnotation(tempPage, tempRect)
        {
            Subject = "New Subject"
        };

        // Modify annotations on page 1 (start = 1, end = 1)
        editor.ModifyAnnotations(1, 1, newValues);
        editor.Save(ModifiedFile);
        editor.Close();

        // Verify that the Subject was updated to "New Subject"
        using (Document modifiedDoc = new Document(ModifiedFile))
        {
            Page modifiedPage = modifiedDoc.Pages[1];
            Annotation retrieved = modifiedPage.Annotations[1];
            Assert.IsInstanceOf<TextAnnotation>(retrieved);
            TextAnnotation retrievedText = (TextAnnotation)retrieved;
            Assert.AreEqual("New Subject", retrievedText.Subject);
        }
    }
}

// Dummy entry point to satisfy the compiler for a console‑type project
public static class Program
{
    public static void Main()
    {
        // No runtime logic required – tests are executed by the test runner.
    }
}