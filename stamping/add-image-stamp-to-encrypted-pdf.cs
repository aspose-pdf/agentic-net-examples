using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string password = "user123";
        const string outputPath = "stamped.pdf";
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

        // Open the encrypted PDF with the correct password
        using (Document doc = new Document(inputPath, password))
        {
            // Decrypt the document to allow modifications
            doc.Decrypt();

            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                XIndent = 50,          // Horizontal position
                YIndent = 700,         // Vertical position
                Width = 100,           // Desired width
                Height = 50,           // Desired height
                Opacity = 0.5f,        // Semi‑transparent
                Background = false     // Stamp on top of content
            };

            // Apply the stamp to each page (or modify as needed)
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Save the stamped PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}