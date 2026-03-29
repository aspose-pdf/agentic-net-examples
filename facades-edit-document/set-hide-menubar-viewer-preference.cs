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

        // Read current viewer preferences, set HideMenubar, and save the PDF
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);
            int currentPreferences = editor.GetViewerPreference();
            Console.WriteLine($"Current viewer preferences: 0x{currentPreferences:X}");

            // Enable the HideMenubar flag
            editor.ChangeViewerPreference(ViewerPreference.HideMenubar);

            // Persist the changes
            editor.Save(outputPath);
        }

        Console.WriteLine($"Viewer preference updated and saved to '{outputPath}'.");
    }
}