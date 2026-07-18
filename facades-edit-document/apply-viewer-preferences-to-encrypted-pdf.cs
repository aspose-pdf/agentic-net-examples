using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";   // input encrypted PDF
        const string tempDecryptedPath = "temp_decrypted.pdf"; // intermediate decrypted file
        const string outputPath = "final.pdf";          // result with viewer preferences
        const string ownerPassword = "ownerpass";       // correct decryption password

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // ---------- Decrypt the PDF ----------
        // PdfFileSecurity ctor takes input and output file paths.
        using (PdfFileSecurity security = new PdfFileSecurity(encryptedPath, tempDecryptedPath))
        {
            bool success = security.DecryptFile(ownerPassword);
            if (!success)
            {
                Console.Error.WriteLine("Decryption failed. Check the password.");
                return;
            }
        }

        // ---------- Apply viewer preferences ----------
        // PdfContentEditor works on a regular (decrypted) PDF file.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(tempDecryptedPath);

            // Example viewer preferences:
            // Hide the menu bar and the toolbar.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF.
            editor.Save(outputPath);
            editor.Close();
        }

        // Clean up the temporary decrypted file.
        try
        {
            File.Delete(tempDecryptedPath);
        }
        catch
        {
            // Ignored – if deletion fails, the file will remain on disk.
        }

        Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
    }
}