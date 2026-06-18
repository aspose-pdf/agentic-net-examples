using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "continuous_view.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Bind the source PDF file
        editor.BindPdf(inputPath);

        // Set continuous view mode (single‑column layout)
        editor.ChangeViewerPreference(ViewerPreference.PageLayoutOneColumn);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources held by the facade
        editor.Close();

        Console.WriteLine($"PDF saved with continuous view mode: {outputPath}");
    }
}