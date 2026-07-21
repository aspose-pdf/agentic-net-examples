using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_centered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use PdfContentEditor facade to modify viewer preferences
        using (PdfContentEditor editor = new PdfContentEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Set the CenterWindow flag (true) using the ViewerPreference constant
            editor.ChangeViewerPreference(ViewerPreference.CenterWindow);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with CenterWindow enabled: {outputPath}");
    }
}