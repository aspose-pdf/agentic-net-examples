using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace is included as requested

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "encrypted_output.pdf";
        const string oldUserPwd     = "oldUserPassword";
        const string oldOwnerPwd    = "oldOwnerPassword";
        const string newUserPwd     = "newUserPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF using the existing user password.
            // If the PDF is not encrypted this will also succeed.
            using (Document doc = new Document(inputPath, oldUserPwd))
            {
                // Preserve encryption settings:
                // - Keep the same owner password.
                // - Use a strong algorithm (AESx256) as recommended.
                // - Preserve typical permissions (adjust if you know the original set).
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;

                // Re‑encrypt with the new user password while keeping other settings.
                doc.Encrypt(newUserPwd, oldOwnerPwd, perms, CryptoAlgorithm.AESx256);

                // Save the PDF with the updated password.
                doc.Save(outputPath);
            }

            Console.WriteLine($"User password changed successfully. Saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            // Thrown if the PDF is encrypted and the supplied password is incorrect,
            // or if the file is not a valid PDF.
            Console.Error.WriteLine($"Failed to open encrypted PDF: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}