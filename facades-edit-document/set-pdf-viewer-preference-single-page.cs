using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_single_page.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a facade for editing PDF content and viewer preferences.
        // It implements IDisposable, so we wrap it in a using block.
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Set the viewer preference to display one page at a time.
            // ViewerPreference.PageLayoutSinglePage is a constant defined in Aspose.Pdf.Facades.
            editor.ChangeViewerPreference(ViewerPreference.PageLayoutSinglePage);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference set to single‑page layout. Saved to '{outputPath}'.");
    }
}