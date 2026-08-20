using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted.pdf";   // Encrypted source PDF
        const string password   = "user123";        // Password to open the PDF
        const string outputPath = "stamped.pdf";    // Resulting PDF with stamp
        const string stampImage = "logo.png";       // Image to use as stamp

        if (!File.Exists(inputPath) || !File.Exists(stampImage))
        {
            Console.Error.WriteLine("Input PDF or stamp image not found.");
            return;
        }

        // Open the encrypted PDF using the password
        using (Document doc = new Document(inputPath, password))
        {
            // Decrypt the document so it can be modified
            doc.Decrypt();

            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImage);
            imgStamp.HorizontalAlignment = HorizontalAlignment.Center;
            imgStamp.VerticalAlignment   = VerticalAlignment.Center;
            imgStamp.Opacity = 0.5f; // optional transparency

            // Add the stamp to the first page (pages are 1‑based)
            Page page = doc.Pages[1];
            page.AddStamp(imgStamp);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}