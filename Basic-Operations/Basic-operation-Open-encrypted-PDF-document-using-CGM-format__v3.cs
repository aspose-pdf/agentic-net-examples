using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";
        const string outputPath = "decrypted_copy.pdf";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Open the encrypted PDF using ComHelper
        ComHelper helper = new ComHelper();
        using (Document doc = helper.OpenFile(encryptedPdfPath, password))
        {
            // Decrypt the document (Decrypt takes no parameters)
            doc.Decrypt();

            // Save the decrypted copy
            doc.Save(outputPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
    }
}