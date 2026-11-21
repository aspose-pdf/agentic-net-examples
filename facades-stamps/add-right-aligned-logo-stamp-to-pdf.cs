using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string logoPath = "logo.png";
        const string outputPath = "output.pdf";

        // Verify that the source PDF and logo image exist
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

        try
        {
            // Load the PDF document using the high‑level Document API
            Document doc = new Document(inputPath);

            // Create an image stamp for the logo and align it to the right margin
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                HorizontalAlignment = HorizontalAlignment.Right, // align to right margin
                Background = false,                               // place above page content
                Opacity = 1.0,                                    // fully opaque
                VerticalAlignment = VerticalAlignment.Top        // optional vertical position
            };

            // Add the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
            Console.WriteLine($"Logo stamp added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
