using System;
using System.IO;
using Aspose.Pdf;   // All Aspose.Pdf types (Document, MhtLoadOptions, etc.) are in this namespace

class Program
{
    static void Main()
    {
        // Path to the encrypted (password‑protected) PDF file
        const string encryptedPdfPath = "encrypted.pdf";

        // The password required to open the PDF (user or owner password)
        const string password = "user123";

        // Path where the decrypted PDF will be saved
        const string decryptedPdfPath = "decrypted.pdf";

        // Verify that the source file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF by providing the password.
            // The Document constructor (string filename, string password) handles decryption internally.
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Decrypt the document explicitly (Decrypt() takes no parameters).
                doc.Decrypt();

                // Save the now‑decrypted PDF.
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