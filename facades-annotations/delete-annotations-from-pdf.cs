using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class DeleteAnnotationsUnitTest
{
    // Entry point required for compilation
    static void Main(string[] args)
    {
        // Create a temporary PDF with known annotations
        string originalPdfPath = Path.Combine(Path.GetTempPath(), "original.pdf");
        string cleanedPdfPath   = Path.Combine(Path.GetTempPath(), "cleaned.pdf");

        // Ensure any previous files are removed
        if (File.Exists(originalPdfPath)) File.Delete(originalPdfPath);
        if (File.Exists(cleanedPdfPath))   File.Delete(cleanedPdfPath);

        // Build a PDF containing two text annotations on the first page
        using (Document doc = new Document())
        {
            // Add a blank page
            Page page = doc.Pages.Add();

            // First annotation
            Aspose.Pdf.Rectangle rect1 = new Aspose.Pdf.Rectangle(100, 700, 200, 750);
            TextAnnotation annot1 = new TextAnnotation(page, rect1);
            annot1.Title    = "Note 1";
            annot1.Contents = "First annotation";
            annot1.Open     = true;
            page.Annotations.Add(annot1);

            // Second annotation
            Aspose.Pdf.Rectangle rect2 = new Aspose.Pdf.Rectangle(100, 600, 200, 650);
            TextAnnotation annot2 = new TextAnnotation(page, rect2);
            annot2.Title    = "Note 2";
            annot2.Contents = "Second annotation";
            annot2.Open     = true;
            page.Annotations.Add(annot2);

            // Save the PDF to disk
            doc.Save(originalPdfPath);
        }

        // Verify that the original PDF contains exactly 2 annotations
        int originalCount;
        using (Document verifyDoc = new Document(originalPdfPath))
        {
            originalCount = verifyDoc.Pages[1].Annotations.Count;
        }

        if (originalCount != 2)
        {
            Console.Error.WriteLine($"Setup failure: expected 2 annotations, found {originalCount}.");
            Environment.Exit(1);
        }

        // Remove all annotations from page 1 using the Document API (PdfFileEditor has no DeleteAnnotations method)
        using (Document docToClean = new Document(originalPdfPath))
        {
            // Clear annotations on the first page (or on all pages if desired)
            docToClean.Pages[1].Annotations.Clear();
            docToClean.Save(cleanedPdfPath);
        }

        // Load the cleaned PDF and count remaining annotations
        int cleanedCount;
        using (Document cleanedDoc = new Document(cleanedPdfPath))
        {
            cleanedCount = cleanedDoc.Pages[1].Annotations.Count;
        }

        // Assert that all annotations were removed
        if (cleanedCount == 0)
        {
            Console.WriteLine("DeleteAnnotations unit test passed: all annotations were removed.");
            Environment.Exit(0);
        }
        else
        {
            Console.Error.WriteLine($"DeleteAnnotations unit test failed: expected 0 annotations, found {cleanedCount}.");
            Environment.Exit(1);
        }
    }
}