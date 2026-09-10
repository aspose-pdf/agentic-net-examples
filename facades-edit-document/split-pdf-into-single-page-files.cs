using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Path to the edited PDF that contains annotations
        const string editedPdfPath = "edited.pdf";

        // Folder where the single‑page PDFs will be saved
        const string outputFolder = "Pages";

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        // Template for the output files – %NUM% will be replaced by the page number
        string fileNameTemplate = Path.Combine(outputFolder, "page%NUM%.pdf");

        // Verify the source file exists
        if (!File.Exists(editedPdfPath))
        {
            Console.Error.WriteLine($"Source file not found: {editedPdfPath}");
            return;
        }

        // PdfFileEditor does not implement IDisposable, so we instantiate it directly
        PdfFileEditor editor = new PdfFileEditor();

        // Split the PDF into single‑page documents; each page (with its annotations) is saved
        // to a separate file according to the template above.
        editor.SplitToPages(editedPdfPath, fileNameTemplate);

        Console.WriteLine("PDF has been split into individual pages.");
    }
}