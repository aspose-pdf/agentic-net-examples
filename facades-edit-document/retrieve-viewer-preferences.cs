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

        // Ensure the source PDF exists – create a minimal document if it does not.
        if (!File.Exists(inputPath))
        {
            // Create a one‑page PDF with default settings.
            using (var doc = new Document())
            {
                // Add an empty page.
                doc.Pages.Add();
                doc.Save(inputPath);
            }
        }

        // Bind the existing (or newly created) PDF.
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Change first viewer preference.
        editor.ChangeViewerPreference(ViewerPreference.HideMenubar);
        int prefAfterFirstChange = editor.GetViewerPreference();
        Console.WriteLine($"Preferences after HideMenubar: 0x{prefAfterFirstChange:X}");

        // Change second viewer preference – use the correct enum member.
        editor.ChangeViewerPreference(ViewerPreference.NonFullScreenPageModeUseOutlines);
        int prefAfterSecondChange = editor.GetViewerPreference();
        Console.WriteLine($"Preferences after NonFullScreenPageModeUseOutlines: 0x{prefAfterSecondChange:X}");

        // Save the modified PDF.
        editor.Save(outputPath);
    }
}
