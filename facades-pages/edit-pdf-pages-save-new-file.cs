using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string originalPath = "original.pdf";
        const string backupPath   = "original_backup.pdf";
        const string editedPath   = "edited.pdf";

        // Verify the source PDF exists
        if (!File.Exists(originalPath))
        {
            Console.Error.WriteLine($"Source file not found: {originalPath}");
            return;
        }

        // Create a backup copy of the original PDF
        File.Copy(originalPath, backupPath, overwrite: true);

        // Load the PDF document using the high‑level API
        Document doc = new Document(originalPath);

        // Example edit: add a visible text stamp on the first page
        TextStamp stamp = new TextStamp("Edited");
        stamp.HorizontalAlignment = HorizontalAlignment.Center;
        stamp.VerticalAlignment   = VerticalAlignment.Top;
        // TextState is read‑only; modify its properties directly
        stamp.TextState.FontSize       = 24;
        stamp.TextState.FontStyle      = FontStyles.Bold;
        stamp.TextState.ForegroundColor = Color.FromRgb(1, 0, 0); // Red color

        // Apply the stamp to page 1 (pages are 1‑based)
        doc.Pages[1].AddStamp(stamp);

        // Save the edited PDF to a new file; the original remains unchanged
        doc.Save(editedPath);

        Console.WriteLine($"Backup created at '{backupPath}'.");
        Console.WriteLine($"Edited PDF saved at '{editedPath}'.");
    }
}
