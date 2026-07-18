using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string imagePath = "stamp.png";

        // Percentage offsets relative to page dimensions (e.g., 10% from left, 20% from bottom)
        const double percentX = 0.10; // 10%
        const double percentY = 0.20; // 20%

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
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
            // Apply the image stamp to each page (or modify as needed)
            foreach (Page page in doc.Pages)
            {
                // Create an ImageStamp from the image file
                ImageStamp imgStamp = new ImageStamp(imagePath);

                // Compute absolute coordinates based on page size
                double x = page.PageInfo.Width * percentX;
                double y = page.PageInfo.Height * percentY;

                // Set the stamp position using percentage‑based offsets
                imgStamp.XIndent = x; // horizontal coordinate from left
                imgStamp.YIndent = y; // vertical coordinate from bottom

                // Add the stamp to the current page
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPath}'.");
    }
}