using System;
using System.IO;
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

        // Initialize the facade and bind the source PDF
        PdfContentEditor editor = new PdfContentEditor();
        editor.BindPdf(inputPath);

        // Hide UI elements (scrollbars, navigation controls) for small‑screen devices
        editor.ChangeViewerPreference(ViewerPreference.HideWindowUI);

        // Save the modified document
        editor.Save(outputPath);

        // Release resources if the facade supports it
        editor.Close();

        Console.WriteLine($"PDF saved with hidden UI to '{outputPath}'.");
    }
}