using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Required for ConvertErrorAction enum

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPdfAPath = "output_pdfa1b.pdf";
        const string conversionLog   = "conversion_log.xml";
        const string altText        = "Company logo";

        // Validate input files
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPdfPath))
            {
                // Prepare the image stamp
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    // Position the stamp at the bottom‑right corner of each page
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Optional visual settings
                    Opacity = 0.5,
                    Background = false
                };

                // Apply the stamp to every page (per‑page call, not collection)
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Set alternative text for all images on each page (including the stamp)
                foreach (Page page in doc.Pages)
                {
                    foreach (XImage img in page.Resources.Images)
                    {
                        img.TrySetAlternativeText(altText, page);
                    }
                }

                // Convert the document to PDF/A‑1b
                doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

                // Save the PDF/A‑1b output
                doc.Save(outputPdfAPath);
            }

            Console.WriteLine($"PDF/A‑1b file created: {outputPdfAPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}