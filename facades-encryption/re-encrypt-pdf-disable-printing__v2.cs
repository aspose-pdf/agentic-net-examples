using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace included as requested

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "reprotected_output.pdf";
        const string userPassword   = "user123";   // existing user password
        const string newOwnerPassword = "newOwner456"; // new owner password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the encrypted PDF using the existing user password
            using (Document doc = new Document(inputPath, userPassword))
            {
                // Define permissions: exclude PrintDocument to disallow printing
                Permissions perms = Permissions.ExtractContent; // any permissions you want to keep

                // Re‑encrypt the document with the new owner password and the desired permissions
                doc.Encrypt(userPassword, newOwnerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with updated owner password and printing disabled: {outputPath}");
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