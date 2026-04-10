using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hide_scrollbars.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // PdfContentEditor is a facade for low‑level PDF modifications.
        // It implements IFacade and provides ChangeViewerPreference to set viewer flags.
        PdfContentEditor editor = new PdfContentEditor();
        try
        {
            // Load the source PDF.
            editor.BindPdf(inputPath);

            // Hide UI elements (scrollbars, navigation controls, etc.).
            editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);

            // Optionally also hide the menu bar and toolbar for an even cleaner view.
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF.
            editor.Save(outputPath);
        }
        finally
        {
            // Ensure resources are released.
            editor.Close();
        }

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}