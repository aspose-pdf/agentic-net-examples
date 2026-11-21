using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // Needed for RuntimeBinderException when using dynamic

class Program
{
    static void Main()
    {
        // Paths – adjust as needed
        const string inputPdfPath = "input.pdf";
        const string attachmentPath = "attachment.txt";
        const string encryptedPdfPath = "encrypted.pdf";

        // Passwords for encryption
        const string userPassword = "user123";
        const string ownerPassword = "owner123";

        // Verify required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdfPath))
            {
                // -------------------------------------------------------------
                // 1. Add attachment (use EmbeddedFiles collection)
                // -------------------------------------------------------------
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath), "Sample attachment description")
                {
                    Contents = new MemoryStream(File.ReadAllBytes(attachmentPath))
                };
                doc.EmbeddedFiles.Add(fileSpec);

                // -------------------------------------------------------------
                // 2. Set viewer preferences (guarded for older Aspose.Pdf versions)
                // -------------------------------------------------------------
                try
                {
                    // Use dynamic to avoid compile‑time binding when the property is missing
                    dynamic dynDoc = doc;
                    var prefs = dynDoc.ViewerPreferences;
                    prefs.HideToolbar = true;
                    prefs.HideMenubar = true;
                    prefs.HideWindowUI = false;
                    prefs.FitWindow = true;
                    prefs.CenterWindow = true;
                    prefs.DisplayDocTitle = true;
                }
                catch (RuntimeBinderException)
                {
                    // ViewerPreferences not available in the referenced Aspose.Pdf version
                    Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version.");
                }

                // -------------------------------------------------------------
                // 3. Encrypt the PDF with a user password
                // -------------------------------------------------------------
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // Save the encrypted PDF
                doc.Save(encryptedPdfPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{encryptedPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
