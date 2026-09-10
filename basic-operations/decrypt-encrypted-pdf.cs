using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the encrypted input PDF and the decrypted output PDF
        const string encryptedPath = "encrypted.pdf";
        const string decryptedPath = "decrypted.pdf";

        // The user password required to open the encrypted PDF
        const string userPassword = "user123";

        // Verify that the input file exists
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"Error: File not found – {encryptedPath}");
            return;
        }

        try
        {
            // Open the encrypted document using the user password.
            // Document(string, string) constructor handles encrypted PDFs.
            using (Document doc = new Document(encryptedPath, userPassword))
            {
                // Decrypt the document. No parameters are required.
                doc.Decrypt();

                // Save the now-unprotected PDF to the desired output path.
                doc.Save(decryptedPath);
            }

            Console.WriteLine($"Decryption successful. Unprotected file saved as '{decryptedPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect.
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General exception handling for any other errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}