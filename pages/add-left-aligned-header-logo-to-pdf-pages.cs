using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for ImageStamp and alignment enums

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string logoPath   = "company_logo.png";
        const string outputPath = "output_with_header.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(logoPath))
        {
            Console.Error.WriteLine($"Logo image not found: {logoPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add the logo as a left‑aligned header on every page
            foreach (Page page in doc.Pages)
            {
                // Create a stamp that contains the image
                ImageStamp logoStamp = new ImageStamp(logoPath)
                {
                    // Place the stamp in the header area (top‑left)
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Optional margins to fine‑tune positioning
                    LeftMargin   = 10,   // distance from the left edge
                    TopMargin    = 10,   // distance from the top edge
                    // Ensure the stamp is drawn over the page content
                    Background = false
                };

                // Apply the stamp to the current page
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header logo to '{outputPath}'.");
    }
}