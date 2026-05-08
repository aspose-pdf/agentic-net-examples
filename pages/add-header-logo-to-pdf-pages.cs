using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_header.pdf";
        const string logoPath   = "company_logo.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
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
            // Add the logo stamp to every page
            foreach (Page page in doc.Pages)
            {
                ImageStamp logoStamp = new ImageStamp(logoPath)
                {
                    // Align the stamp to the left side of the page header area
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment   = VerticalAlignment.Top,
                    // Optional: set margins if needed
                    LeftMargin = 10,
                    TopMargin  = 10,
                    // Ensure the stamp is drawn over the page content (not as background)
                    Background = false,
                    Opacity    = 1.0f
                };

                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with header logo: {outputPath}");
    }
}
