using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "output.pdf";
        const string password   = "userpass"; // correct decryption password

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Open the encrypted PDF with the provided password
            using (Document doc = new Document(inputPath, password))
            {
                // Decrypt the document in memory
                doc.Decrypt();

                // Apply viewer preferences using PdfContentEditor
                using (PdfContentEditor editor = new PdfContentEditor())
                {
                    editor.BindPdf(doc);
                    // Example preferences: hide the menu bar and use no page mode
                    editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
                    editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);
                    // Save the modified PDF
                    editor.Save(outputPath);
                }
            }

            Console.WriteLine($"Viewer preferences applied and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}