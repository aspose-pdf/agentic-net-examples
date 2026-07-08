using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath      = "input.pdf";   // source PDF
        const string imagePath    = "logo.png";    // image to stamp
        const string outputPath   = "output.pdf";  // stamped PDF

        // Ensure source files exist
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image not found: {imagePath}");
            return;
        }

        // Load the PDF document (creation & loading rule)
        using (Document doc = new Document(pdfPath))
        {
            // Load image into a memory stream
            using (FileStream fileStream = File.OpenRead(imagePath))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                memoryStream.Position = 0; // reset for reading

                // Create an ImageStamp from the memory stream (constructor rule)
                ImageStamp imgStamp = new ImageStamp(memoryStream)
                {
                    // Position the stamp at the center of each page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Center,
                    // Optional visual settings
                    Opacity = 0.6,
                    Background = false
                };

                // Apply the stamp to every page in the document
                foreach (Page page in doc.Pages)
                {
                    page.AddStamp(imgStamp);
                }
            }

            // Save the modified PDF (saving rule)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Image stamp applied and saved to '{outputPath}'.");
    }
}