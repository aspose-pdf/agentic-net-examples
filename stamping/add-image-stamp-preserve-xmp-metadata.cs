using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string stampImagePath = "stamp.png";
        const string outputPath = "output.pdf";

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

        try
        {
            // Load the source PDF
            using (Document doc = new Document(inputPath))
            {
                // Preserve custom XMP metadata
                // Example: add a custom property named "MyCustomProperty"
                doc.Metadata.Add("MyCustomProperty", "CustomValue");

                // Create an image stamp
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    Background = false,                     // place on top of page content
                    Opacity = 0.5,                          // semi‑transparent
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                // Apply the stamp to every page
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Save the stamped PDF; metadata is retained automatically
                doc.Save(outputPath);
            }

            Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}