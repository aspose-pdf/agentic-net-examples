using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string stampPath  = "stamp.png";
        const string outputPath = "stamped_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }
        if (!File.Exists(stampPath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal.
            using (Document pdfDoc = new Document(inputPath))
            {
                // Configure the image stamp (position, opacity, background).
                ImageStamp imgStamp = new ImageStamp(stampPath)
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    Opacity = 0.5,
                    Background = false
                };

                // Apply the stamp to every existing page.
                foreach (Page page in pdfDoc.Pages)
                {
                    page.AddStamp(imgStamp);
                }

                // Stamping does not modify the /PageLabels dictionary,
                // so the original page labels remain unchanged automatically.
                pdfDoc.Save(outputPath);
            }

            Console.WriteLine($"Image stamp applied. Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}