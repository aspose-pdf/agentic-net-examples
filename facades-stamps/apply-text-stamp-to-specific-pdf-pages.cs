using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampImagePath = "stamp.png";

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
            // Load the PDF document
            using (Document pdf = new Document(inputPath))
            {
                // Create and configure the image stamp (Aspose.Pdf.Drawing.ImageStamp)
                ImageStamp imgStamp = new ImageStamp(stampImagePath)
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Place stamp above page content (false = foreground)
                    Background = false
                };

                // Pages we want to stamp – 1‑based indices
                int[] pagesToStamp = { 1, 5, 10 };

                foreach (int pageNumber in pagesToStamp)
                {
                    // Guard against out‑of‑range page numbers
                    if (pageNumber > 0 && pageNumber <= pdf.Pages.Count)
                    {
                        pdf.Pages[pageNumber].AddStamp(imgStamp);
                    }
                    else
                    {
                        Console.Error.WriteLine($"Page {pageNumber} does not exist in the document.");
                    }
                }

                // Save the stamped PDF
                pdf.Save(outputPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
