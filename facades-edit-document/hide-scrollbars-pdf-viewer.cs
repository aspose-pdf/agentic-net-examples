using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_hide_scrollbars.pdf";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a PdfContentEditor facade to modify viewer preferences
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document into the editor
        editor.BindPdf(inputPath);

        // Hide UI elements such as scrollbars and navigation controls
        editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);

        // Save the updated PDF
        editor.Save(outputPath);

        Console.WriteLine($"PDF saved with hidden scrollbars: '{outputPath}'.");
    }
}