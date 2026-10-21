using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath      = "input.pdf";
        const string outputPath     = "encrypted.pdf";
        const string attachmentPath = "attachment.pdf";
        const string userPassword   = "user123";
        const string ownerPassword  = "owner123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }
        if (!File.Exists(attachmentPath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- Add an attachment using EmbeddedFiles -----
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentPath));
                fileSpec.Contents = new MemoryStream(File.ReadAllBytes(attachmentPath));
                doc.EmbeddedFiles.Add(fileSpec);

                // ----- Set viewer preferences (if supported by the library version) -----
                try
                {
                    dynamic dynDoc = doc; // bypass compile‑time check
                    var prefs = dynDoc.ViewerPreferences;
                    prefs.HideToolbar      = true;
                    prefs.HideMenubar      = true;
                    prefs.FitWindow       = true;
                    prefs.CenterWindow    = true;
                    prefs.DisplayDocTitle = true;
                }
                catch (RuntimeBinderException)
                {
                    // ViewerPreferences property is not available in the referenced Aspose.Pdf version.
                    Console.WriteLine("ViewerPreferences not supported by the current Aspose.Pdf library version; skipping this step.");
                }

                // ----- Encrypt the document with a user password -----
                Permissions perms = Permissions.PrintDocument | Permissions.ExtractContent;
                doc.Encrypt(userPassword, ownerPassword, perms, CryptoAlgorithm.AESx256);

                // ----- Save the encrypted PDF -----
                doc.Save(outputPath);
            }

            Console.WriteLine($"Encrypted PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
