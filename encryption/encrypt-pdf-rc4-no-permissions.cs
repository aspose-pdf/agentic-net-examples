using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Security;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // source PDF
        const string outputPath = "user6RC4.pdf";   // encrypted output
        const string userPassword = "user6";        // password to open the PDF
        const string ownerPassword = "";            // owner password (can be empty)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the PDF with the user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Encrypt with RC4 128‑bit and no permissions (all privileges disabled)
                Permissions noPermissions = (Permissions)0; // equivalent to "no privileges"
                doc.Encrypt(userPassword, ownerPassword, noPermissions, CryptoAlgorithm.RC4x128);

                // Save the encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved as '{outputPath}'.");
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
