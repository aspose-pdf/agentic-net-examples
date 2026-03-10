using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Annotations;   // For possible annotations (optional)

class Program
{
    static void Main()
    {
        // Input and output file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "updated_output.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example modification: add a blank page at the end of the document
            // Aspose.Pdf uses 1‑based indexing; Add() without parameters appends a new page
            pdfDoc.Pages.Add();

            // Additional modifications can be performed here (e.g., annotations, text, etc.)
            // Example: add a simple text annotation on the newly added page
            Aspose.Pdf.Page newPage = pdfDoc.Pages[pdfDoc.Pages.Count];
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
            TextAnnotation txtAnn = new TextAnnotation(newPage, rect)
            {
                Title    = "Note",
                Contents = "This is a newly added page.",
                Color    = Aspose.Pdf.Color.Yellow,
                Open     = true,
                Icon     = TextIcon.Note
            };
            newPage.Annotations.Add(txtAnn);

            // Save the modified document back to disk as a PDF.
            // Using the overload Save(string) ensures the output format remains PDF.
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Document saved successfully to '{outputPath}'.");
    }
}