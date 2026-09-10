using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_header.pdf";
        const string logoPath = "company_logo.png";

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
            // Prepare an image stamp that will act as the header logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Place the stamp at the top‑left corner of each page
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                // Optional margins (distance from page edges)
                TopMargin = 10,
                LeftMargin = 10,
                // Draw over the page content (not as background)
                Background = false,
                Opacity = 1.0f
                // Width / Height can be set here if scaling is required
            };

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with header logo saved to '{outputPath}'.");
    }
}
