using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "redacted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure we work with the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the area to be redacted (coordinates in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the redaction annotation
            RedactionAnnotation redaction = new RedactionAnnotation(page, rect)
            {
                // Text that will appear over the redacted area
                OverlayText = "Confidential",
                // Repeat the overlay text to fill the entire redaction rectangle
                Repeat = true
            };

            // Add the annotation to the page
            page.Annotations.Add(redaction);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}