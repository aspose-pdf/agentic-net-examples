using System;
using System.IO;
using Aspose.Pdf; // Core API (Document, Page, ImageStamp, PdfFormat, ConvertErrorAction)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_pdfa.pdf";    // PDF/A‑1b result
        const string imagePath  = "stamp.png";          // image to use as stamp
        const string altText    = "Company logo";       // alternative text for accessibility
        const string logPath    = "conversion_log.xml"; // conversion log (optional)

        // Verify required files exist
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {imagePath}");
            return;
        }

        // Load the source PDF (document disposal handled by using)
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp and set its alternative text
            ImageStamp imgStamp = new ImageStamp(imagePath);
            imgStamp.AlternativeText = altText; // <-- accessibility text for the stamp

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp); // <-- Page.AddStamp adds the stamp to the page
            }

            // Convert the document to PDF/A‑1b (PDF/A‑1b is PdfFormat.PDF_A_1B)
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b output
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file with image stamp saved to '{outputPath}'.");
    }
}