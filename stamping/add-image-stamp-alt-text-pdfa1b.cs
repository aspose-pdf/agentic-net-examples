using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string outputPdf  = "output.pdf";         // final PDF/A‑1b file
        const string imagePath  = "stamp.png";          // image to use as stamp
        const string altText    = "Company logo";       // alternative text for the stamp
        const bool generatePdfA = true;                 // flag indicating PDF/A‑1b output

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the source document
        using (Document doc = new Document(inputPdf))
        {
            // Add the image stamp only when PDF/A‑1b output is required
            if (generatePdfA)
            {
                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(imagePath)
                {
                    // Set the alternative text for accessibility
                    AlternativeText = altText,

                    // Position the stamp (example: bottom‑right corner)
                    // Adjust these values as needed
                    XIndent = 400,
                    YIndent = 50,
                    // Optional visual settings
                    Opacity = 0.8f,
                    Background = false
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }
            }

            // Convert the document to PDF/A‑1b (PDF/A‑1b is PDF_FORMAT.PDF_A_1B)
            string conversionLog = "pdfa_conversion.log";
            doc.Convert(conversionLog, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the resulting PDF/A‑1b file
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b file saved to '{outputPdf}'.");
    }
}