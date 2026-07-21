using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_use_none.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the PdfContentEditor facade
        PdfContentEditor editor = new PdfContentEditor();

        // Load the PDF document
        editor.BindPdf(inputPath);

        // Set the viewer preference to PageModeUseNone (hide navigation panels)
        editor.ChangeViewerPreference(ViewerPreference.PageModeUseNone);

        // Save the modified PDF
        editor.Save(outputPath);

        // Release resources
        editor.Close();

        Console.WriteLine($"PDF saved with PageMode UseNone: {outputPath}");
    }
}