using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the encrypted source, a temporary decrypted file, and the final output
        const string encryptedPath = "encrypted.pdf";
        const string tempDecryptedPath = "temp_decrypted.pdf";
        const string finalPath = "final.pdf";

        // Password that can open the encrypted PDF (owner or user password)
        const string password = "ownerpass";

        // Verify that the encrypted PDF exists before attempting to process it.
        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"The file '{encryptedPath}' was not found. Please provide a valid encrypted PDF.");
            return;
        }

        // ---------- Decrypt the encrypted PDF ----------
        // The obsolete constructor that takes source and destination paths has been replaced.
        // Use the parameter‑less constructor, bind the source file, then call DecryptFile.
        // After a successful decryption, save the result to a temporary file.
        using (PdfFileSecurity security = new PdfFileSecurity())
        {
            security.BindPdf(encryptedPath);
            bool decrypted = security.DecryptFile(password);
            if (!decrypted)
            {
                Console.Error.WriteLine("Failed to decrypt the PDF. The password may be incorrect.");
                return;
            }
            // Save the decrypted PDF to a temporary location.
            security.Save(tempDecryptedPath);
        }

        // ---------- Apply viewer preferences ----------
        // PdfContentEditor is a facade for editing PDF view settings.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the previously decrypted temporary file.
            editor.BindPdf(tempDecryptedPath);

            // Example viewer preferences:
            // Hide the menu bar and set page mode to "UseNone".
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

            // Save the result to the final output file.
            editor.Save(finalPath);
        }

        // Clean up the temporary decrypted file.
        try { File.Delete(tempDecryptedPath); } catch { }

        Console.WriteLine($"Viewer preferences applied and saved to '{finalPath}'.");
    }
}
