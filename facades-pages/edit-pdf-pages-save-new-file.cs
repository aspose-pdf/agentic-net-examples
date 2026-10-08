using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // for FontStyles enum

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string backupPath   = "original_backup.pdf";
        const string editedPath   = "edited.pdf";

        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Source file not found: {originalPath}");
            return;
        }

        // Create a backup copy of the original PDF (keeps the source unchanged)
        if (!File.Exists(backupPath))
        {
            File.Copy(originalPath, backupPath);
            Console.WriteLine($"Backup created at '{backupPath}'.");
        }
        else
        {
            Console.WriteLine($"Backup already exists at '{backupPath}'.");
        }

        try
        {
            // Load the original PDF using the high‑level Document API
            Document pdfDocument = new Document(originalPath);

            // Example edit: add a text stamp on the first page
            TextStamp stamp = new TextStamp("Edited with Aspose.Pdf.Facades")
            {
                XIndent = 100,
                YIndent = 500,
                TextState =
                {
                    FontSize = 14,
                    FontStyle = FontStyles.Bold,
                    ForegroundColor = Color.Blue
                }
            };

            // Apply the stamp to the first page (or iterate over all pages if needed)
            pdfDocument.Pages[1].AddStamp(stamp);

            // Save the edited document to a new file; original remains untouched
            pdfDocument.Save(editedPath);
            Console.WriteLine($"Edited PDF saved as '{editedPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during PDF editing: {ex.Message}");
        }
    }
}
