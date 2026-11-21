using System;
using System.IO;
using Aspose.Pdf;

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
                // 30 percent opacity
                Opacity = 0.3,

                // Place at the bottom of the page
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment   = VerticalAlignment.Bottom,

                // Optional: adjust margins if needed
                BottomMargin = 10
            };

            // Apply the stamp to every page
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