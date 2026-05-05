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

        // Create the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Combine viewer preference flags to hide toolbar and menubar
        int hidePrefs = ViewerPreference.HideToolbar | ViewerPreference.HideMenubar;

        // Apply the viewer preferences
        editor.ChangeViewerPreference(hidePrefs);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources (optional but recommended)
        editor.Close();

        Console.WriteLine($"Viewer preferences updated and saved to '{outputPath}'.");
    }
}