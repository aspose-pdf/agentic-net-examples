using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_fullscreen.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Create the facade for content editing
            PdfContentEditor editor = new PdfContentEditor();

            // Load the PDF document into the editor
            editor.BindPdf(inputPath);

            // Activate full‑screen viewer preference (no UI elements)
            editor.ChangeViewerPreference(ViewerPreference.PageModeFullScreen);

            // Save the modified PDF
            editor.Save(outputPath);

            // Release resources held by the facade
            editor.Close();

            Console.WriteLine($"PDF saved with full‑screen mode: '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}