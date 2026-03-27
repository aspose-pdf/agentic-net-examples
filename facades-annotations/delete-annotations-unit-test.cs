using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;
using System.Diagnostics;

class DeleteAnnotationsUnitTest
{
    static void Main()
    {
        // Create a PDF with a single annotation
        string originalPath = "original.pdf";
        string cleanedPath = "cleaned.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();
            TextAnnotation annotation = new TextAnnotation(page, new Aspose.Pdf.Rectangle(100, 500, 200, 550))
            {
                Title = "Note",
                Contents = "Sample annotation",
                Open = true,
                Color = Aspose.Pdf.Color.Yellow
            };
            page.Annotations.Add(annotation);
            doc.Save(originalPath);
        }

        // Verify the annotation exists
        using (Document doc = new Document(originalPath))
        {
            Page page = doc.Pages[1];
            int initialCount = page.Annotations.Count;
            if (initialCount != 1)
            {
                throw new Exception($"Expected 1 annotation before deletion, found {initialCount}.");
            }
        }

        // Delete all annotations using PdfAnnotationEditor
        PdfAnnotationEditor editor = new PdfAnnotationEditor();
        editor.BindPdf(originalPath);
        editor.DeleteAnnotations();
        editor.Save(cleanedPath);
        editor.Close();

        // Verify that all annotations have been removed
        using (Document doc = new Document(cleanedPath))
        {
            Page page = doc.Pages[1];
            int finalCount = page.Annotations.Count;
            if (finalCount != 0)
            {
                throw new Exception($"Expected 0 annotations after deletion, found {finalCount}.");
            }
        }

        Console.WriteLine("DeleteAnnotations unit test passed.");
    }
}