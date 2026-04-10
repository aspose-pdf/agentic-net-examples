using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source encrypted PDF, a temporary decrypted copy, and the final output.
        const string encryptedPath = "encrypted_input.pdf";
        const string tempDecryptedPath = "temp_decrypted.pdf";
        const string finalOutputPath = "output_with_viewer_preferences.pdf";

        // Password that unlocks the encrypted PDF (owner or user password).
        const string password = "ownerPassword";

        // -----------------------------------------------------------------
        // Step 0: Verify the source file exists.
        // -----------------------------------------------------------------
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: '{encryptedPath}'. Ensure the file exists before running the program.");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Decrypt the encrypted PDF using PdfFileSecurity.
        // -----------------------------------------------------------------
        // First, optionally verify that the file is actually encrypted.
        var fileInfo = new PdfFileInfo(encryptedPath);
        if (!fileInfo.IsEncrypted)
        {
            Console.Error.WriteLine("The input PDF is not encrypted. No decryption needed.");
            return;
        }

        // Bind the encrypted PDF, decrypt it with the supplied password, and save the decrypted copy.
        using (var security = new PdfFileSecurity())
        {
            security.BindPdf(encryptedPath);
            // DecryptFile overload takes only the password after the PDF is bound.
            security.DecryptFile(password);
            security.Save(tempDecryptedPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Apply viewer preferences to the now‑decrypted PDF.
        // -----------------------------------------------------------------
        using (var editor = new PdfContentEditor())
        {
            editor.BindPdf(tempDecryptedPath);
            // Example viewer preference – hide the menu bar.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.Save(finalOutputPath);
        }

        // -----------------------------------------------------------------
        // Optional cleanup: remove the temporary decrypted file.
        // -----------------------------------------------------------------
        try
        {
            File.Delete(tempDecryptedPath);
        }
        catch
        {
            // If deletion fails, ignore – the file is not critical.
        }

        Console.WriteLine($"Viewer preferences applied and saved to '{finalOutputPath}'.");
    }
}
