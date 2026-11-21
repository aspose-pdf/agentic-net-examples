using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf   = "input.pdf";          // source PDF
        const string stampImage = "stamp.png";          // image to stamp
        const string outputPdf  = "output_pdfa1b.pdf";  // PDF/A‑1b result
        const string altText    = "Company logo stamp"; // alternative text for accessibility
        const string logFile    = "conversion_log.xml"; // conversion log (optional)

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }
        if (!File.Exists(stampImage))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImage}");
            return;
        }

        // Load the source PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPdf))
        {
            // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create an ImageStamp and configure its appearance
                ImageStamp imgStamp = new ImageStamp(stampImage)
                {
                    // Position the stamp at the bottom‑right corner (example)
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Do not place the stamp behind page content
                    Background = false,
                    // Optional opacity
                    Opacity = 0.8
                };

                // Add the stamp to the current page (add-stamp-to-all-pages-call-per-page-not-on-collection rule)
                page.AddStamp(imgStamp);
            }

            // After stamping, set alternative text on all image resources.
            // XImage.TrySetAlternativeText(string altText, Page page) is valid.
            foreach (Page page in doc.Pages)
            {
                foreach (XImage img in page.Resources.Images)
                {
                    // The method returns a bool indicating success; ignore it here.
                    img.TrySetAlternativeText(altText, page);
                }
            }

            // Convert the document to PDF/A‑1b (pdf-format-conversion-options-pua-strategy-enum rule)
            // Using the simple overload with log file, target format, and error action.
            doc.Convert(logFile, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the PDF/A‑1b output. No SaveOptions needed for PDF output.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF/A‑1b file with image stamp saved to '{outputPdf}'.");
    }
}