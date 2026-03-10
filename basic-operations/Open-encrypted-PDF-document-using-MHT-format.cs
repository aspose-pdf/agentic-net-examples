using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths to the encrypted PDF and the output (decrypted) PDF.
        const string encryptedPdfPath = "encrypted.pdf";
        const string decryptedPdfPath = "decrypted.pdf";

        // Password for the encrypted PDF (user or owner password).
        const string password = "myPassword";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password constructor.
            using (Document doc = new Document(encryptedPdfPath, password))
            {
                // Decrypt the document (Decrypt takes no parameters).
                doc.Decrypt();

                // Save the decrypted PDF.
                doc.Save(decryptedPdfPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}