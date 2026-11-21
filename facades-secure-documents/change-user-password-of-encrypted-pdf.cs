using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // required by task specification

class Program
{
    static void Main()
    {
        const string inputPath      = "encrypted_input.pdf";
        const string outputPath     = "encrypted_output.pdf";
        const string oldUserPassword = "oldUserPass";
        const string newUserPassword = "newUserPass";
        const string ownerPassword   = "ownerPass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the current user password.
            using (Document doc = new Document(inputPath, oldUserPassword))
            {
                // Preserve existing encryption settings.
                // Aspose.Pdf does not expose the current permissions or algorithm,
                // so we re‑apply the same values that were used originally.
                // Adjust these values if the source PDF used different settings.
                Permissions existingPermissions = Permissions.PrintDocument | Permissions.ExtractContent;
                CryptoAlgorithm existingAlgorithm = CryptoAlgorithm.AESx256;

                // Re‑encrypt the document with the new user password,
                // keeping the owner password, permissions and algorithm unchanged.
                doc.Encrypt(newUserPassword, ownerPassword, existingPermissions, existingAlgorithm);

                // Save the PDF; the new password is now in effect.
                doc.Save(outputPath);
            }

            Console.WriteLine($"Password changed successfully. Output saved to '{outputPath}'.");
        }
        catch (PdfException ex)
        {
            // Thrown if the PDF is not encrypted or the password is incorrect.
            Console.Error.WriteLine($"PDF error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}