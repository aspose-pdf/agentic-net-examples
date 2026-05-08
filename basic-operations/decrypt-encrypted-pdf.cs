using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths for the encrypted input PDF and the decrypted output PDF
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string decryptedPdfPath = "decrypted_output.pdf";

        // The user password required to open the encrypted PDF
        const string userPassword = "user123";

        // Verify that the input file exists before proceeding
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Error: File not found – {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted document using the constructor that accepts a password
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document; after this call the PDF is no longer protected
                doc.Decrypt();

                // Save the unprotected version to a new file
                doc.Save(decryptedPdfPath);
            }

            Console.WriteLine($"Decryption successful. Unprotected PDF saved to '{decryptedPdfPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            // Thrown when the supplied password is incorrect
            Console.Error.WriteLine($"Invalid password: {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}