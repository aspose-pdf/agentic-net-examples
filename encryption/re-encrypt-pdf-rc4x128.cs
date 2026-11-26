using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";   // PDF already encrypted with an owner password
        const string outputPath     = "re_encrypted_output.pdf";
        const string ownerPassword  = "ownerPass123";          // password that grants full permissions
        const string userPassword   = "userPass456";           // password to be required for opening after re‑encryption

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the existing PDF using the owner password (full access)
            using (Document doc = new Document(inputPath, ownerPassword))
            {
                // Define desired permissions for the new encryption
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt the document with the specified user/owner passwords
                // and the RC4x128 algorithm (as required)
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.RC4x128);

                // Save the newly encrypted PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document re‑encrypted and saved to '{outputPath}'.");
        }
        catch (InvalidPasswordException ex)
        {
            Console.Error.WriteLine($"Password error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}