using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "protected.pdf";   // encrypted PDF
        const string ownerPassword  = "ownerSecret";     // owner password
        const string outputPath     = "decrypted.pdf";   // result after decryption

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted document using the owner password.
            // The owner password can be supplied in the same overload as the user password.
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Decrypt the document. No parameters are required.
                doc.Decrypt();

                // Save the now‑decrypted PDF.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Decrypted PDF saved to '{outputPath}'.");
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