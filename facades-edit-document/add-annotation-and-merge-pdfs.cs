using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;      // Annotation classes
using Aspose.Pdf.Facades;          // Facade classes for merging

class Program
{
    static void Main()
    {
        const string firstPdfPath  = "first.pdf";          // Original PDF to annotate
        const string secondPdfPath = "second.pdf";         // PDF to merge after annotation
        const string tempAnnotated = "first_annotated.pdf";// Temporary file for annotated PDF
        const string outputPath    = "merged_output.pdf"; // Final merged result

        // Verify input files exist
        if (!File.Exists(firstPdfPath) || !File.Exists(secondPdfPath))
        {
            Console.Error.WriteLine("One or more input PDF files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Load the first PDF, add a text annotation, and save it
        // ------------------------------------------------------------
        using (Document doc = new Document(firstPdfPath))
        {
            // Add a simple text annotation on the first page
            Page page = doc.Pages[1]; // Pages are 1‑based
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation annotation = new TextAnnotation(page, rect)
            {
                Title    = "Note",
                Contents = "Added via Aspose.Pdf.Facades",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };
            page.Annotations.Add(annotation);

            // Save the annotated document to a temporary file
            doc.Save(tempAnnotated);
        }

        // ------------------------------------------------------------
        // Step 2: Merge the temporary annotated PDF with the second PDF
        // ------------------------------------------------------------
        PdfFileEditor fileEditor = new PdfFileEditor();

        // Concatenate returns true on success
        bool merged = fileEditor.Concatenate(tempAnnotated, secondPdfPath, outputPath);
        if (!merged)
        {
            Console.Error.WriteLine("Failed to merge PDFs.");
            return;
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");

        // Clean up the temporary file
        try { File.Delete(tempAnnotated); } catch { /* ignore cleanup errors */ }
    }
}