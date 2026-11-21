using System;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <input.pdf> <attachment.file> <output.pdf>
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <input.pdf> <attachment.file> <output.pdf>");
            return;
        }

        string inputPdfPath       = args[0];
        string attachmentFilePath = args[1];
        string outputPdfPath      = args[2];

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(attachmentFilePath))
        {
            Console.Error.WriteLine($"Attachment file not found: {attachmentFilePath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ----- Add attachment via EmbeddedFiles -----
                var fileSpec = new FileSpecification(Path.GetFileName(attachmentFilePath))
                {
                    Contents = new MemoryStream(File.ReadAllBytes(attachmentFilePath))
                };
                pdfDoc.EmbeddedFiles.Add(fileSpec);

                // ----- Set viewer preferences (runtime‑safe) -----
                // The ViewerPreferences property is available only in newer Aspose.Pdf versions.
                // To keep the source compatible with older versions we access it dynamically.
                dynamic doc = pdfDoc;
                try
                {
                    var prefs = doc.ViewerPreferences;
                    prefs.HideToolbar      = true;   // Hide the toolbar
                    prefs.HideMenubar      = true;   // Hide the menu bar
                    prefs.CenterWindow     = true;   // Center the window on screen
                    prefs.DisplayDocTitle = true;   // Show document title in the window title
                }
                catch (RuntimeBinderException)
                {
                    // Property not present – gracefully continue without setting preferences.
                    Console.WriteLine("ViewerPreferences property is not supported by the current Aspose.Pdf library version; skipping this step.");
                }

                // Save the modified PDF
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"Attachment added and viewer preferences set (if supported). Saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
