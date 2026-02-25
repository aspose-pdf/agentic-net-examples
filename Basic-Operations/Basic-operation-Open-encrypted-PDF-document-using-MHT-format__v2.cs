using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Path to the encrypted PDF file.
        const string encryptedPdfPath = "encrypted_input.pdf";

        // Password required to open the PDF (user password).
        const string userPassword = "user123";

        // Path where the decrypted copy will be saved.
        const string decryptedPdfPath = "decrypted_output.pdf";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF by providing the password.
            // Document(string, string) constructor handles password‑protected PDFs.
            using (Document pdfDoc = new Document(encryptedPdfPath, userPassword))
            {
                // The document is now opened and can be used normally.
                Console.WriteLine($"Pages: {pdfDoc.Pages.Count}");
                Console.WriteLine($"Title: {pdfDoc.Info.Title}");
                Console.WriteLine($"Author: {pdfDoc.Info.Author}");

                // Save a decrypted copy (no password will be required to open this file).
                pdfDoc.Save(decryptedPdfPath);
                Console.WriteLine($"Decrypted PDF saved to '{decryptedPdfPath}'.");
            }
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect.
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}