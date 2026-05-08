using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF, output PDF and logo image paths
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string logoPath = "logo.png";

        // Validate input files
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Iterate over each page and apply the logo stamp aligned to the right margin
        foreach (Page page in pdfDocument.Pages)
        {
            // Create an ImageStamp from the image file
            ImageStamp stamp = new ImageStamp(logoPath)
            {
                // Align the stamp to the right edge of the page and center it vertically
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment   = VerticalAlignment.Center,
                // Optional: make the stamp appear in front of page content (false = foreground)
                Background = false,
                // Optional: set opacity (0‑1 range)
                Opacity = 1.0f
            };

            // Apply the stamp to the current page
            page.AddStamp(stamp);
        }

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Logo stamp added and saved to '{outputPdf}'.");
    }
}
