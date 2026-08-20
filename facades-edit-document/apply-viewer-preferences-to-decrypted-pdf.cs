using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string tempDecryptedPath = "temp_decrypted.pdf";
        const string outputPath = "final.pdf";
        const string password = "ownerpass";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"Input file not found: {encryptedPath}");
            return;
        }

        // Decrypt the encrypted PDF using PdfFileSecurity.
        // The constructor takes the source and destination file paths.
        PdfFileSecurity fileSecurity = new PdfFileSecurity(encryptedPath, tempDecryptedPath);
        bool success = fileSecurity.DecryptFile(password);
        if (!success)
        {
            Console.Error.WriteLine("Decryption failed. Check the password.");
            return;
        }

        // Apply viewer preferences to the decrypted PDF using PdfContentEditor.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(tempDecryptedPath);

        // Example viewer preferences: hide the menu bar and toolbar.
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

        // Save the modified PDF.
        editor.Save(outputPath);
        editor.Close();

        // Clean up the temporary decrypted file.
        try { File.Delete(tempDecryptedPath); } catch { }

        Console.WriteLine($"Viewer preferences applied. Output saved to '{outputPath}'.");
    }
}