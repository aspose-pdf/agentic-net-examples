using System;
using System.IO;
using Aspose.Pdf.Facades; // PdfContentEditor, ViewerPreference

class Program
{
    static void Main()
    {
        // Paths for the source PDF and the resulting PDF
        const string inputPath  = "input.pdf";
        const string outputPath = "output_single_page.pdf";

        // Verify that the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor (a Facades class) to modify viewer preferences.
        // The class implements IDisposable, so we wrap it in a using block.
        using (Aspose.Pdf.Facades.PdfContentEditor editor = new Aspose.Pdf.Facades.PdfContentEditor())
        {
            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Set the viewer preference to display one page at a time.
            // Fully‑qualify the enum to avoid ambiguity with other ViewerPreference types.
            editor.ChangeViewerPreference(Aspose.Pdf.Facades.ViewerPreference.PageLayoutSinglePage);

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference set to single‑page layout. Saved as '{outputPath}'.");
    }
}
