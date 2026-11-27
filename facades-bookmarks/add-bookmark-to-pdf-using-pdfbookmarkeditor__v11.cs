using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // PdfBookmarkEditor does not implement IDisposable, so we use a plain instance.
            PdfBookmarkEditor editor = new PdfBookmarkEditor();

            // Load (bind) the existing PDF document.
            editor.BindPdf(inputPath);

            // Create a bookmark titled "Overview" that points to page 4 (1‑based indexing).
            // The facade provides CreateBookmarkOfPage for this purpose.
            editor.CreateBookmarkOfPage("Overview", 4);

            // Save the updated PDF to the specified output path.
            editor.Save(outputPath);

            // Close the editor (optional but recommended to release resources).
            editor.Close();

            Console.WriteLine($"Bookmark added and PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
