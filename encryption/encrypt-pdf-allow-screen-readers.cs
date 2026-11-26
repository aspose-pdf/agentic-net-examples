using System;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string outputPath = "privScreenReaders.pdf";

        // Create a new (empty) PDF document
        using (Document doc = new Document())
        {
            // Add a blank page – a PDF must contain at least one page
            doc.Pages.Add();

            // Define the permissions you want to allow.
            // Permissions.ExtractContentWithDisabilities enables screen‑reader access.
            // Here we also allow printing as an example.
            Permissions permissions = Permissions.PrintDocument | Permissions.ExtractContentWithDisabilities;

            // Encrypt the document with user password "u1", owner password "o1" using AES‑128.
            doc.Encrypt("u1", "o1", permissions, CryptoAlgorithm.AESx128);

            // Save the encrypted PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved as '{outputPath}'.");
    }
}
