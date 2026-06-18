using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths and password
        const string encryptedPdfPath = "encrypted.pdf";
        const string ownerPassword    = "ownerpass";
        const string tempPdfPath      = "temp_decrypted.pdf";
        const string outputPdfPath    = "final.pdf";

        // Verify input file exists
        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {encryptedPdfPath}");
            return;
        }

        // -----------------------------------------------------------------
        // 1. Decrypt the encrypted PDF using PdfFileSecurity (Facades API)
        // -----------------------------------------------------------------
        bool decryptionSucceeded;
        using (PdfFileSecurity security = new PdfFileSecurity(encryptedPdfPath, tempPdfPath))
        {
            // DecryptFile returns true on success; throws on failure otherwise
            decryptionSucceeded = security.DecryptFile(ownerPassword);
        }

        if (!decryptionSucceeded)
        {
            Console.Error.WriteLine("Decryption failed.");
            return;
        }

        // ---------------------------------------------------------------
        // 2. Apply viewer preferences to the now‑decrypted temporary PDF
        // ---------------------------------------------------------------
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the decrypted PDF
            editor.BindPdf(tempPdfPath);

            // Example viewer preferences – hide the toolbar and use no page mode
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
            editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

            // Save the result to the final output file
            editor.Save(outputPdfPath);
            editor.Close();
        }

        // Clean up the temporary decrypted file
        try
        {
            File.Delete(tempPdfPath);
        }
        catch
        {
            // Ignored – if deletion fails the temp file will remain on disk
        }

        Console.WriteLine($"Viewer preferences applied. Output saved to '{outputPdfPath}'.");
    }
}