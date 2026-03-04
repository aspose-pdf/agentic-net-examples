using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Open the encrypted PDF using the constructor that accepts a password.
        using (Document doc = new Document(encryptedPdfPath, password))
        {
            // The document is now accessible; you can perform any PDF operations here.
            Console.WriteLine($"Number of pages: {doc.Pages.Count}");

            // Example: decrypt the document and save an unprotected copy.
            doc.Decrypt();                     // Decrypts the document (no parameters).
            doc.Save("decrypted.pdf");         // Saves as a regular PDF.
        }

        Console.WriteLine("Encrypted PDF opened successfully.");
    }
}