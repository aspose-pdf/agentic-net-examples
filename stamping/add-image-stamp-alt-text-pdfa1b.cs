using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ConvertErrorAction enum (also in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string stampImage = "stamp.png";          // image to use as stamp
        const string outputPdf  = "output_pdfa1b.pdf";  // PDF/A‑1b result
        const string logFile    = "conversion_log.xml"; // conversion log (optional)

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

        // Load the source PDF (using statement ensures proper disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImage)
            {
                // Set alternative text for accessibility
                AlternativeText = "Company logo – accessible description",
                // Optional visual settings
                Background = false,
                Opacity = 0.8f,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Center
            };

            // Apply the stamp to every page (or choose specific pages as needed)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Convert the document to PDF/A‑1b (PDF/A-1b) format
            // The Convert method changes the document in‑place
            doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b compliant document
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b file with image stamp saved to '{outputPdf}'.");
    }
}