using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string stampImage = "stamp.png";          // image to use as stamp
        const string outputPdf  = "output_pdfa1b.pdf";  // PDF/A‑1b result
        const string altText    = "Company logo – accessible description";

        // Verify required files exist
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the source document
        using (Document doc = new Document(inputPdf))
        {
            // Convert to PDF/A‑1b (PDF/A‑1b is PDF_A_1B)
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Create an image stamp and set its alternative text
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                AlternativeText = altText,
                Background      = false,                     // stamp on top of content
                Opacity         = 0.5f,                      // semi‑transparent
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the PDF/A‑1b document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b file with image stamp saved to '{outputPdf}'.");
    }
}