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
            // Pages are 1‑based in Aspose.Pdf
            Page page = doc.Pages[1];

            // Define the rectangle area to be redacted (llx, lly, urx, ury)
            var rect = new Rectangle(100, 500, 300, 550);

            // Create the redaction annotation and set its fill color
            var redaction = new RedactionAnnotation(page, rect)
            {
                FillColor = Color.LightBlue
            };

            // Add the annotation to the page
            page.Annotations.Add(redaction);

            // Apply the redaction for this annotation (Document.Redact() does not exist)
            redaction.Redact();

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPath}'.");
    }
}
