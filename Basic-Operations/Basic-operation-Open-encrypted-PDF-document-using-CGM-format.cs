using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file and its password
        const string encryptedPdfPath = "encrypted.pdf";
        const string password = "user123";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // Open the encrypted PDF using the password.
        // The Document(string, string) constructor handles encrypted PDFs.
        using (Document doc = new Document(encryptedPdfPath, password))
        {
            // Example operation: display page count
            Console.WriteLine($"Page count: {doc.Pages.Count}");

            // Decrypt the document (removes encryption) and save the result.
            doc.Decrypt();
            doc.Save("decrypted_output.pdf");
        }

        Console.WriteLine("Decrypted PDF saved as 'decrypted_output.pdf'.");
    }
}