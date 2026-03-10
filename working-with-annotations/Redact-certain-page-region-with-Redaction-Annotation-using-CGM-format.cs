using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputCgmPath  = "input.cgm";
        const string outputPdfPath = "redacted_output.pdf";

        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        // Load the CGM file (CGM is input‑only, so we convert it to a PDF document)
        using (Document doc = new Document(inputCgmPath, new CgmLoadOptions()))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The loaded document contains no pages.");
                return;
            }

            // Choose the page to redact (e.g., first page)
            Page page = doc.Pages[1];

            // Define the region to be redacted (coordinates in points)
            // Rectangle(left, bottom, width, height)
            Aspose.Pdf.Rectangle redactionRect = new Aspose.Pdf.Rectangle(100, 500, 200, 100);

            // Create the redaction annotation on the selected page
            RedactionAnnotation redaction = new RedactionAnnotation(page, redactionRect)
            {
                // Optional visual settings
                FillColor = Aspose.Pdf.Color.Black,   // black fill to hide content
                BorderColor = Aspose.Pdf.Color.Black,
                Opacity = 1.0
            };

            // Add the annotation to the page
            page.Annotations.Add(redaction);

            // Apply the redaction (removes underlying content)
            redaction.Redact();

            // Save the resulting PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Redacted PDF saved to '{outputPdfPath}'.");
    }
}