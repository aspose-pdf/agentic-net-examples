using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the encrypted input PDF and the decrypted output PDF
        const string encryptedPdfPath = "encrypted.pdf";
        const string decryptedPdfPath = "decrypted.pdf";

        // The user password required to open the encrypted PDF
        const string userPassword = "user123";

        // Verify that the encrypted file exists before proceeding
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted document using the user password.
            // The constructor overload (string filename, string password) loads the PDF for decryption.
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document. After this call the PDF is no longer protected.
                doc.Decrypt();

                // Save the unprotected version to a new file.
                doc.Save(decryptedPdfPath);
            }

            Console.WriteLine($"Decryption successful. Unprotected PDF saved to '{decryptedPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect.
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Catch-all for any other errors (e.g., I/O issues).
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}