using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "ToolbarRemoved.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // PdfContentEditor does NOT implement IDisposable, so we instantiate it normally.
            PdfContentEditor editor = new PdfContentEditor();

            // Bind the loaded document to the editor.
            editor.BindPdf(doc);

            // Hide the toolbar in the PDF viewer preferences.
            // The ChangeViewerPreference method takes a single ViewerPreference enum value.
            editor.ChangeViewerPreference(ViewerPreference.HideToolbar);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved without toolbar to '{outputPath}'.");
    }
}
