using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string encryptedPdfPath = "encrypted.pdf";   // input encrypted PDF
        const string userPassword     = "user123";        // password to open the PDF
        const string outputPdfPath    = "decrypted.pdf";  // path for the unprotected PDF

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted document using the user password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document (removes encryption)
                doc.Decrypt();

                // Save the unprotected version
                doc.Save(outputPdfPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPdfPath}'.");
        }
        catch (InvalidPasswordException)
        {
            Console.Error.WriteLine("Invalid password provided.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}