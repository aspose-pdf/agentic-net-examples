using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for PDF/A conversion (Convert method is on Document, not Facades, but keep for completeness)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath    = "conversion_log.xml";
        const string stampImagePath = "stamp.png";
        const string altText    = "Company logo – accessible description";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp from the image file
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                // Set the alternative text for accessibility
                AlternativeText = altText,

                // Example positioning – adjust as needed
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Bottom,
                Opacity             = 0.8f
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Convert the document to PDF/A‑1b
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b compliant file
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file with image stamp saved to '{outputPath}'.");
    }
}