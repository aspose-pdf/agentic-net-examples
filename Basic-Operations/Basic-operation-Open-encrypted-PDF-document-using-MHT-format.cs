using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file and its password
        const string encryptedPdfPath = "encrypted.pdf";
        const string userPassword = "user123";

        // Path where the decrypted PDF will be saved
        const string decryptedPdfPath = "decrypted.pdf";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF by providing the password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Remove encryption from the document
                doc.Decrypt();

                // Save the decrypted PDF
                doc.Save(decryptedPdfPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}