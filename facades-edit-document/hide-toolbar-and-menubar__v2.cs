using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Initialize the PdfContentEditor facade and bind the document
            PdfContentEditor editor = new PdfContentEditor();
            editor.BindPdf(doc);

            // Combine the HideToolbar and HideMenubar flags using bitwise OR
            int viewerPrefs = ViewerPreference.HideToolbar | ViewerPreference.HideMenubar;

            // Apply the viewer preferences to the document
            editor.ChangeViewerPreference(viewerPrefs);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Minimalistic UI PDF saved to '{outputPath}'.");
    }
}