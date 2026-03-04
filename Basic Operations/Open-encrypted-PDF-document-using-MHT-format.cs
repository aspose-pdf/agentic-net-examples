using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword = "user123";
        const string outputPdfPath = "decrypted.pdf";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Open the encrypted PDF using the password constructor
        using (Document doc = new Document(encryptedPdfPath, userPassword))
        {
            // Decrypt the document (no parameters required)
            doc.Decrypt();

            // Save the decrypted PDF
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"Decrypted PDF saved to '{outputPdfPath}'.");
    }
}