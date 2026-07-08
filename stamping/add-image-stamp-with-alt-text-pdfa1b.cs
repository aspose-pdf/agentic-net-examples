using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string stampImage = "logo.png";           // image to use as stamp
        const string outputPdf  = "output_pdfa1b.pdf";  // PDF/A‑1b result
        const string logFile    = "conversion_log.xml"; // conversion log

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the source document and add the image stamp with alternative text
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Set the alternative text for accessibility
                AlternativeText = "Company logo",
                // Position the stamp (example: bottom‑right corner)
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Optional visual settings
                Opacity = 0.8f,
                Background = false
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Convert the document to PDF/A‑1b
            doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b output
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPdf}'.");
    }
}