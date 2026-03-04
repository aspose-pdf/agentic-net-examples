using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Paths for the encrypted PDF and the output (decrypted) PDF
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string decryptedPdfPath = "decrypted_output.pdf";
        const string userPassword = "user123"; // replace with the actual password

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the password constructor.
            // This loads the document in encrypted mode.
            using (Document doc = new Document(encryptedPdfPath, userPassword))
            {
                // Decrypt the document. After this call the PDF is no longer encrypted.
                doc.Decrypt();

                // Save the decrypted PDF. No SaveOptions are needed because we are saving as PDF.
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