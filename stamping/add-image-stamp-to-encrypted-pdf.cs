using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "stamped.pdf";
        const string password = "user123";
        const string stampImagePath = "stamp.png";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPath}");
            return;
        }

        if (!File.Exists(stampImagePath))
        {
            Console.Error.WriteLine($"Stamp image not found: {stampImagePath}");
            return;
        }

        // Open the encrypted PDF using the user password
        using (Document doc = new Document(inputPath, password))
        {
            // Decrypt the document to allow modifications
            doc.Decrypt();

            // Create an image stamp
            ImageStamp imgStamp = new ImageStamp(stampImagePath)
            {
                Background = false,
                Opacity = 0.5,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Apply the stamp to every page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(imgStamp);
            }

            // Re‑encrypt the document (optional, using same password)
            Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
            doc.Encrypt(password, password, perms, CryptoAlgorithm.AESx256);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}