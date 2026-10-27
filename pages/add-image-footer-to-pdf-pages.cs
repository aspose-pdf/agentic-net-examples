using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for ImageStamp if needed (ImageStamp is in Aspose.Pdf)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string footerPath = "footer.png";   // image to use as footer
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(footerPath))
        {
            Console.Error.WriteLine($"Footer image not found: {footerPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create an ImageStamp for the footer image
            ImageStamp footerStamp = new ImageStamp(footerPath)
            {
                // 30% opacity (0.0 = fully transparent, 1.0 = fully opaque)
                Opacity = 0.3,
                // Position the stamp at the bottom center of each page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,
                // Ensure the stamp is placed on top of page content
                Background = false
            };

            // Apply the stamp to every page individually
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(footerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer added and saved to '{outputPath}'.");
    }
}