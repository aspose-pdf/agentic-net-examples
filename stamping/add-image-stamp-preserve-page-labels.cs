using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string imagePath  = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Add the same image stamp to every page
            foreach (Page page in doc.Pages)
            {
                ImageStamp imgStamp = new ImageStamp(imagePath);
                // Position the stamp (example: top‑right corner)
                imgStamp.HorizontalAlignment = HorizontalAlignment.Right;
                imgStamp.VerticalAlignment   = VerticalAlignment.Top;
                imgStamp.Opacity = 0.5f; // semi‑transparent

                // Apply the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the PDF; page labels are preserved automatically
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPath}'.");
    }
}