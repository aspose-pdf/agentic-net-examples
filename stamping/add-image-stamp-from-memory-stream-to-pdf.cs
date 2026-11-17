using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "stamped_output.pdf";
        const string stampImagePath = "stamp.png";

        // Verify that the source PDF exists.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"PDF file not found: {Path.GetFullPath(inputPath)}");
            return;
        }

        // Verify that the stamp image exists before loading it into memory.
        if (!File.Exists(stampImagePath))
        {
            Console.WriteLine($"Stamp image not found: {Path.GetFullPath(stampImagePath)}");
            return;
        }

        // Load image data into a memory stream.
        // In a real scenario the byte[] could come from a database, network, etc.
        byte[] imageBytes = File.ReadAllBytes(stampImagePath);
        using (MemoryStream imgStream = new MemoryStream(imageBytes))
        // Open the PDF document inside a using block for deterministic disposal.
        using (Document pdfDocument = new Document(inputPath))
        {
            // Create an ImageStamp directly from the memory stream.
            ImageStamp imgStamp = new ImageStamp(imgStream)
            {
                Background = false,
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the stamp to each page individually.
            foreach (Page page in pdfDocument.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the modified PDF.
            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Image stamp added from memory stream and saved to '{outputPath}'.");
    }
}
