using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string firstInputPath  = "first.pdf";          // PDF to annotate
        const string secondInputPath = "second.pdf";         // PDF to merge
        const string tempAnnotated   = "first_annotated.pdf";// Temp file after annotation
        const string outputPath      = "merged_output.pdf"; // Final merged PDF

        // Verify input files exist
        if (!File.Exists(firstInputPath) || !File.Exists(secondInputPath))
        {
            Console.Error.WriteLine("One or more input files are missing.");
            return;
        }

        // ------------------------------------------------------------
        // Step 1: Open the first PDF, add a text annotation, save it.
        // ------------------------------------------------------------
        using (Document doc = new Document(firstInputPath))
        {
            // Define annotation rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create a TextAnnotation on the first page
            TextAnnotation annotation = new TextAnnotation(doc.Pages[1], rect)
            {
                Title    = "Note",
                Contents = "Added via Aspose.Pdf",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };

            // Add the annotation to the page
            doc.Pages[1].Annotations.Add(annotation);

            // Save the annotated document to a temporary file
            doc.Save(tempAnnotated);
        }

        // ------------------------------------------------------------
        // Step 2: Merge the annotated first PDF with the second PDF.
        // ------------------------------------------------------------
        PdfFileEditor pdfEditor = new PdfFileEditor();

        // Concatenate the two PDFs: first (annotated) + second => outputPath
        // The method returns a bool indicating success; we ignore it here.
        pdfEditor.Concatenate(tempAnnotated, secondInputPath, outputPath);

        // No explicit Close needed; PdfFileEditor does not implement IDisposable.

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}