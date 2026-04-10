using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input parameters
        const string inputPath        = "protected_input.pdf";   // Encrypted source PDF
        const string decryptedPath    = "decrypted_temp.pdf";    // Intermediate decrypted file
        const string updatedPath      = "updated_temp.pdf";      // Intermediate file with new Creator
        const string outputPath       = "re_encrypted_output.pdf"; // Final encrypted PDF

        const string ownerPassword    = "ownerPass";   // Original owner password
        const string newUserPassword  = "newUserPass"; // New user password (can be same as original)
        const string newOwnerPassword = "newOwnerPass"; // New owner password (can be same as original)

        const string newCreator       = "My Application v1.0"; // New CreatorTool value

        // Ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Decrypt the protected PDF using PdfFileSecurity (Facades API)
        // ------------------------------------------------------------
        using (PdfFileSecurity decryptor = new PdfFileSecurity(inputPath, decryptedPath))
        {
            // DecryptFile throws on failure; use the overload that returns bool if you prefer non‑exception handling
            decryptor.DecryptFile(ownerPassword);
        }

        // ------------------------------------------------------------
        // 2. Update the Creator metadata using PdfFileInfo (Facades API)
        // ------------------------------------------------------------
        using (PdfFileInfo info = new PdfFileInfo(decryptedPath))
        {
            info.Creator = newCreator;               // Set the new CreatorTool value
            info.SaveNewInfo(updatedPath);           // Persist changes to a new file
        }

        // ------------------------------------------------------------
        // 3. Re‑encrypt the PDF with new passwords and privileges
        // ------------------------------------------------------------
        using (PdfFileSecurity encryptor = new PdfFileSecurity(updatedPath, outputPath))
        {
            // Encrypt with desired privileges and 256‑bit key size
            encryptor.EncryptFile(newUserPassword, newOwnerPassword,
                                  DocumentPrivilege.Print, KeySize.x256);
        }

        // Optional: clean up temporary files
        try { File.Delete(decryptedPath); } catch { }
        try { File.Delete(updatedPath);   } catch { }

        Console.WriteLine($"Processing complete. Encrypted PDF saved to '{outputPath}'.");
    }
}