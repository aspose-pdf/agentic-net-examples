using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string encryptedPath = "encrypted.pdf";
        const string password = "ownerpass";
        const string tempDecryptedPath = "decrypted.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(encryptedPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPath}");
            return;
        }

        // Decrypt the PDF using Document with the provided password
        using (Document encryptedDoc = new Document(encryptedPath, password))
        {
            encryptedDoc.Decrypt();
            encryptedDoc.Save(tempDecryptedPath);
        }

        // Apply a viewer preference to the decrypted PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(tempDecryptedPath);
            // Example: hide the toolbar in PDF viewers
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);
            editor.Save(outputPath);
        }

        // Clean up the temporary decrypted file
        try
        {
            File.Delete(tempDecryptedPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Could not delete temporary file: {ex.Message}");
        }

        Console.WriteLine($"Viewer preference applied and saved to '{outputPath}'.");
    }
}
