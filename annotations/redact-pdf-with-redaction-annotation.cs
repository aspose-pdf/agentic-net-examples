using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "redacted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Choose the page to annotate (first page in this example)
            Page page = doc.Pages[1];

            // Define the area to be redacted (coordinates in points)
            // Fully qualify Rectangle to avoid ambiguity with System.Drawing
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // Create the redaction annotation
            RedactionAnnotation redaction = new RedactionAnnotation(page, rect)
            {
                FillColor = Aspose.Pdf.Color.Red,
                OverlayText = "REDACTED"
            };

            // Add the annotation to the page
            page.Annotations.Add(redaction);

            // Apply the redaction for this annotation (Document.Redact() does not exist)
            redaction.Redact();

            // Save the redacted PDF (default Save writes PDF)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}
