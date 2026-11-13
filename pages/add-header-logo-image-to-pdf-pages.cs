using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string logoPath   = "logo.png";

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Prepare an ImageStamp for the logo
            ImageStamp logoStamp = new ImageStamp(logoPath)
            {
                // Align left and top to act as a header
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment   = VerticalAlignment.Top,
                // Use individual margin properties (ImageStamp has no Margin object)
                LeftMargin = 10,
                TopMargin  = 10,
                // Keep original size (no Scale property exists)
                Background = false
            };

            // Add the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(logoStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header with logo added to each page. Saved as '{outputPath}'.");
    }
}
